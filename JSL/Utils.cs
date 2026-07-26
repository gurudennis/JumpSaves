using System.Linq;

namespace JSL
{
    public static class Utils
    {
        public static string MakeSafeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            return new string(name.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());
        }
    }
}
