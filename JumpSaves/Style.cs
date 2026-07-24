using System.Drawing;

namespace JumpSaves
{
    internal static class Style
    {
        public static Color GetRarityColor(JSL.Rarity rarity, bool fore)
        {
            if (rarity == JSL.Rarity.Common)
            {
                return fore ? Color.FromArgb(30, 112, 0) : Color.FromArgb(207, 232, 198);
            }
            else if (rarity == JSL.Rarity.Uncommon)
            {
                return fore ? Color.FromArgb(0, 67, 112) : Color.FromArgb(193, 216, 247);
            }
            else if (rarity == JSL.Rarity.Rare)
            {
                return fore ? Color.FromArgb(62, 6, 153) : Color.FromArgb(205, 187, 250);
            }
            else if (rarity == JSL.Rarity.Superior)
            {
                return fore ? Color.FromArgb(181, 59, 7) : Color.FromArgb(240, 175, 175);
            }

            return fore ? Color.Black : Color.White;
        }
    }
}
