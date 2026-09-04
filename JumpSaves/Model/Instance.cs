using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;

namespace JumpSaves.Model
{
    struct OnlyManagerShouldCreateThis { }

    public class PeriodicInfoArgs : EventArgs
    {
        public bool IsRunning { get; set; }

        public bool HasAutoReopened { get; set; }
    }

    public class Instance : IDisposable
    {
        internal Instance(SynchronizationContext syncContext, Manager manager, OnlyManagerShouldCreateThis onlyManagerShouldCreateThis)
        {
            syncContext_ = syncContext;
            manager_ = manager;

#if DEBUG
            DefaultSavePath = "C:\\Prj\\JumpSpaceSaves\\Data\\NewVersion_2";
#else
            DefaultSavePath = JSL.SaveDir.GetDefault(false)?.Path;
#endif

            DefaultSavePathHasExperimental = JSL.SaveDir.HasExperimental(DefaultSavePath);

            manager_.PeriodicInfoEvent += OnGlobalPeriodicInfo;
        }

        public void Dispose()
        {
            Close();
        }

        public string DefaultSavePath { get; private set; }

        public bool DefaultSavePathHasExperimental { get; private set; }

        public void Open(string path, bool experimental)
        {
            Close();

            SaveEditor = JSL.EditorFactory.OpenSave(path, experimental);
            ActionLog.AddEntry(ActionLog.Origin.Editor, ActionLog.Level.Info, $"Opened save \"{Path}\"");
        }

        public void Close()
        {
            if (SaveEditor != null)
            {
                SaveEditor.DirtyChanged = null;
                SaveEditor = null;
                ActionLog.AddEntry(ActionLog.Origin.Editor, ActionLog.Level.Info, "Closed save");
            }
        }

        public void Save()
        {
            if (!IsOpen)
            {
                throw new Exception("Can't save because no save file or directory is open right now");
            }

            if (BackupStore == null)
            {
                throw new Exception("Not ready to save because the backup store hasn't been fully initialized yet");
            }

            BackupStore.Add(Path, IsExperimental, "Before saving");
            ActionLog.AddEntry(ActionLog.Origin.Editor, ActionLog.Level.Info, $"Created a new backup of save \"{Path}\" prior to overwriting it.");

            SaveEditor.Save();
            ActionLog.AddEntry(ActionLog.Origin.Editor, ActionLog.Level.Info, $"Saved \"{Path}\"");
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

        public bool IsExperimental
        {
            get
            {
                return SaveEditor?.IsExperimental ?? false;
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
                return IsGameSaveOpen && IsGameRunning && !IsDirty && (IsMonitoringHook == null || IsMonitoringHook());
            }
        }

        public Settings Settings
        {
            get
            {
                return manager_.Settings;
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
                    
                    IReadOnlyList<string> failures = libraryEditor_.TakeFailures();
                    if (failures != null && failures.Count > 0)
                    {
                        foreach (string failure in failures)
                        {
                            ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Error, failure);
                        }
                    }

                    IReadOnlyList<string> warnings = libraryEditor_.TakeWarnings();
                    if (warnings != null && warnings.Count > 0)
                    {
                        foreach (string warning in warnings)
                        {
                            ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Warning, warning);
                        }
                    }
                }

                return libraryEditor_;
            }
        }

        public JSL.BackupStore BackupStore
        {
            get
            {
                if (backupStore_ == null && manager_.BackupStore != null)
                {
                    backupStore_ = manager_.BackupStore;
                    IReadOnlyList<string> failedPaths = backupStore_.TakeFailedPaths();
                    if (failedPaths != null && failedPaths.Count > 0)
                    {
                        foreach (string path in failedPaths)
                        {
                            ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Error, $"Failed to verify backup path \"{path}\"");
                        }
                    }
                }

                return backupStore_;
            }
        }

        public ActionLog ActionLog
        {
            get
            {
                return manager_.ActionLog;
            }
        }

        public Func<bool> IsMonitoringHook { get; set; }

        public event EventHandler<PeriodicInfoArgs> PeriodicInfoEvent;

        public void RunCLI(string path = null)
        {
            string selfPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string cliPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(selfPath), "JumpSavesCLI.exe");
            Process.Start(cliPath, $"-s \"{path ?? (SaveEditor?.Path ?? DefaultSavePath)}\"");
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
            catch (Exception ex)
            {
                ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Error, $"Failed to transfer item \"{name}\" to {destination.SelfDesignation}: {ex.Message}");
                throw;
            }
        }

        public void AutoAcquireIntoLibrary(Func<JSL.MajorItemEditor, bool> filter)
        {
            if (!IsOpen)
            {
                throw new Exception("Can't auto-acquire when no save is open");
            }

            ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Verbose, "Started Auto-acquiring items to the Library");

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

            ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Verbose, "Finished Auto-acquiring items to the Library");
        }

        private void TransferToLibrary(JSL.MajorItemEditor item, JSL.ConflictBehavior onConflict, bool isAutomated)
        {
            string name = item.Name ?? "Unknown";
            string mode = isAutomated ? "Auto-acquire" : "Acquire";

            try
            {
                if (LibraryEditor.Add(item, onConflict))
                {
                    ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Info, $"{mode}d item \"{name}\" to the Library");
                }
            }
            catch (Exception ex)
            {
                ActionLog.AddEntry(ActionLog.Origin.Library, ActionLog.Level.Error, $"Failed to {mode} item \"{name}\" to the Library: {ex.Message}");
                throw;
            }
        }

        private void OnGlobalPeriodicInfo(object sender, GlobalPeriodicInfoEventArgs args)
        {
            PostOnUIThread(() =>
            {
                if (IsGameRunning != args.IsGameRunning)
                {
                    string state = args.IsGameRunning ? "running" : "not running";
                    ActionLog.AddEntry(ActionLog.Origin.Application, ActionLog.Level.Info, $"Jump Space game is now {state}");
                }

                IsGameRunning = args.IsGameRunning;

                PeriodicInfoEvent?.Invoke(this, new PeriodicInfoArgs
                {
                    IsRunning = args.IsGameRunning,
                    HasAutoReopened = ReopenIfNewerAndMonitoring()
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
                try
                {
                    JSL.SaveEditor editor = JSL.EditorFactory.OpenSave(Path, IsExperimental);
                    if (SaveEditor != null)
                    {
                        SaveEditor.DirtyChanged = null;
                    }
                    SaveEditor = editor;
                    ActionLog.AddEntry(ActionLog.Origin.Editor, ActionLog.Level.Info, $"Detected a change and re-opened save \"{Path}\"");
                    return true;
                }
                catch (Exception ex)
                {
                    ActionLog.AddEntry(ActionLog.Origin.Editor, ActionLog.Level.Warning, $"Detected a change but failed to re-open save \"{Path}\" (will retry): {ex.Message}");
                }
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
        private JSL.BackupStore backupStore_;
    }
}
