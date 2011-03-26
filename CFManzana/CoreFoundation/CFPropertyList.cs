using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFoundation
{
    public class CFPropertyList
    {
        internal IntPtr theList;

        public CFPropertyList() { }
        public CFPropertyList(IntPtr PropertyList) { this.theList = PropertyList; }
        /// <summary>
        /// Creates an XML-string representation of the specified property list
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Encoding.UTF8.GetString(new CFData(CFLibrary.CFPropertyListCreateXMLData(IntPtr.Zero, theList)).ToByteArray());
        }                
    }
}
