using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFArray
    {
        internal IntPtr theArray;

        public CFArray() { }
        public CFArray(IntPtr Number) { theArray = Number; }

        /// <summary>
        /// Returns the number of values currently in an array
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            return CFLibrary.CFArrayGetCount(theArray);
        }

        /// <summary>
        /// Retrieves a value at a given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CFType getValue(int index)
        {
            if (index >= getCount())
                return null;

            return new CFType(CFLibrary.CFArrayGetValueAtIndex(theArray, index));

        }
    }
}
