using System.Collections.Generic;
using System.Diagnostics;

namespace JSL
{
    // Rarity as it pertains to items (not ingots)
    public enum Rarity
    {
        Unknown = -1,
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
            for (int i = 1; i < Count; ++i)
            {
                byRaw_[All[i].Raw] = (Enum)i;
            }
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
            return byRaw_.TryGetValue(raw, out Enum e) ? e : Enum.Unknown;
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

        private static Dictionary<string, Enum> byRaw_ = new Dictionary<string, Enum>();
    }

    public static class ShipModuleType
    {
        static ShipModuleType()
        {
            Debug.Assert(All.Length == Count);
            for (int i = 1; i < Count; ++i)
            {
                byRaw_[All[i].Raw] = (Enum)i;
            }
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
            C_AdditionalShotPercentPerMag,
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
            C_FasterShieldRecharge,
            C_LowerShieldBreakChance,
            C_ShorterShieldDowntime,
            C_ReactorCapacity,
            C_AdditionalShotsPerMag,
            C_SearChanceOnHit,
            C_RuptureProjectile,
            C_RadiationAfterCorrosion,
            C_DisruptionAfterEMP,
            C_ShieldCapacity,
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
            return byRaw_.TryGetValue(raw, out Enum e) ? e : Enum.Unknown;
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
            new EnumInfo { Title = "Unknown",                   Abbr = "Unk", Raw = null,                               PotencyCount = 0, Kind = ModuleKind.Unknown, Purpose = MajorItemPurpose.Unknown    },
            // ModuleKind.Feature:
            new EnumInfo { Title = "Reload speed",              Abbr = "Rel", Raw = "9009aa4df2ad3a04ba4dea5518e1d611", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Magazine size",             Abbr = "Mag", Raw = "cafc9599b386ee84a890c2c760b62f5e", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Damage",                    Abbr = "Dmg", Raw = "df5b391e9981fdd47af8f2f6e74a9fd9", PotencyCount = 1, Kind = ModuleKind.Feature, Purpose = MajorItemPurpose.Weapon     },
            // ModuleKind.Custom:
            new EnumInfo { Title = "Reduced materia cost",      Abbr = "Mat", Raw = "072b30aa0e26c5c49b7c3ca156c62282", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Corrosion chance on hit",   Abbr = "Cor", Raw = "bb680a7ce4769fa4396b560d36435371", PotencyCount = 2, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Additional projectiles",    Abbr = "Frg", Raw = "b4c71cf386f6f3a42aaf7fe311eb202c", PotencyCount = 3, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Additional shot % per mag", Abbr = "Mag", Raw = "09dd872497cec754bba28c7616b8810f", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "EMP chance on hit",         Abbr = "EMP", Raw = "c70b3f3ddb76d4141bc113b651ffbfdd", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Breach chance on hit",      Abbr = "Bre", Raw = "e60458dfaa15275469527dab6ddf9b02", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Corrosion projectile",      Abbr = "Cor", Raw = "bf3cecfa0702aa04e95d109144f21ed1", PotencyCount = 0, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Chance to chain enemies",   Abbr = "Chn", Raw = "fc4cf93ada70dcf4c910cad5faa5c9a9", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Increase Rupture damage",   Abbr = "Rup", Raw = "6a8561321dfb64d4789ae84a0cda11d4", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Breach causes Rupture",     Abbr = "Rup", Raw = "3fd4cd1ef685c464bab96af82388c2ac", PotencyCount = 3, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Virus chance on hit",       Abbr = "Vir", Raw = "72a1c39fe91bdee4da863bee2afa6db8", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Virus causes EMP",          Abbr = "EMP", Raw = "5c15051e787622d43bae554502e4a052", PotencyCount = 2, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Max speed",                 Abbr = "Spd", Raw = "2f006f1878bb9ed4992f73cf87ac953d", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Max boost",                 Abbr = "Bst", Raw = "b9d81f3aea38f3d47848701fcbdd521b", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Turn rate",                 Abbr = "Trn", Raw = "7aa83416608d90e41b0c0ecfab3e869a", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Acceleration",              Abbr = "Acc", Raw = "782a431317dc9a64f90e4c20edfe0e04", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Faster shield recharge",    Abbr = "Rec", Raw = "8dacbec9a6bac2947ab3eaecb8a020f5", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Shield     },
            new EnumInfo { Title = "Lower shield break chance", Abbr = "Brk", Raw = "76dc2ea8251eafe4b9752f33a15f90df", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Shield     },
            new EnumInfo { Title = "Shorter shield downtime",   Abbr = "Dwn", Raw = "34f7c993f959fbe459777c90eab95189", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Shield     },
            new EnumInfo { Title = "Reactor capacity",          Abbr = "Cap", Raw = "987886742b6da2740b8f64922c59b0b1", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Additional shots per mag",  Abbr = "Mag", Raw = "fe1243281c445474da91a86cd378d640", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Sear chance on hit",        Abbr = "Sea", Raw = "318504b5400b90d4c81cc63c64baf0ac", PotencyCount = 3, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Rupture projectile",        Abbr = "Sea", Raw = "e8ca44d9362a2e143917f65180369a6d", PotencyCount = 0, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Radiation after corrosion", Abbr = "Sea", Raw = "66ad1d1ada3da75479cef1a83a739aae", PotencyCount = 3, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Disruption after EMP",      Abbr = "Sea", Raw = "a5331df01eb1f1147b27f8cb852c82ca", PotencyCount = 2, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Shield capacity",           Abbr = "Cap", Raw = "1a0f06c6024f86d48bdc56076def5dd3", PotencyCount = 1, Kind = ModuleKind.Custom,  Purpose = MajorItemPurpose.Shield     },
        };

        private static Dictionary<string, Enum> byRaw_ = new Dictionary<string, Enum>();
    }

    public static class PlayerWeaponModuleType
    {
        static PlayerWeaponModuleType()
        {
            Debug.Assert(All.Length == Count);
            for (int i = 1; i < Count; ++i)
            {
                byRaw_[All[i].Raw] = (Enum)i;
            }
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
            C_Damage,
            C_LastShotDamage,
            C_MagazineSize,
            C_FinalShotAdditionalProjectiles,
            C_SearChanceOnHitNearby,
            C_SearAfterDamage,
            C_KillsIncreaseMeleeDamage,
            C_FireRate,
            C_CorrosionAfterDamage,
            C_DamageAfterCorrosion,
            C_SearChanceOnHit,
            C_DamageAfterEMP,
            C_DamagePerStatusEffect,
            C_MeleeEMPAfterDeflect,
            C_MeleeHealsAfterDeflect,
            C_LastShotDamagePercent,
            C_RandomStatusEffectOnHit,
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
            return byRaw_.TryGetValue(raw, out Enum e) ? e : Enum.Unknown;
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
            new EnumInfo { Title = "Unknown",                      Abbr = "Unk", Raw = null,                               PotencyCount = 0, Kind = ModuleKind.Unknown },
            // ModuleKind.Feature:
            new EnumInfo { Title = "Damage",                       Abbr = "Dmg", Raw = "8fecf9fa19f5d0748bc7e5794d2e2e93", PotencyCount = 1, Kind = ModuleKind.Feature },
            new EnumInfo { Title = "Fire rate",                    Abbr = "RoF", Raw = "bb21cfa6fd5a9364c99ef22d8d4ea38f", PotencyCount = 1, Kind = ModuleKind.Feature },
            new EnumInfo { Title = "Magazine capacity",            Abbr = "Mag", Raw = "68fbcdf863d097c498e0ffb0ec1d4cba", PotencyCount = 1, Kind = ModuleKind.Feature },
            new EnumInfo { Title = "Reload speed",                 Abbr = "Rel", Raw = "13cec6085efd0a342a6ecfef9f5aa2da", PotencyCount = 1, Kind = ModuleKind.Feature },
            // ModuleKind.Custom:
            new EnumInfo { Title = "Reload speed",                 Abbr = "Rel", Raw = "e755854780143a1419bec0445b46f072", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Mag size but less damage",     Abbr = "Mag", Raw = "425315a74cef11542b1c3fcb07d8d934", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Consecutive crit damage",      Abbr = "Crt", Raw = "676bc98f5878db4409a11c68b7e2bd59", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Chance to chain enemies",      Abbr = "Chn", Raw = "5ef2583dd770e944e84c8ab47e12b50f", PotencyCount = 3, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Breach chance on hit",         Abbr = "Bre", Raw = "13cec6085efd0a342a6ecfef9f5aa2da", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Crits return ammo",            Abbr = "Amm", Raw = "87320adbde1d5a6448effd71fcba76b3", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Rupture after damage",         Abbr = "Rup", Raw = "3d864ddf5a372664ebcad63c686b0ceb", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage but lower fire rate",   Abbr = "Dmg", Raw = "4e049bf738976824ebdfd81f9fd34796", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage but lower mag size",    Abbr = "Dmg", Raw = "4ec6f7c2d7fa7734792a2db069c46d9c", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "EMP on crit",                  Abbr = "EMP", Raw = "749c007fa80faf840ade9af6e8d7584f", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Rupture chance on hit",        Abbr = "Dup", Raw = "15748cbe448ab444c990e086e31fea7b", PotencyCount = 3, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Kills restore health",         Abbr = "Hel", Raw = "71a93d1d5c620864c93e796b0218a90b", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Corrosion chance on hit",      Abbr = "Cor", Raw = "3ad789a04e7a05e42affc0e22f0a309a", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Additional projectiles",       Abbr = "Fle", Raw = "f893ca79dbff4b448bdf21715d8e6d1d", PotencyCount = 3, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage",                       Abbr = "Dmg", Raw = "4ab19bf28b038444d904a1032d398ac7", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Last shot does more damage",   Abbr = "Dmg", Raw = "490125f0fc44ef04090900b7eaaafeec", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Magazine size",                Abbr = "Mag", Raw = "989e818fc6f5c4a47ac1dbd682f96e94", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Final shot frag",              Abbr = "Frg", Raw = "61121570f54f3904b9bef6f39801d39c", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Sear chance on hit nearby",    Abbr = "Sea", Raw = "55561444a0d58ff4a980836c77f1905c", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Sear after damage",            Abbr = "Sea", Raw = "dfb61796e77bac246b8cb6786ef49297", PotencyCount = 3, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Kills increase melee",         Abbr = "Mel", Raw = "3c3ea2f8998b5474d91965dea056b2ac", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Fire rate",                    Abbr = "RoF", Raw = "767a4ec9ef9c7cf40be160743b4e6cf3", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Corrosion after damage",       Abbr = "Cor", Raw = "e41e01652b2bcc5479b2742f0e062ba6", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage after corrosion",       Abbr = "Dam", Raw = "284e8453cfde3ee40b5316f7cf4ade45", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Sear chance on hit",           Abbr = "Sea", Raw = "17064961c8b22fb40a445885cf78adbe", PotencyCount = 3, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage after EMP",             Abbr = "Dmg", Raw = "bbcd941bbc8563748afe7dc0bad13e96", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Damage per status effect",     Abbr = "Dmg", Raw = "57f9ebc23ef4bbc4aa9be052158b6f63", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Melee EMP after deflect",      Abbr = "Mel", Raw = "44d9c736d6730d2409de0f0051e08c70", PotencyCount = 2, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Melee heals after deflect",    Abbr = "Hel", Raw = "6da7dc2db0f033242a5b6990bae55e15", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Last shot does % more damage", Abbr = "Dmg", Raw = "e8b4433b1b4cf5648b5a7c236fdf4507", PotencyCount = 1, Kind = ModuleKind.Custom  },
            new EnumInfo { Title = "Random status effect on hit",  Abbr = "Ran", Raw = "f004308cf75411f4897f0286397ca2c2", PotencyCount = 1, Kind = ModuleKind.Custom  },
        };

        private static Dictionary<string, Enum> byRaw_ = new Dictionary<string, Enum>();
    }

    public static class ModuleType
    {
        public static string GetTitleFromRaw(string raw)
        {
            ShipModuleType.Enum shipEnum = ShipModuleType.FromRaw(raw);
            if (shipEnum != ShipModuleType.Enum.Unknown)
            {
                return ShipModuleType.GetTitle(shipEnum);
            }

            PlayerWeaponModuleType.Enum playerEnum = PlayerWeaponModuleType.FromRaw(raw);
            if (playerEnum != PlayerWeaponModuleType.Enum.Unknown)
            {
                return PlayerWeaponModuleType.GetTitle(playerEnum);
            }

            return null;
        }

        public static string GetAbbreviationFromRaw(string raw)
        {
            ShipModuleType.Enum shipEnum = ShipModuleType.FromRaw(raw);
            if (shipEnum != ShipModuleType.Enum.Unknown)
            {
                return ShipModuleType.GetAbbreviation(shipEnum);
            }

            PlayerWeaponModuleType.Enum playerEnum = PlayerWeaponModuleType.FromRaw(raw);
            if (playerEnum != PlayerWeaponModuleType.Enum.Unknown)
            {
                return PlayerWeaponModuleType.GetAbbreviation(playerEnum);
            }

            return null;
        }

        public static ModuleKind GetKindFromRaw(string raw)
        {
            ShipModuleType.Enum shipEnum = ShipModuleType.FromRaw(raw);
            if (shipEnum != ShipModuleType.Enum.Unknown)
            {
                return ShipModuleType.GetKind(shipEnum);
            }

            PlayerWeaponModuleType.Enum playerEnum = PlayerWeaponModuleType.FromRaw(raw);
            if (playerEnum != PlayerWeaponModuleType.Enum.Unknown)
            {
                return PlayerWeaponModuleType.GetKind(playerEnum);
            }

            return ModuleKind.Unknown;
        }
    }

    static class MajorItemType
    {
        static MajorItemType()
        {
            Debug.Assert(All.Length == Count);
            for (int i = 1; i < Count; ++i)
            {
                byRaw_[All[i].Raw] = (Enum)i;
            }
        }

        public enum Enum
        {
            Unknown,
            // MajorItemCategory.Enum.PlayerWeapons
            PW_Bulldog,
            PW_Halberd,
            PW_Stinger,
            PW_Sideclip,
            PW_MAW,
            PW_Javelin,
            PW_Ironbelt,
            PW_HeatBlade,
            PW_Wrench,
            PW_Crowbar,
            // MajorItemCategory.Enum.Multiturrets
            MT_AssaultTurrets,
            MT_MiningLasers,
            MT_FlakLauncherTurrets,
            MT_GatlingTurrets,
            // MajorItemCategory.Enum.PilotCannons
            PC_FragmentationCannon,
            PC_ReaverRotaryCannon,
            PC_BoltAccelerator,
            PC_DisruptorLaser,
            // MajorItemCategory.Enum.SpecialWeapons
            SW_BurstShield,
            SW_VulcanRotaryCannon,
            SW_ThunderburstHeavyCannon,
            SW_Railgun,
            SW_MissileLauncher,
            SW_TargetingModule,
            // MajorItemCategory.Enum.Engines
            EN_DriftPhaseEngine,
            EN_NitroPulseEngine,
            EN_MassEjectorEngine,
            EN_MicroplasmaEngine,
            // MajorItemCategory.Enum.ShieldGenerators
            SH_SkirmisherShield,
            SH_FighterShield,
            SH_FortressShield,
            // MajorItemCategory.Enum.Sensors
            SE_SectorScanner,
            SE_SupplyUplinkUnit,
            SE_VectorTargetingModule,
            // MajorItemCategory.Enum.Reactors
            RE_NullWaveReactor,
            RE_SplitReactor,
            RE_MateriaScatterReactor,
            RE_SolidStateReactor,
            // MajorItemCategory.Enum.AuxGenerators
            AG_BioFissionGenerator,
            AG_MateriaShiftGenerator,
            AG_NullTensionGenerator,
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

        public static MajorItemCategory.Enum GetCategory(Enum e)
        {
            return All[(int)e].Category;
        }

        public static MajorItemPurpose GetPurpose(Enum e)
        {
            return All[(int)e].Purpose;
        }

        public static Enum FromTitle(string title, MajorItemCategory.Enum category = MajorItemCategory.Enum.Unknown)
        {
            for (int i = 1; i < All.Length; ++i)
            {
                if (All[i].Title.ToLower() == title.ToLower())
                {
                    if (category != MajorItemCategory.Enum.Unknown && All[i].Category != category)
                    {
                        return Enum.Unknown;
                    }

                    return (Enum)i;
                }
            }

            return Enum.Unknown;
        }

        public static Enum FromRaw(string raw, MajorItemCategory.Enum category = MajorItemCategory.Enum.Unknown)
        {
            if (byRaw_.TryGetValue(raw, out Enum e))
            {
                if (category == MajorItemCategory.Enum.Unknown || GetCategory(e) == category)
                {
                    return e;
                }
            }

            return Enum.Unknown;
        }

        private struct EnumInfo
        {
            public string Title;
            public string Raw;
            public MajorItemCategory.Enum Category;
            public MajorItemPurpose Purpose;
        }

        private static EnumInfo[] All = new EnumInfo[]
        {
            new EnumInfo { Title = "Unknown",                   Raw = null,                               Category = MajorItemCategory.Enum.Unknown,          Purpose = MajorItemPurpose.Unknown    },
            // MajorItemCategory.Enum.PlayerWeapons
            new EnumInfo { Title = "Bulldog-SA7 (AR)",          Raw = "b5b14acbf52842b4f90002bd4e9d1391", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "VSR Halberd (AR)",          Raw = "dd2afc39b34322c4381b6a3ed3e5988a", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Stinger MP-75 (SMG)",       Raw = "b90b477e483f6cc4aa9a48f33e4400e3", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "CX-305 Sideclip (SMG)",     Raw = "ea1f1e6ac3c88fe469708ce10b5e451a", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "MAW-23 (Shotgun)",          Raw = "11d2d7e9f079ed4499c9591d8d68c7a7", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "SR.99 Javelin (Sniper)",    Raw = "2cbd789d647fa9a4d96f462001a99c91", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Ironbelt (LMG)",            Raw = "55b2b5f6d20d4eb4ab9ef216e5237928", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Heat Blade (Melee)",        Raw = "046383b13f53ad144805f5dca98b4b86", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Wrench (Melee)",            Raw = "a52219e07db611248800766fe8d53744", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Crowbar (Melee)",           Raw = "f077b42a85cfb7f4bb0d55729c4fc5c0", Category = MajorItemCategory.Enum.PlayerWeapons,    Purpose = MajorItemPurpose.Weapon     },
            // MajorItemCategory.Enum.Multiturrets
            new EnumInfo { Title = "Assault Turrets",           Raw = "558ac570efdfbe64390ab32c39d45ff3", Category = MajorItemCategory.Enum.Multiturrets,     Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Mining Lasers",             Raw = "e2b646f5b39d9994a929c62d32635793", Category = MajorItemCategory.Enum.Multiturrets,     Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Flak Launcher Turrets",     Raw = "b98c1f15ee8b58343acf4f62ddaa93ab", Category = MajorItemCategory.Enum.Multiturrets,     Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Gatling Turrets",           Raw = "5e4082467be3c344698f724e74c6660a", Category = MajorItemCategory.Enum.Multiturrets,     Purpose = MajorItemPurpose.Weapon     },
            // MajorItemCategory.Enum.PilotCannons
            new EnumInfo { Title = "Fragmentation Cannon",      Raw = "c7e0f6d13e18db440b37a42470f42744", Category = MajorItemCategory.Enum.PilotCannons,     Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Reaver Rotary Cannon",      Raw = "8c32f9e4ff293894582ceb6b831c40af", Category = MajorItemCategory.Enum.PilotCannons,     Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Bolt Accelerator",          Raw = "d151b8914ed56d74985d345a118f9a5e", Category = MajorItemCategory.Enum.PilotCannons,     Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Disruptor Laser",           Raw = "c3346b9a547672948a2fe02ea2bb5de5", Category = MajorItemCategory.Enum.PilotCannons,     Purpose = MajorItemPurpose.Weapon     },
            // MajorItemCategory.Enum.SpecialWeapons
            new EnumInfo { Title = "Burst Shield",              Raw = "e1d2c08495890004e9ebb0ca7fe7c5a1", Category = MajorItemCategory.Enum.SpecialWeapons,   Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Vulcan Rotary Cannon",      Raw = "3290a7dbea83de5488cca77e44cae0a8", Category = MajorItemCategory.Enum.SpecialWeapons,   Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Thunderburst Heavy Cannon", Raw = "889c471a3fab57d44b28f2592be5d7f8", Category = MajorItemCategory.Enum.SpecialWeapons,   Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Railgun",                   Raw = "d593e671947518544ab6dcd2deef3e2e", Category = MajorItemCategory.Enum.SpecialWeapons,   Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Missile Launcher",          Raw = "15bebeb1e9273af4fb7f4213452a6c58", Category = MajorItemCategory.Enum.SpecialWeapons,   Purpose = MajorItemPurpose.Weapon     },
            new EnumInfo { Title = "Targeting Module",          Raw = "10b36cf783993154cbcf8679744cd900", Category = MajorItemCategory.Enum.SpecialWeapons,   Purpose = MajorItemPurpose.Weapon     },
            // MajorItemCategory.Enum.Engines
            new EnumInfo { Title = "Drift Phase Engine",        Raw = "f1302af5a63e825478b7fb9f953401aa", Category = MajorItemCategory.Enum.Engines,          Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Nitro Pulse Engine",        Raw = "fb2bbc3f13c228a44ba53f020e1df249", Category = MajorItemCategory.Enum.Engines,          Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Mass Ejector Engine",       Raw = "dde01f5723d9b0047abb3d223f5541cd", Category = MajorItemCategory.Enum.Engines,          Purpose = MajorItemPurpose.Propulsion },
            new EnumInfo { Title = "Microplasma Engine",        Raw = "4524f4ac1fa8cb34dbff50f8c5c14927", Category = MajorItemCategory.Enum.Engines,          Purpose = MajorItemPurpose.Propulsion },
            // MajorItemCategory.Enum.ShieldGenerators
            new EnumInfo { Title = "Skirmisher Shield",         Raw = "352d8b4d1f0f38249b5a88c19a11caf3", Category = MajorItemCategory.Enum.ShieldGenerators, Purpose = MajorItemPurpose.Shield     },
            new EnumInfo { Title = "Fighter Shield",            Raw = "554d65de54698714c9f5d2f73bc33261", Category = MajorItemCategory.Enum.ShieldGenerators, Purpose = MajorItemPurpose.Shield     },
            new EnumInfo { Title = "Fortress Shield",           Raw = "7a2d7e2dbbb33f742a1c885667377425", Category = MajorItemCategory.Enum.ShieldGenerators, Purpose = MajorItemPurpose.Shield     },
            // MajorItemCategory.Enum.Sensors
            new EnumInfo { Title = "Sector Scanner",            Raw = "3ee2f96ed55e8de4898d4f922eba8f66", Category = MajorItemCategory.Enum.Sensors,          Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Supply Uplink Unit",        Raw = "eac8acd058989964ab7a07cf7de03fa5", Category = MajorItemCategory.Enum.Sensors,          Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Vector Targeting Module",   Raw = "d4b4eb9b7856fce4e9c1bb3a78c9807e", Category = MajorItemCategory.Enum.Sensors,          Purpose = MajorItemPurpose.General    },
            // MajorItemCategory.Enum.Reactors
            new EnumInfo { Title = "Null Wave Reactor",         Raw = "95c51a0f21d28ba459a43bbf3dbc0790", Category = MajorItemCategory.Enum.Reactors,         Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Split Reactor",             Raw = "65e495611fd9233419832053f8ce55bd", Category = MajorItemCategory.Enum.Reactors,         Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Materia Scatter Reactor",   Raw = "f400f9acd1731b64a87b69ce40517971", Category = MajorItemCategory.Enum.Reactors,         Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Solid State Reactor",       Raw = "85b241f7f2fa6654e9c522519358d1bc", Category = MajorItemCategory.Enum.Reactors,         Purpose = MajorItemPurpose.General    },
            // MajorItemCategory.Enum.AuxGenerators
            new EnumInfo { Title = "Bio Fission Generator",     Raw = "2c60b00a88d34b546865a36738fabc2a", Category = MajorItemCategory.Enum.AuxGenerators,    Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Materia Shift Generator",   Raw = "4bfcb457279ce824e8fd7989760d9252", Category = MajorItemCategory.Enum.AuxGenerators,    Purpose = MajorItemPurpose.General    },
            new EnumInfo { Title = "Null Tension Generator",    Raw = "8cbe912f6e885134680a52935cca96a5", Category = MajorItemCategory.Enum.AuxGenerators,    Purpose = MajorItemPurpose.General    },
        };

        private static Dictionary<string, Enum> byRaw_ = new Dictionary<string, Enum>();
    }
}
