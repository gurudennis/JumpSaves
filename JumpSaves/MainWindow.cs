using System;
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
            CloseFileDir();

            if (!model_.IsOpen)
            {
                model_.RunCLI();
            }
        }

        private void ShowAboutBox()
        {
            string credits = "JumpSaves, a Jump Space save file editor.\n\nProgramming: gurudennis (gurudenis <at> gmail.com)\nBeta testing: Snakeyes";
            MessageBox.Show(this, credits, "About JumpSaves", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnPeriodicInfo(object sender, Model.PeriodicInfoArgs args)
        {
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            // Menu and toolbar
            toolStripCloseButton.Visible = model_.IsOpen;
            toolStripSaveButton.Visible = model_.IsOpen;
            closeToolStripMenuItem.Visible = model_.IsOpen;
            saveToolStripMenuItem.Visible = model_.IsOpen;
            toolStripLabelDirty.Visible = model_.IsDirty;
            toolStripGameRunningLabel.Visible = model_.IsGameRunning;

            // Editor panel
            saveLabel.Text = string.IsNullOrEmpty(model_.Path) ? "(Open a save to display its contents here)" : model_.Path;
            editorMajorItemList.Editor = model_.Editor;
            editorMajorItemList.AllowCustomization = IsCheaterMode;
            editorMajorItemList.Enabled = CanEdit;
            editorResourceView.Editor = model_.Editor?.Resources;
            editorResourceView.AllowCustomization = IsCheaterMode;
            editorResourceView.Enabled = CanEdit;

            // Library panel
            libraryMajorItemList.AllowCustomization = IsCheaterMode;
        }

        private Model.Instance model_;
    }
}
