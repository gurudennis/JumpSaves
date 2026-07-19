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
            // ModuleKind.Feature:
            F_ReloadSpeed,
            F_MagazineSize,
            F_Damage,
            // ModuleKind.Custom:
            C_ReduceMateriaCost,
            C_CorrosionChanceOnHit,
            C_AdditionalProjectiles,
            C_AdditionalShotsPerMag,
            C_EMPChanceOnHit,
            C_BreachChanceOnHit,
            C_CorrosionProjectile,
            C_ChanceToChainEnemies,
            C_IncreaseRuptureDamage,
            C_BreachCausesRupture,
            C_VirusChanceOnHit,
            C_VirusCausesEMP,
            C_Speed,
            C_Boost,
            C_TurnRate,
            C_Acceleration,
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

        public static string GetAbbreviation(Enum e)
        {
            return All[(int)e].Abbr;
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
            public string Abbr;
            public string Raw;
            public int PotencyCount;
            public ModuleKind Kind;
            public MajorItemPurpose Purpose;
        }

        private static EnumInfo[] All = new EnumInfo[]
        {
            new EnumInfo { Title = "Unknown",                  Abbr = "Unk", Raw = null,                               PotencyCount = 0, Kind = ModuleKind.Unknown, Purpose = MajorItemPurpose.Unknown    },
            // ModuleKind.Feature:
            new EnumInfo { Title = "Reload speed",             Abbr = "Rel", Raw = "9009aa4df2ad3a04ba4dea5518e1d611", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Magazine size",            Abbr = "Mag", Raw = "cafc9599b386ee84a890c2c760b62f5e", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Damage",                   Abbr = "Dmg", Raw = "df5b391e9981fdd47af8f2f6e74a9fd9", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            // ModuleKind.Custom:
            new EnumInfo { Title = "Reduced materia cost",     Abbr = "Mat", Raw = "072b30aa0e26c5c49b7c3ca156c62282", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Corrosion chance on hit",  Abbr = "Cor", Raw = "bb680a7ce4769fa4396b560d36435371", PotencyCount = 2, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Additional projectiles",   Abbr = "Frg", Raw = "b4c71cf386f6f3a42aaf7fe311eb202c", PotencyCount = 3, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Additional shots per mag", Abbr = "Mag", Raw = "09dd872497cec754bba28c7616b8810f", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "EMP chance on hit",        Abbr = "EMP", Raw = "c70b3f3ddb76d4141bc113b651ffbfdd", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Breach chance on hit",     Abbr = "Bre", Raw = "e60458dfaa15275469527dab6ddf9b02", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Corrosion projectile",     Abbr = "Cor", Raw = "bf3cecfa0702aa04e95d109144f21ed1", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Chance to chain enemies",  Abbr = "Chn", Raw = "fc4cf93ada70dcf4c910cad5faa5c9a9", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Increase Rupture damage",  Abbr = "Rup", Raw = "6a8561321dfb64d4789ae84a0cda11d4", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Breach causes Rupture",    Abbr = "Rup", Raw = "3fd4cd1ef685c464bab96af82388c2ac", PotencyCount = 3, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Virus chance on hit",      Abbr = "Vir", Raw = "72a1c39fe91bdee4da863bee2afa6db8", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Virus causes EMP",         Abbr = "EMP", Raw = "5c15051e787622d43bae554502e4a052", PotencyCount = 2, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Max speed",                Abbr = "Spd", Raw = "2f006f1878bb9ed4992f73cf87ac953d", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Max boost",                Abbr = "Bst", Raw = "2f006f1878bb9ed4992f73cf87ac953d", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Turn rate",                Abbr = "Trn", Raw = "2f006f1878bb9ed4992f73cf87ac953d", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Acceleration",             Abbr = "Acc", Raw = "2f006f1878bb9ed4992f73cf87ac953d", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
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
            // ModuleKind.Feature:
            F_Damage,
            F_FireRate,
            F_MagazineSize,
            F_ReloadSpeed,
            // ModuleKind.Custom:
            C_ReloadSpeed,
            C_MagazineSizeButLessDamage,
            C_ConsecutiveCrits,
            C_ChanceToChainEnemies,
            C_BreachChanceOnHit,
            C_CritsReturnAmmo,
            C_RuptureAfterDamage,
            C_DamageButLowerFireRate,
            C_DamageButLowerMagSize,
            C_EMPOnCrit,
            C_RuptureChanceOnHit,
            C_KillsRestoreHealth,
            C_CorrosionChanceOnHit,
            C_AdditionalProjectiles,
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

        public static string GetAbbreviation(Enum e)
        {
            return All[(int)e].Abbr;
        }

        public static MajorItemPurpose GetPurpose(Enum e)
        {
            return MajorItemPurpose.Weapon;
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
            public string Abbr;
            public string Raw;
            public int PotencyCount;
            public ModuleKind Kind;
        }

        private static EnumInfo[] All = new EnumInfo[]
        {
            new EnumInfo { Title = "Unknown",                    Abbr = "Unk", Raw = null,                               PotencyCount = 0, Kind = ModuleKind.Unknown },
            // ModuleKind.Feature:
            new EnumInfo { Title = "Damage",                     Abbr = "Dmg", Raw = "8fecf9fa19f5d0748bc7e5794d2e2e93", PotencyCount = 1, Kind = ModuleKind.Feature },
            new EnumInfo { Title = "Fire rate",                  Abbr = "RoF", Raw = "bb21cfa6fd5a9364c99ef22d8d4ea38f", PotencyCount = 1, Kind = ModuleKind.Feature },
            new EnumInfo { Title = "Magazine capacity",          Abbr = "Mag", Raw = "68fbcdf863d097c498e0ffb0ec1d4cba", PotencyCount = 1, Kind = ModuleKind.Feature },
            new EnumInfo { Title = "Reload speed",               Abbr = "Rel", Raw = "13cec6085efd0a342a6ecfef9f5aa2da", PotencyCount = 1, Kind = ModuleKind.Feature },
            // ModuleKind.Custom:
            new EnumInfo { Title = "Reload speed",               Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Mag size but less damage",   Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Consecutive crit damage",    Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Chance to chain enemies",    Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Breach chance on hit",       Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Crits return ammo",          Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Rupture after damage",       Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage but lower fire rate", Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage but lower mag size",  Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "EMP on crit",                Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Rupture chance on hit",      Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Kills restore health",       Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Corrosion chance on hit",    Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Additional projectiles",     Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
        };
    }

    static class PlayerWeaponType
    {
        static PlayerWeaponType()
        {
            Debug.Assert(All.Length == Count);
        }

        public enum Enum
        {
            Unknown,
            Bulldog,
            Halberd,
            Stinger,
            Sideclip,
            MAW,
            Javelin,
            Ironbelt,
            HeatBlade,
            Wrench,
            Crowbar,
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
            return MajorItemPurpose.Weapon;
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
        }

        private static EnumInfo[] All = new EnumInfo[]
        {
            new EnumInfo { Title = "Unknown",                Raw = null,                              },
            new EnumInfo { Title = "Bulldog-SA7 (AR)",       Raw = "b5b14acbf52842b4f90002bd4e9d1391" },
            new EnumInfo { Title = "VSR Halberd (AR)",       Raw = "dd2afc39b34322c4381b6a3ed3e5988a" },
            new EnumInfo { Title = "Stinger MP-75 (SMG)",    Raw = "b90b477e483f6cc4aa9a48f33e4400e3" },
            new EnumInfo { Title = "CX-305 Sideclip (SMG)",  Raw = "ea1f1e6ac3c88fe469708ce10b5e451a" },
            new EnumInfo { Title = "MAW-23 (Shotgun)",       Raw = "11d2d7e9f079ed4499c9591d8d68c7a7" },
            new EnumInfo { Title = "SR.99 Javelin (Sniper)", Raw = "2cbd789d647fa9a4d96f462001a99c91" },
            new EnumInfo { Title = "Ironbelt (LMG)",         Raw = "fd51532d25c4d4841b1c439708726682" },
            new EnumInfo { Title = "Heat Blade (Melee)",     Raw = "046383b13f53ad144805f5dca98b4b86" },
            new EnumInfo { Title = "Wrench (Melee)",         Raw = "a52219e07db611248800766fe8d53744" },
            new EnumInfo { Title = "Crowbar (Melee)",        Raw = "f077b42a85cfb7f4bb0d55729c4fc5c0" },
        };
    }
}
