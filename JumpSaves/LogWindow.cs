using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class LogWindow : Form
    {
        public LogWindow(Model.ActionLog log)
        {
            Log = log;
            Log.Changed += OnChanged;

            InitializeComponent();
        }

        public Model.ActionLog Log { get; private set; }

        private void LogWindow_Load(object sender, EventArgs e)
        {
            OnChanged(Log, null);
        }

        private void LogWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.Changed -= OnChanged;
        }

        private void toolStripButtonBrowse_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Log.LocationPath,
                UseShellExecute = true
            });
        }

        private void OnChanged(object sender, EventArgs e)
        {
            list.SetObjects(Log.Entries);
        }
    }
}
