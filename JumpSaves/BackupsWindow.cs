using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JumpSaves
{
    public partial class BackupsWindow : Form
    {
        public BackupsWindow(Model.Instance model)
        {
            model_ = model;
            BackupStore.Changed += OnChanged;

            InitializeComponent();
        }

        public JSL.BackupStore BackupStore
        {
            get
            {
                return model_.BackupStore;
            }
        }

        private void BackupsWindow_Load(object sender, EventArgs e)
        {
            OnChanged(BackupStore, EventArgs.Empty);
        }

        private void BackupsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            BackupStore.Changed -= OnChanged;
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            string path = model_.SaveEditor?.Path;
            if (path == null)
            {
                MessageBox.Show("Open a save file in the main window to be able to create additional backups of it here.", "Open a save first", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                JSL.Backup backup = BackupStore.Add(path);
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Editor, Model.ActionLog.Level.Info,
                                         $"Created a new backup of save \"{path}\"");
            }
            catch (Exception ex)
            {
                string errMsg = $"Failed to create a new backup of save \"{path}\": {ex.Message}";
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Editor, Model.ActionLog.Level.Error, errMsg);
                MessageBox.Show(errMsg, "Failed to create backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            IList selected = list.SelectedObjects;
            if (selected.Count == 0)
            {
                MessageBox.Show("Select one or more backups and press this button to delete them", "No backups selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string submsg = selected.Count == 1 ? "this backup" : $"these {selected.Count} backups";
            string msg = $"Are you sure you want to permanently remove {submsg}?\n\nThis is irreversible.";
            if (MessageBox.Show(Parent, msg, $"Remove {submsg}?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            bool failed = false;
            foreach (object obj in selected)
            {
                JSL.Backup backup = (JSL.Backup)obj;
                string name = string.IsNullOrEmpty(backup.Name) ? "unnamed" : $"\"{backup.Name}\"";
                try
                {
                    BackupStore.Remove(backup);
                    model_.ActionLog.AddEntry(Model.ActionLog.Origin.Editor, Model.ActionLog.Level.Warning, $"Removed {name} backup dated {backup.Timestamp}");
                }
                catch (Exception ex)
                {
                    model_.ActionLog.AddEntry(Model.ActionLog.Origin.Editor, Model.ActionLog.Level.Error, $"Failed to remove {name} backup dated {backup.Timestamp}: {ex.Message}");
                    failed = true;
                }

                if (failed)
                {
                    MessageBox.Show("Failed to delete one or more of the selected backups. See log for more info.", "Failed to delete backup(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButtonBrowse_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = BackupStore.Path,
                UseShellExecute = true
            });
        }

        private void OnChanged(object sender, EventArgs e)
        {
            list.SetObjects(BackupStore.Backups);
        }

        private Model.Instance model_;
    }
}
