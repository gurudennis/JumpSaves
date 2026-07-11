using System;
using System.Collections.Generic;

namespace JSL
{
    public class Location // can't point to the root by design
    {
        public Location(string loc = null)
        {
            Sequence = new List<int>();
            if (!String.IsNullOrEmpty(loc))
            {
                string[] parts = loc.Split('/');
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int value) && value >= 0)
                    {
                        Sequence.Add(value);
                    }
                    else
                    {
                        Sequence.Clear();
                        return;
                    }
                }
            }
        }

        public Location(List<int> sequence)
        {
            Sequence = sequence != null ? new List<int>(sequence) : new List<int>();
        }

        public bool IsValid
        {
            get
            {
                return Sequence.Count != 0;
            }
        }

        public Location Parent
        {
            get
            {
                List<int> parentSequence = new List<int>();
                foreach (int child in Sequence)
                {
                    parentSequence.Add(child);
                }
                parentSequence.RemoveRange(parentSequence.Count - 1, 1);
                return new Location(parentSequence);
            }
        }

        public bool IsAtOrAfter(Location location)
        {
            if (location == null || !location.IsValid)
            {
                return true;
            }

            int count = Math.Min(Sequence.Count, location.Sequence.Count);
            for (int i = 0; i < count; ++i)
            {
                if (Sequence[i] > location.Sequence[i])
                {
                    return true;
                }
                else if (Sequence[i] < location.Sequence[i])
                {
                    return false;
                }
            }

            return Sequence.Count >= location.Sequence.Count;
        }

        public override string ToString()
        {
            return IsValid ? string.Join("/", Sequence) : "<invalid>";
        }

        public List<int> Sequence { get; private set; }
    }
}
