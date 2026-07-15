using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSL
{
    public class Library
    {
        public Library(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}
