using System;
using System.Diagnostics;
using System.Linq;
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

        private Model.ActionLog.Level Level
        {
            get
            {
                try
                {
                    return (Model.ActionLog.Level)toolStripComboBoxLevel.SelectedIndex;
                }
                catch
                {
                    return Model.ActionLog.Level.Info;
                }
            }
        }

        private void LogWindow_Load(object sender, EventArgs e)
        {
            toolStripComboBoxLevel.SelectedIndex = (int)Model.ActionLog.Level.Info; // also triggers OnChanged
        }

        private void LogWindow_FormClosing(object sender, FormClosingEventArgs e)
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

        private void toolStripComboBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnChanged(Log, null);
        }

        private void OnChanged(object sender, EventArgs e)
        {
            if (IsDisposed)
            {
                return;
            }

            list.SetObjects(Log.Entries.Where((entry) => entry.Level >= Level));
        }
    }
}
