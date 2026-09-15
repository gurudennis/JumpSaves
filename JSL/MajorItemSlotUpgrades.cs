using System;
using System.Linq;

namespace JSL
{
    public interface IMajorItemSlotLimits
    {
        int DefaultMinSlotCount { get; }

        int DefaultMaxSlotCount { get; }

        int GetMaxMajorItemSlots(MajorItemCategory.Enum category);

        void SetMaxMajorItemSlots(MajorItemCategory.Enum category, int slots);
    }

    public class MajorItemSlotUpgrade : ArrayBasedObject
    {
        public MajorItemSlotUpgrade(object o, object[] parent) : base(o, parent)
        {
        }

        public string RawCategory
        {
            get
            {
                return GetPropertyStrict<string>(Index_RawCategory);
            }
            set
            {
                SetPropertyStrict(Index_RawCategory, value);
            }
        }

        public int SlotCount
        {
            get
            {
                if (Root.Length == 2) // Legacy behavior
                {
                    return GetPropertyStrict<int>(Index_LegacySlotCount);
                }

                return GetPropertyStrict<int>(Index_AdditionalSlotCount) + EffectiveBaseSlotCount;
            }
            set
            {
                if (Root.Length == 2) // Legacy behavior
                {
                    SetPropertyStrict(Index_LegacySlotCount, value);
                    return;
                }

                SetPropertyStrict(Index_AdditionalSlotCount, Math.Max(value - EffectiveBaseSlotCount, 0));
            }
        }

        private int EffectiveBaseSlotCount
        {
            get
            {
                return RawCategory == MajorItemCategory.GetRaw(MajorItemCategory.Enum.PlayerWeapons) ? 5 : 4;
            }
        }

        private const int Index_RawCategory = 0;
        private const int Index_LegacySlotCount = 1; // Essentially ignored in the latest game version (presumed 4 or 5, depending on category)
        private const int Index_AdditionalSlotCount = 2;
    }

    public class MajorItemSlotUpgrades : ArrayBasedObject, IMajorItemSlotLimits
    {
        public MajorItemSlotUpgrades(object o, object[] parent) : base(o, parent)
        {
            if (Root.Length != ExpectedElementCount)
            {
                throw new ArgumentException($"Expected {ExpectedElementCount} elements in MajorItemSlotUpgrades, found {Root.Length}.");
            }
        }

        public MajorItemSlotUpgrade[] Elements
        {
            get
            {
                return GetFixedElementsStrict<MajorItemSlotUpgrade>();
            }
        }

        public int DefaultMinSlotCount
        {
            get
            {
                return 4;
            }
        }

        public int DefaultMaxSlotCount
        {
            get
            {
                return 7;
            }
        }

        public int PracticalMaxSlotCount
        {
            get
            {
                return 12;
            }
        }

        public int GetMaxMajorItemSlots(MajorItemCategory.Enum category)
        {
            string raw = MajorItemCategory.GetRaw(category);
            MajorItemSlotUpgrade upgrade = Elements.FirstOrDefault((u) => u.RawCategory == raw);
            if (upgrade == null)
            {
                throw new Exception($"MajorItemSlotUpgrade entry not found for category \"{MajorItemCategory.GetTitle(category)}\"");
            }

            int count = upgrade.SlotCount;
            if (count == 0) // seen in Experimental (TODO: investigate)
            {
                count = DefaultMinSlotCount;
            }

            return count;
        }

        public void SetMaxMajorItemSlots(MajorItemCategory.Enum category, int slots)
        {
            if (slots < DefaultMinSlotCount || slots > PracticalMaxSlotCount)
            {
                throw new Exception($"Can't have fewer than {DefaultMinSlotCount} or more than {PracticalMaxSlotCount} slots per major item category.'");
            }

            string raw = MajorItemCategory.GetRaw(category);
            MajorItemSlotUpgrade upgrade = Elements.FirstOrDefault((u) => u.RawCategory == raw);
            if (upgrade == null)
            {
                throw new Exception($"MajorItemSlotUpgrade entry not found for category \"{MajorItemCategory.GetTitle(category)}\"");
            }

            upgrade.SlotCount = slots;
        }

        private const int ExpectedElementCount = 9;
    }
}
