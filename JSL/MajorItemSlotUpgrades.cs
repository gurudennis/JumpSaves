using System;
using System.Linq;

namespace JSL
{
    public interface IMajorItemSlotLimits
    {
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
                return GetPropertyStrict<int>(Index_SlotCount);
            }
            set
            {
                SetPropertyStrict(Index_SlotCount, value);
            }
        }

        private const int Index_RawCategory = 0;
        private const int Index_SlotCount = 0;
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

        public int GetMaxMajorItemSlots(MajorItemCategory.Enum category)
        {
            string raw = MajorItemCategory.GetRaw(category);
            MajorItemSlotUpgrade upgrade = Elements.FirstOrDefault((u) => u.RawCategory == raw);
            if (upgrade == null)
            {
                throw new Exception($"MajorItemSlotUpgrade entry not found for category \"{MajorItemCategory.GetTitle(category)}\"");
            }

            return upgrade.SlotCount;
        }

        public void SetMaxMajorItemSlots(MajorItemCategory.Enum category, int slots)
        {
            if (slots < 2 || slots > 6)
            {
                throw new Exception($"Can't have fewer than 2 or more than 6 slots per major item category.'");
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
