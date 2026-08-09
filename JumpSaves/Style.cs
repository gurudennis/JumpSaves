using System.Drawing;

namespace JumpSaves
{
    internal static class Style
    {
        public static Color GetRarityColor(JSL.Rarity rarity, bool fore, bool colorblind)
        {
            if (colorblind && !fore)
            {
                return Color.LightGray;
            }

            if (rarity == JSL.Rarity.Common)
            {
                if (colorblind)
                {
                    return Color.FromArgb(6, 205, 6);
                }

                return fore ? Color.FromArgb(30, 112, 0) : Color.FromArgb(207, 232, 198);
            }
            else if (rarity == JSL.Rarity.Uncommon)
            {
                if (colorblind)
                {
                    return Color.FromArgb(16, 113, 235);
                }

                return fore ? Color.FromArgb(0, 67, 112) : Color.FromArgb(193, 216, 247);
            }
            else if (rarity == JSL.Rarity.Rare)
            {
                if (colorblind)
                {
                    return Color.FromArgb(55, 0, 126);
                }

                return fore ? Color.FromArgb(62, 6, 153) : Color.FromArgb(205, 187, 250);
            }
            else if (rarity == JSL.Rarity.Superior)
            {
                if (colorblind)
                {
                    return Color.FromArgb(200, 30, 12);
                }

                return fore ? Color.FromArgb(181, 59, 7) : Color.FromArgb(240, 175, 175);
            }

            return fore ? Color.Black : Color.White;
        }
    }
}
