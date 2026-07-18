using System.Diagnostics;

namespace JSL
{
    // Rarity as it pertains to items (not ingots)
    public enum Rarity
    {
        Common = 0,   // green
        Uncommon = 1, // blue
        Rare = 2,     // purple
        Superior = 3, // orange
    }

    // Representation of the overall purpose of an item
    public enum MajorItemPurpose
    {
        Unknown,
        Weapon,
        Propulsion,
        Shield,
        General
    }

    public enum ModuleKind
    {
        Unknown,
        Feature, // "Upgradeable Feature" (only on MajorItemPurpose.Weapon)
        Custom   // "Custom Module"
    }

    // Categories of major items where each represents a group of related slots in the Blueprints menu
    public static class MajorItemCategory
    {
        static MajorItemCategory()
        {
            Debug.Assert(All.Length == Count);
        }

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
            AuxGenerators,
            __COUNT__
        }

        public static int Count
        {
            get
            {
                return (int)Enum.__COUNT__;
            }
        }

        public static string GetTitle(Enum e)
        {
            return All[(int)e].Title;
        }

        public static string GetRaw(Enum e)
        {
            return All[(int)e].Raw;
        }

        public static MajorItemPurpose GetPurpose(Enum e)
        {
            return All[(int)e].Purpose;
        }

        public static Enum FromTitle(string title)
        {   
            for (int i = 1; i < All.Length; ++i)
            {
                if (All[i].Title.ToLower() == title.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        public static Enum FromRaw(string raw)
        {
            for (int i = 1; i < All.Length; ++i)
            {
                if (All[i].Raw.ToLower() == raw.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        public static bool IsWithShip(Enum e)
        {
            return e != Enum.PlayerWeapons && e != Enum.Unknown;
        }

        public static bool IsWithPlayerWeapon(Enum e)
        {
            return e == Enum.PlayerWeapons;
        }

        private struct EnumInfo
        {
            public string Title;
            public string Raw;
            public MajorItemPurpose Purpose;
        }

        private static EnumInfo[] All = new EnumInfo[]
        {
            new EnumInfo { Title = "Unknown",           Raw = null,                               Purpose = MajorItemPurpose.Unknown    },
            new EnumInfo { Title = "Player Weapons",    Raw = "fd51532d25c4d4841b1c439708726682", Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Multiturrets",      Raw = "110603b5e382aec438ef983ddde55f81", Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Pilot Cannons",     Raw = "00ef3c858516b02498fcf9e8ee5497de", Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Special Weapons",   Raw = "9eb5cd7261a6dba439db975e7e05d069", Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Engines",           Raw = "d7c2724a0c49930438f6d6ed3da628ba", Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Shield Generators", Raw = "1292627bbf531c84ba2c56e6e55af6d3", Purpose = MajorItemPurpose.Shield     },
            new EnumInfo { Title = "Sensors",           Raw = "6ef31e2e90989364d8e2d4958615b299", Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Reactors",          Raw = "2546892240b110847b64e524e9bd1d39", Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Aux. Generators",   Raw = "0c28f786865a0b142926742a182c0011", Purpose = MajorItemPurpose.General    },
        };
    }

    public static class ShipModuleType
    {
        static ShipModuleType()
        {
            Debug.Assert(All.Length == Count);
        }

        public enum Enum
        {
            Unknown,
            F_ReloadSpeed,
            // ...
            C_ReduceMateriaCost,
            C_CorrosionChanceOnHit,
            C_AdditionalProjectiles,
            C_AdditionalShotsPerMag,
            C_EMPChanceOnHit,
            // ...
            __COUNT__
        }

        public static int Count
        {
            get
            {
                return (int)Enum.__COUNT__;
            }
        }

        public static string GetTitle(Enum e)
        {
            return All[(int)e].Title;
        }

        public static string GetRaw(Enum e)
        {
            return All[(int)e].Raw;
        }

        public static MajorItemPurpose GetPurpose(Enum e)
        {
            return All[(int)e].Purpose;
        }

        public static ModuleKind GetKind(Enum e)
        {
            return All[(int)e].Kind;
        }

        public static int GetPotencyCount(Enum e)
        {
            return All[(int)e].PotencyCount;
        }

        public static Enum FromTitle(string title)
        {
            for (int i = 1; i < All.Length; ++i)
            {
                if (All[i].Title.ToLower() == title.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        public static Enum FromRaw(string raw)
        {
            for (int i = 1; i < All.Length; ++i)
            {
                if (All[i].Raw.ToLower() == raw.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        private struct EnumInfo
        {
            public string Title;
            public string Raw;
            public int PotencyCount;
            public ModuleKind Kind;
            public MajorItemPurpose Purpose;
        }

        private static EnumInfo[] All = new EnumInfo[]
        {
            new EnumInfo { Title = "Unknown",                  Raw = null,                               PotencyCount = 0, Kind = ModuleKind.Unknown, Purpose = MajorItemPurpose.Unknown    },
            new EnumInfo { Title = "Reload speed",             Raw = "9009aa4df2ad3a04ba4dea5518e1d611", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            // ...
            new EnumInfo { Title = "Reduced materia cost",     Raw = "072b30aa0e26c5c49b7c3ca156c62282", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Corrosion chance on hit",  Raw = "bb680a7ce4769fa4396b560d36435371", PotencyCount = 2, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Additional projectiles",   Raw = "b4c71cf386f6f3a42aaf7fe311eb202c", PotencyCount = 3, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Additional shots per mag", Raw = "09dd872497cec754bba28c7616b8810f", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "EMP chance on hit",        Raw = "c70b3f3ddb76d4141bc113b651ffbfdd", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            // ...
        };
    }

    public static class PlayerWeaponModuleType
    {
        static PlayerWeaponModuleType()
        {
            Debug.Assert(All.Length == Count);
        }

        public enum Enum
        {
            Unknown,
            F_Damage,
            // ...
            C_ReloadSpeed,
            // ...
            __COUNT__
        }

        public static int Count
        {
            get
            {
                return (int)Enum.__COUNT__;
            }
        }

        public static string GetTitle(Enum e)
        {
            return All[(int)e].Title;
        }

        public static string GetRaw(Enum e)
        {
            return All[(int)e].Raw;
        }

        public static MajorItemPurpose GetPurpose(Enum e)
        {
            return All[(int)e].Purpose;
        }

        public static ModuleKind GetKind(Enum e)
        {
            return All[(int)e].Kind;
        }

        public static int GetPotencyCount(Enum e)
        {
            return All[(int)e].PotencyCount;
        }

        public static Enum FromTitle(string title)
        {
            for (int i = 1; i < All.Length; ++i)
            {
                if (All[i].Title.ToLower() == title.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        public static Enum FromRaw(string raw)
        {
            for (int i = 1; i < All.Length; ++i)
            {
                if (All[i].Raw.ToLower() == raw.ToLower())
                {
                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        private struct EnumInfo
        {
            public string Title;
            public string Raw;
            public int PotencyCount;
            public ModuleKind Kind;
            public MajorItemPurpose Purpose;
        }

        private static EnumInfo[] All = new EnumInfo[]
        {
            new EnumInfo { Title = "Unknown",                  Raw = null,                               PotencyCount = 0, Kind = ModuleKind.Unknown, Purpose = MajorItemPurpose.Unknown    },
            new EnumInfo { Title = "Damage",                   Raw = "8fecf9fa19f5d0748bc7e5794d2e2e93", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            // ...
            new EnumInfo { Title = "Reload speed",             Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            // ...
        };
    }
}
