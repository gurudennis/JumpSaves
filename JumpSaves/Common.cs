using System;
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
    }
}
