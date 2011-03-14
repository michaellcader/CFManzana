using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFArray
    {
        internal IntPtr theArray;

        public CFArray() { }
        public CFArray(IntPtr Number){theArray = Number;}

        /// <summary>
        /// Returns the number of values currently in an array
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            return CFLibrary.CFArrayGetCount(theArray);
        }
    }
}
