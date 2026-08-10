using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class MainWindow : Form
    {
        internal MainWindow(Model.Manager modelManager)
        {
            model_ = modelManager.CreateInstance(SynchronizationContext.Current);
            model_.PeriodicInfoEvent += OnPeriodicInfo;
            model_.IsMonitoringHook = () => libraryMajorItemList.ShouldAutoAcquire;

            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (model_.Settings.ShowTutorial)
            {
                TutorialWindow tutorial = new TutorialWindow(model_);
                tutorial.ShowDialog();
            }

            Text = $"JumpSaves {Assembly.GetExecutingAssembly().GetName().Version}";

            colorblindModeToolStripMenuItem.Checked = model_.Settings.Colorblind;
            toolStripComboBoxMode.SelectedIndex = 0;

            editorMajorItemList.TransferAction = DoTransfer;
            editorMajorItemList.LogAction = DoListLog;

            libraryMajorItemList.TransferAction = DoTransfer;
            libraryMajorItemList.LogAction = DoListLog;

            OnStateChanged();

            try
            {
                OpenDefaultDirectory();
            }
            catch
            {
                string msg = $"JumpSaves couldn't locate a valid Jump Ship save directory automatically.\n\n" +
                              "To open a save directory, select File -> Open Directory from the menu. Typically, it should be something like:\n" +
                              "C:\\Program Files (x86)\\Steam\\userdata\\<user_id>\\1757300\\remote";
                MessageBox.Show(this, msg, "Couldn't locate the live save directory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnDirtyChanged(object sender, EventArgs e)
        {
            OnStateChanged();
        }

        private void toolStripOpenDefaultDirectoryButton_Click(object sender, EventArgs e)
        {
            OpenDefaultDirectory();
        }

        private void openDefaultDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDefaultDirectory();
        }

        private void toolStripOpenDirectoryButton_Click(object sender, EventArgs e)
        {
            OpenDirectory();
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDirectory();
        }

        private void toolStripOpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolStripCloseButton_Click(object sender, EventArgs e)
        {
            CloseFileDir();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFileDir();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void runCLIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCLI();
        }

        private void toolStripRunCLIButton_Click(object sender, EventArgs e)
        {
            RunCLI();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        private void toolStripAboutButton_Click(object sender, EventArgs e)
        {
            ShowAboutBox();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripSaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void backupsToolStripMenuItemBackups_Click(object sender, EventArgs e)
        {
            ShowBackups();
        }

        private void toolStripButtonBackups_Click(object sender, EventArgs e)
        {
            ShowBackups();
        }

        private void logsToolStripMenuItemLogs_Click(object sender, EventArgs e)
        {
            ShowLogs();
        }

        private void toolStripButtonLog_Click(object sender, EventArgs e)
        {
            ShowLogs();
        }

        private void toolStripComboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnStateChanged();
        }

        private void colorblindModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            model_.Settings.Colorblind = colorblindModeToolStripMenuItem.Checked;
            OnStateChanged();
        }

        private bool CanEdit
        {
            get
            {
                return model_.IsOpen && !model_.IsMonitoring;
            }
        }

        private bool IsCheaterMode
        {
            get
            {
                return toolStripComboBoxMode.SelectedIndex == 1;
            }
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseFileDir();

            if (model_.IsOpen)
            {
                e.Cancel = true;
            }
        }

        private void onFormClosed(object sender, FormClosedEventArgs e)
        {
            model_.PeriodicInfoEvent -= OnPeriodicInfo;
            model_.IsMonitoringHook = null;
            model_?.Dispose();
        }

        private void OpenDefaultDirectory()
        {
            model_.Open(model_.DefaultSavePath);
            OnStateChanged();
        }

        private void OpenDirectory()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = model_.DefaultSavePath;
                dialog.Description = "Select the Jump Ship save directory";
                DialogResult result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Common.Safe(this, "opening a save directory", () => model_.Open(dialog.SelectedPath), model_.ActionLog);
                    OnStateChanged();
                }
            }
        }

        private void OpenFile()
        {
            string prompt = "To modify a live save, JumpSaves needs to open the Jump Ship save directory, not just one file.\n\n" +
                            "Changing a single save file from the game save directory may result in your changes being discarded by the game.\n\n" +
                            "Are you sure you want to open a single file?";
            if (MessageBox.Show(this, prompt, "Are you sure you want to open a single file?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                OpenDirectory();
                return;
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select a Jump Ship save file";
                dialog.Filter = "JumpSpace Save Files (*.bin;*.bin.bak1;*.bin.bak2)|*.bin;*.bin.bak1;*.bin.bak2|All Files (*.*)|*.*";
                dialog.FilterIndex = 0;
                dialog.Multiselect = false;
                dialog.CheckPathExists = true;
                dialog.CheckFileExists = true;
                DialogResult result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Common.Safe(this, "opening a save file", () => model_.Open(dialog.FileName), model_.ActionLog);
                    OnStateChanged();
                }
            }
        }

        private void Save()
        {
            model_.Save();
            OnStateChanged();
        }

        private void CloseFileDir()
        {
            if (model_.IsDirty)
            {
                string text = $"There are unsaved changes to {model_.Path}.\n\nDo you want to save them now?";
                DialogResult result = MessageBox.Show(this, text, "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Save();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            model_.Close();
            OnStateChanged();

            return;
        }

        private void RunCLI()
        {
            string path = model_.SaveEditor?.Path ?? model_.DefaultSavePath;

            CloseFileDir();

            if (!model_.IsOpen)
            {
                model_.RunCLI(path);
            }
        }

        private void ShowBackups()
        {
            if (backupsWindow_ == null || backupsWindow_.IsDisposed)
            {
                backupsWindow_ = new BackupsWindow(model_, OnStateChanged);
            }

            if (backupsWindow_.Visible)
            {
                backupsWindow_.Focus();
                backupsWindow_.BringToFront();
            }
            else
            {
                backupsWindow_.Show();
            }
        }

        private void ShowLogs()
        {
            if (logWindow_ == null || logWindow_.IsDisposed)
            {
                logWindow_ = new LogWindow(model_.ActionLog);
            }

            if (logWindow_.Visible)
            {
                logWindow_.Focus();
                logWindow_.BringToFront();
            }
            else
            {
                logWindow_.Show();
            }
        }

        private void ShowAboutBox()
        {
            string credits = $"JumpSaves, a Jump Space save file editor.\nVersion {Assembly.GetExecutingAssembly().GetName().Version}" +
                              "\n\nProgramming: gurudennis (gurudenis <at> gmail <dot> com)\nItem property cataloguing, beta testing: Snakeyes";
            MessageBox.Show(this, credits, "About JumpSaves", MessageBoxButtons.OK, MessageBoxIcon.Information);

#if DEBUG
            string res = model_.SaveEditor.StoredMajorItems.VerifyConstants();
            res += model_.SaveEditor.RecentMajorItems.VerifyConstants();
            res += model_.LibraryEditor.VerifyConstants();
            if (!string.IsNullOrEmpty(res))
            {
                MessageBox.Show("Validation failures:\n" + res, "Validation failures", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
#endif
        }

        private void DoTransfer(MajorItemList from, IReadOnlyList<JSL.MajorItemEditor> items)
        {
            bool fromLibrary = from.IsLibraryEditor;

            JSL.ConflictBehavior onConflict = JSL.ConflictBehavior.Error;
            if (!fromLibrary)
            {
                bool hasRepeats = items.Any((i) => model_.LibraryEditor.Contains(i));
                if (hasRepeats)
                {
                    string msgCount = items.Count == 1 ? "Item is" : "One or more of the items are";
                    string msg = $"{msgCount} already present in the Library. Overwrite?\n\n" +
                                 "Yes = overwrite conflict(s)\nNo = skip conflict(s)\nCancel = transfer nothing";
                    DialogResult decision = MessageBox.Show(this, msg, "Overwrite?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (decision == DialogResult.Yes)
                    {
                        onConflict = JSL.ConflictBehavior.Overwrite;
                    }
                    else if (decision == DialogResult.No)
                    {
                        onConflict = JSL.ConflictBehavior.Skip;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            List<string> failures = new List<string>();
            foreach (JSL.MajorItemEditor item in items)
            {
                try
                {
                    if (fromLibrary)
                    {
                        model_.TransferFromLibrary(item, editorMajorItemList.Editor);
                    }
                    else
                    {
                        model_.TransferToLibrary(item, onConflict);
                    }
                }
                catch (Exception ex)
                {
                    string name = item.Name ?? "(unknown)";
                    failures.Add($"{name}: {ex.Message}");
                    MajorItemList to = from == editorMajorItemList ? libraryMajorItemList : editorMajorItemList;
                    DoListLog(to, Model.ActionLog.Level.Error, $"Failed to transfer item \"{name}\" from {from.SelfDesignation} to {to.SelfDesignation}: {ex.Message}");
                }
            }

            OnStateChanged();

            if (from.IsLibraryEditor)
            {
                editorMajorItemList.Reload();
            }
            else
            {
                libraryMajorItemList.Reload();
            }

            if (failures.Count != 0)
            {
                string message = "Failed to transfer one or more items:\n\n";
                foreach (string failure in failures)
                {
                    message += failure;
                    message += "\n";
                }
                MessageBox.Show(this, message, "Failed to transfer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoListLog(MajorItemList origin, Model.ActionLog.Level level, string text)
        {
            Model.ActionLog.Origin actionOrigin = origin == editorMajorItemList ? Model.ActionLog.Origin.Editor : Model.ActionLog.Origin.Library;
            model_.ActionLog.AddEntry(actionOrigin, level, text);
        }

        private void OnPeriodicInfo(object sender, Model.PeriodicInfoArgs args)
        {
            OnStateChanged();

            if (args.HasAutoReopened)
            {
                model_.AutoAcquireIntoLibrary(libraryMajorItemList.IsInterestedInItem);
                libraryMajorItemList.Reload();
            }
        }

        private void OnStateChanged()
        {
            // Save editor
            if (model_.SaveEditor != null)
            {
                model_.SaveEditor.DirtyChanged += OnDirtyChanged;
            }

            // Menu and toolbar
            toolStripCloseButton.Visible = model_.IsOpen;
            toolStripSaveButton.Visible = model_.IsOpen && !model_.IsMonitoring;
            closeToolStripMenuItem.Visible = model_.IsOpen;
            saveToolStripMenuItem.Visible = model_.IsOpen;
            toolStripButtonBackups.Visible = model_.BackupStore != null;
            toolStripLabelDirty.Visible = model_.IsDirty;
            toolStripGameRunningLabel.Visible = model_.IsGameRunning && !model_.IsMonitoring;
            toolStripLabelMonitoring.Visible = model_.IsMonitoring;

            // Editor panel
            saveLabel.Text = string.IsNullOrEmpty(model_.Path) ? "(Open a save to display its contents here)" : model_.Path;
            editorMajorItemList.SaveEditor = model_.SaveEditor;
            editorMajorItemList.AllowCustomization = IsCheaterMode;
            editorMajorItemList.CanEdit = CanEdit;
            editorMajorItemList.CanTransfer = model_.IsOpen;
            editorMajorItemList.Colorblind = model_.Settings.Colorblind;
            editorResourceView.Editor = model_.SaveEditor?.Resources;
            editorResourceView.AllowCustomization = IsCheaterMode;
            editorResourceView.Enabled = CanEdit;
            editorSlotsView.Editor = model_.SaveEditor;
            editorSlotsView.AllowCustomization = IsCheaterMode;
            editorSlotsView.Enabled = CanEdit;

            // Library panel
            if (model_.LibraryEditor != null)
            {
                libraryMajorItemList.LibraryEditor = model_.LibraryEditor;
            }
            libraryMajorItemList.Enabled = libraryMajorItemList.LibraryEditor != null;
            libraryMajorItemList.AllowCustomization = IsCheaterMode;
            libraryMajorItemList.CanEdit = true;
            libraryMajorItemList.CanTransfer = CanEdit;
            libraryMajorItemList.Colorblind = model_.Settings.Colorblind;

            // Now that the game is started, warn about a dirty file
            if (!gameWasRunning_ && model_.IsGameRunning)
            {
                gameWasRunning_ = true;

                if (model_.IsGameSaveOpen && model_.IsDirty && !model_.IsMonitoring)
                {
                    string msg = "There are unsaved changes. The game will not be monitoring the save until these are discarded.\n" +
                                 "Saving is not recommended at this point, but you can close and re-open the file.";
                    MessageBox.Show(this, msg, "JumpSaves WARNING: Unsaved changes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (!model_.IsGameRunning)
            {
                gameWasRunning_ = false;
            }
        }

        private Model.Instance model_;
        private bool gameWasRunning_;
        private LogWindow logWindow_;
        private BackupsWindow backupsWindow_;
    }
}
