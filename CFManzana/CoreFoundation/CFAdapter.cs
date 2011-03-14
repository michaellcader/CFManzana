using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFAdapter
    {
        internal IntPtr theAdapter;

        public CFAdapter() { }
        public CFAdapter(IntPtr Number){theAdapter = Number;}
    }
}
