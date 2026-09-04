using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JumpSaves
{
    public partial class BackupsWindow : Form
    {
        public BackupsWindow(Model.Instance model, Action onStateChange)
        {
            model_ = model;
            onStateChange_ = onStateChange;

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
                JSL.Backup backup = BackupStore.Add(path, model_.IsExperimental, "User created");
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

        private void toolStripButtonRestore_Click(object sender, EventArgs e)
        {
            if (model_.IsDirty)
            {
                MessageBox.Show(this, "As a safety measure, you can't restore a backup while you have unsaved changes.", "Unsaved changes detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (model_.IsGameRunning)
            {
                MessageBox.Show(this, "As a safety measure, you can't restore a backup while the game is running.", "Game is running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IList selected = list.SelectedObjects;
            if (selected.Count != 1)
            {
                MessageBox.Show(this, "Select one backup and press this button to restore it to the original location from which it was taken.",
                                "Exactly one backup must be selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            JSL.Backup backup = (JSL.Backup)list.SelectedObject;
            string msg = $"This will retore backup \"{backup.Title}\", overwriting the original location from which it was taken:\n" +
                         $"\"{backup.OriginalPath}\".\n" +
                         "A new backup will be created just prior, to ensure that you can undo this action if you want to.\n\n" +
                         "Are you sure you want to proceed?";
            if (MessageBox.Show(this, msg, "Restore this backup?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                BackupStore.Add(backup.OriginalPath, model_.IsExperimental, "Before restoring");
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Editor, Model.ActionLog.Level.Info,
                                          $"Created a new backup of save \"{backup.OriginalPath}\" before restoring another backup to that location.");
            }
            catch (Exception ex)
            {
                string errMsg = $"Backup restoration aborted due to failure to create a new backup of save \"{backup.OriginalPath}\" before restoring that location: { ex.Message}";
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Application, Model.ActionLog.Level.Warning, errMsg);
                MessageBox.Show(this, msg, "Failed to restore backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                backup.Restore();
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Application, Model.ActionLog.Level.Warning,
                                          $"Restored backup \"{backup.Title}\" to \"{backup.OriginalPath}\".");
            }
            catch (Exception ex)
            {
                string errMsg = $"Failed to restore backup \"{backup.Title}\" to \"{backup.OriginalPath}\": {ex.Message}";
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Application, Model.ActionLog.Level.Warning, errMsg);
                MessageBox.Show(this, msg, "Failed to restore backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string path = model_.Path;
            bool experimental = model_.IsExperimental;
            model_.Close();
            onStateChange_(); // needed to persuade the editor list to fully reopen on the next refresh
            model_.Open(path, experimental);
            onStateChange_();
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
        private Action onStateChange_;
    }
}
