using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
            OnJumpSpaceGameStateChanged();
            OpenDefaultDirectory();
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

        private void onFormClosed(object sender, FormClosedEventArgs e)
        {
            model_?.Dispose();
        }

        private void OpenDefaultDirectory()
        {
            model_.Open(model_.DefaultPath);
            OnOpenCloseStateChanged();
        }

        private void OpenDirectory()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = model_.DefaultPath;
                dialog.Description = "Select the Jump Ship save directory";
                DialogResult result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    model_.Open(dialog.SelectedPath);
                    OnOpenCloseStateChanged();
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
                    model_.Open(dialog.FileName);
                    OnOpenCloseStateChanged();
                }
            }
        }

        private void CloseFileDir()
        {
            if (model_.IsDirty)
            {
                if (MessageBox.Show("There are unsaved changes. Are you sure you want to close?") != DialogResult.OK)
                {
                    return;
                }
            }

            model_.Close();
            OnOpenCloseStateChanged();
        }

        private void RunCLI()
        {
            CloseFileDir();

            if (!model_.IsOpen)
            {
                model_.RunCLI();
            }
        }

        private void OnPeriodicInfo(object sender, Model.PeriodicInfoArgs args)
        {
            OnJumpSpaceGameStateChanged();
        }

        private void OnOpenCloseStateChanged()
        {
            // ...
        }

        private void OnJumpSpaceGameStateChanged()
        {
            toolStripGameRunningLabel.Visible = model_.IsGameRunning;
        }

        private Model.Instance model_;

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseFileDir();

            if (model_.IsOpen)
            {
                e.Cancel = true;
            }
        }
    }
}
