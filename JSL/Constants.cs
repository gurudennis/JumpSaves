using System.Diagnostics;

namespace JSL
{
    public enum Rarity
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Superior = 3,
    }

    public static class MajorItemCategory
    {
        public enum Enum
        {
            Unknown,
            PlayerWeapons,
            Multiturrets,
            PilotCannons,
            SpecialWeapons,
            Engines,
            ShieldGenerators,
            Sensors,
            Reactors,
            AuxGenerators
        }

        public static int Count
        {
            get
            {
                return ((int)Enum.AuxGenerators) + 1;
            }
        }

        public static string GetTitle(Enum e)
        {
            Debug.Assert(Titles.Length == Count);
            return Titles[(int)e];
        }

        public static string GetRaw(Enum e)
        {
            Debug.Assert(Raw.Length == Count);
            return Raw[(int)e];
        }

        public static Enum FromTitle(string title)
        {
            Debug.Assert(Titles.Length == Count);
            for (int i = 1; i < Raw.Length; ++i)
            {
                if (Titles[i].ToLower() == title.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        public static Enum FromRaw(string raw)
        {
            Debug.Assert(Raw.Length == Count);
            for (int i = 1; i < Raw.Length; ++i)
            {
                if (Raw[i].ToLower() == raw.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        private static string[] Titles = new string[]
        {
            "Unknown",
            "Player Weapons",
            "Multiturrets",
            "Pilot Cannons",
            "Special Weapons",
            "Engines",
            "Shield Generators",
            "Sensors",
            "Reactors",
            "Aux. Generators"
        };

        private static string[] Raw = new string[]
        {
            null,
            "fd51532d25c4d4841b1c439708726682",
            "110603b5e382aec438ef983ddde55f81",
            "00ef3c858516b02498fcf9e8ee5497de", 
            "9eb5cd7261a6dba439db975e7e05d069",
            "d7c2724a0c49930438f6d6ed3da628ba",
            "1292627bbf531c84ba2c56e6e55af6d3",
            "6ef31e2e90989364d8e2d4958615b299",
            "2546892240b110847b64e524e9bd1d39",
            "0c28f786865a0b142926742a182c0011"
        };
    }
}
