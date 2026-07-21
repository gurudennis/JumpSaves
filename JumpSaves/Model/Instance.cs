using System;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;

namespace JumpSaves.Model
{
    struct OnlyManagerShouldCreateThis { }

    public class PeriodicInfoArgs : EventArgs
    {
        public bool IsRunning { get; set; }

        public bool HasReopened { get; set; }
    }

    public class Instance : IDisposable
    {
        internal Instance(SynchronizationContext syncContext, Manager manager, OnlyManagerShouldCreateThis onlyManagerShouldCreateThis)
        {
            syncContext_ = syncContext;
            manager_ = manager;

            this.ActionLog = new ActionLog();

            manager_.PeriodicInfoEvent += OnGlobalPeriodicInfo;
        }

        public void Dispose()
        {
            Close();
        }

        public string DefaultSavePath
        {
            get
            {
#if DEBUG
                return "C:\\Prj\\JumpSpaceSaves\\Data\\NewVersion_2";
#else
                return JSL.SaveDir.Default.Path;
#endif
            }
        }

        public void Open(string path)
        {
            Close();

            SaveEditor = JSL.EditorFactory.OpenSave(path);
        }

        public void Close()
        {
            SaveEditor = null;
        }

        public void Save()
        {
            if (!IsOpen)
            {
                throw new Exception("Can't save because no save file or directory is open right now");
            }

            SaveEditor.Save();
        }

        public bool IsOpen
        {
            get
            {
                return SaveEditor != null;
            }
        }

        public bool IsDirty
        {
            get
            {
                return SaveEditor?.IsDirty ?? false;
            }
        }

        public string Path
        {
            get
            {
                return SaveEditor?.Path ?? String.Empty;
            }
        }

        public bool IsGameSaveOpen
        {
            get
            {
                if (!IsOpen)
                {
                    return false;
                }

                if (Path == DefaultSavePath)
                {
                    return true;
                }

                if (System.IO.Path.GetDirectoryName(Path) == DefaultSavePath)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsGameRunning { get; private set; }

        public bool IsMonitoring
        {
            get
            {
                return IsGameSaveOpen && IsGameRunning && !IsDirty;
            }
        }

        public JSL.SaveEditor SaveEditor { get; private set; }

        public JSL.LibraryMajorItemListEditor LibraryEditor
        {
            get
            {
                if (libraryEditor_ == null && manager_.Library != null)
                {
                    libraryEditor_ = JSL.EditorFactory.OpenLibrary(manager_.Library);
                }

                return libraryEditor_;
            }
        }

        public ActionLog ActionLog { get; private set; }

        public event EventHandler<PeriodicInfoArgs> PeriodicInfoEvent;

        public void RunCLI(string path = null)
        {
            string selfPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string cliPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(selfPath), "JumpSavesCLI.exe");
            Process.Start(cliPath, $"-s {path ?? (SaveEditor?.Path ?? DefaultSavePath)}");
        }

        public void TransferToLibrary(JSL.MajorItemEditor item, JSL.ConflictBehavior onConflict)
        {
            TransferToLibrary(item, onConflict, false);
        }

        public void TransferFromLibrary(JSL.MajorItemEditor item, JSL.MajorItemListEditor destination)
        {
            string name = item.Name ?? "Unknown";

            try
            {
                destination.Add(item, JSL.ConflictBehavior.Error);
                ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Info, $"Transferred item \"{name}\" to {destination.SelfDesignation}");
            }
            catch
            {
                ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Error, $"Failed to transfer item \"{name}\" to {destination.SelfDesignation}");
                throw;
            }
        }

        public void AutoAcquireIntoLibrary(Func<JSL.MajorItemEditor, bool> filter)
        {
            if (!IsOpen)
            {
                throw new Exception("Can't auto-acquire when no save is open");
            }

            ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Info, "Started Auto-acquiring items to the Library");

            {
                JSL.MajorItemListEditor stored = SaveEditor.StoredMajorItems;
                for (int i = 0; i < stored.Count; ++i)
                {
                    JSL.MajorItemEditor item = stored[i];
                    if (filter(item))
                    {
                        TransferToLibrary(item, JSL.ConflictBehavior.Skip, true);
                    }
                }
            }

            {
                JSL.MajorItemListEditor recent = SaveEditor.RecentMajorItems;
                for (int i = 0; i < recent.Count; ++i)
                {
                    JSL.MajorItemEditor item = recent[i];
                    if (filter(item))
                    {
                        TransferToLibrary(item, JSL.ConflictBehavior.Skip, true);
                    }
                }
            }

            ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Info, "Finished Auto-acquiring items to the Library");
        }

        private void TransferToLibrary(JSL.MajorItemEditor item, JSL.ConflictBehavior onConflict, bool isAutomated)
        {
            string name = item.Name ?? "Unknown";
            string mode = isAutomated ? "Auto-acquire" : "Acquire";

            try
            {
                LibraryEditor.Add(item, onConflict);
                ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Info, $"{mode}d item \"{name}\" to the Library");
            }
            catch
            {
                ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Error, $"Failed to {mode} item \"{name}\" to the Library");
                throw;
            }
        }

        private void OnGlobalPeriodicInfo(object sender, GlobalPeriodicInfoEventArgs args)
        {
            PostOnUIThread(() =>
            {
                IsGameRunning = args.IsGameRunning;

                PeriodicInfoEvent?.Invoke(this, new PeriodicInfoArgs
                {
                    IsRunning = args.IsGameRunning,
                    HasReopened = ReopenIfNewerAndMonitoring()
                });
            });
        }

        // Reopen the save if 1) we are monitoring the save dir, and 2) there is a newer version on disk
        private bool ReopenIfNewerAndMonitoring()
        {
            if (!IsOpen || !IsMonitoring)
            {
                return false;
            }

            if (SaveEditor.OpenedTime < SaveEditor.LastEditTime)
            {
#if !DEBUG
                try
                {
#endif
                    JSL.SaveEditor editor = JSL.EditorFactory.OpenSave(Path);
                    SaveEditor = editor;
                    return true;
#if !DEBUG
                }
                catch { } // do nothing - will try again on next change
#endif
            }

            return false;
        }

        private void PostOnUIThread(Action action)
        {
            syncContext_.Post(_ => { action(); }, null);
        }

        private readonly Manager manager_;
        private readonly SynchronizationContext syncContext_;
        private JSL.LibraryMajorItemListEditor libraryEditor_;
    }
}
