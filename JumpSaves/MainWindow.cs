using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class MainWindow : Form
    {
        internal MainWindow(Model.Instance model)
        {
            model_ = model;
            model_.PeriodicInfoEvent += OnPeriodicInfo;

            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void toolStripOpenDirectoryButton_Click(object sender, EventArgs e)
        {
            OpenDirectory();
        }

        private void toolStripOpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDirectory();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void runCLIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCLI();
        }

        private void toolStripRunCLIButton_Click(object sender, EventArgs e)
        {
            RunCLI();
        }

        private void OpenDirectory()
        {
        }

        private void OpenFile()
        {
        }

        private void RunCLI()
        {
        }

        private void OnPeriodicInfo(object sender, Model.PeriodicInfoArgs args)
        {
        }

        private Model.Instance model_;
    }
}
