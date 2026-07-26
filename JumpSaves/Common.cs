using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JumpSaves
{
    internal class Common
    {
        public static bool Safe(IWin32Window owner, string description, Action action, Model.ActionLog log)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                log.AddEntry(Model.ActionLog.Origin.Editor, Model.ActionLog.Level.Error, $"Error while {description}: {ex}");
                MessageBox.Show(owner, $"Error encountered while {description}.\n\n{ex.Message}", "JumpSaves error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        public static void OpenFolderAndSelect(string path)
        {
            if (!System.IO.File.Exists(path) && !System.IO.Directory.Exists(path))
            {
                throw new Exception($"File or directory {path} doesn't exist");
            }

            Process.Start("explorer.exe", $"/select,\"{path}\"");
        }
    }
}
