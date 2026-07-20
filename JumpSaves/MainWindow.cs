using JSL;
using System;
using System.Collections.Generic;
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

            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Text = $"JumpSaves {Assembly.GetExecutingAssembly().GetName().Version} (beta)";

            toolStripComboBoxMode.SelectedIndex = 0;

            editorMajorItemList.MaybeDirty += OnEditorMajorItemList_MaybeDirty;
            editorResourceView.MaybeDirty += OnEditorResourceView_MaybeDirty;

            OnStateChanged();
            OpenDefaultDirectory();
        }

        private void OnEditorResourceView_MaybeDirty(object sender, EventArgs e)
        {
            OnStateChanged();
        }

        private void OnEditorMajorItemList_MaybeDirty(object sender, EventArgs e)
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

        private void toolStripComboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                    Common.Safe(this, "opening a save directory", () => model_.Open(dialog.SelectedPath));
                    OnStateChanged();
                }
            }
        }

        private void OpenFile()
        {
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
                    Common.Safe(this, "opening a save file", () => model_.Open(dialog.FileName));
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

        private void ShowAboutBox()
        {
            string credits = $"JumpSaves, a Jump Space save file editor.\nVersion {Assembly.GetExecutingAssembly().GetName().Version} (beta)" +
                              "\n\nProgramming: gurudennis (gurudenis <at> gmail.com)\nBeta testing: Snakeyes";
            MessageBox.Show(this, credits, "About JumpSaves", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DoTransfer(MajorItemList from, IReadOnlyList<JSL.MajorItemEditor> items)
        {
            List<string> failures = new List<string>();
            foreach (JSL.MajorItemEditor item in items)
            {
                try
                {
                    if (from.IsLibraryEditor)
                    {
                        model_.TransferFromLibrary(item, editorMajorItemList.Editor);
                    }
                    else
                    {
                        model_.TransferToLibrary(item, ConflictBehavior.Error);
                    }
                }
                catch (Exception ex)
                {
                    string name = item.Name ?? "(unknown)";
                    failures.Add($"{name}: {ex.Message}");

#if DEBUG
                    throw;
#endif
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

        private void OnPeriodicInfo(object sender, Model.PeriodicInfoArgs args)
        {
            OnStateChanged();

            if (args.HasReopened)
            {
                model_.AutoAcquireIntoLibrary(libraryMajorItemList.IsInterestedInItem);
                libraryMajorItemList.Reload();
            }
        }

        private void OnStateChanged()
        {
            // Menu and toolbar
            toolStripCloseButton.Visible = model_.IsOpen;
            toolStripSaveButton.Visible = model_.IsOpen && !model_.IsMonitoring;
            closeToolStripMenuItem.Visible = model_.IsOpen;
            saveToolStripMenuItem.Visible = model_.IsOpen;
            toolStripLabelDirty.Visible = model_.IsDirty;
            toolStripGameRunningLabel.Visible = model_.IsGameRunning && !model_.IsMonitoring;
            toolStripLabelMonitoring.Visible = model_.IsMonitoring;

            // Editor panel
            saveLabel.Text = string.IsNullOrEmpty(model_.Path) ? "(Open a save to display its contents here)" : model_.Path;
            editorMajorItemList.TransferAction = DoTransfer;
            editorMajorItemList.SaveEditor = model_.SaveEditor;
            editorMajorItemList.AllowCustomization = IsCheaterMode;
            editorMajorItemList.CanEdit = CanEdit;
            editorMajorItemList.CanTransfer = model_.IsOpen;
            editorResourceView.Editor = model_.SaveEditor?.Resources;
            editorResourceView.AllowCustomization = IsCheaterMode;
            editorResourceView.Enabled = CanEdit;

            // Library panel
            if (model_.LibraryEditor != null)
            {
                libraryMajorItemList.LibraryEditor = model_.LibraryEditor;
            }
            libraryMajorItemList.TransferAction = DoTransfer;
            libraryMajorItemList.Enabled = libraryMajorItemList.LibraryEditor != null;
            libraryMajorItemList.AllowCustomization = IsCheaterMode;
            libraryMajorItemList.CanEdit = true;
            libraryMajorItemList.CanTransfer = CanEdit;

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
    }
}
