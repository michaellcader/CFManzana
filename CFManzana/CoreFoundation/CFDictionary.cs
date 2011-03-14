using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreFoundation
{
    public class CFDictionary 
    {
        internal IntPtr theDict;
        public CFDictionary() { }

        public CFDictionary(IntPtr dictionary){this.theDict = dictionary;}

        public CFDictionary(string dict)
        {
            
        }
       
        public IntPtr getDataValue(string value)
        {
            
            return CFLibrary.CFDictionaryGetValue(theDict, new CFString(value).ToIntPtr());            
        }
        /// <summary>
        /// Returns the number of key-value pairs in a dictionary
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return CFLibrary.CFDictionaryGetCount(theDict);            
        }

        /// <summary>
        /// Returns the Keys and Values from the Dictionary as a C-String
        /// </summary>
        public override string ToString()
        {
            IntPtr[] keys = new IntPtr[Length()];
            IntPtr[] values = new IntPtr[Length()];
            CFLibrary.CFDictionaryGetKeysAndValues(theDict, keys, values);
            string[] keyz = new string[Length()];
            string[] valuez = new string[Length()];
            int i;
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < Length(); i++)
            {
                if (i == 0)
                {
                    sb.Append("<dict>\n");                    
                }
                
                keyz[i] = new CFString(keys[i]).ToString();
                sb.Append("<key>" + keyz[i] + "</key>" + "\n");
                valuez[i] = parseValue(values[i]);
                sb.Append(valuez[i] + "\n");

                if (i == Length() - 1)
                {
                    sb.Append("\n</dict");                         
                }

            }            
            return sb.ToString();
        }

        private string parseValue(IntPtr typeRef)
        {
            if (typeRef == IntPtr.Zero)
                return string.Empty;
            string desc = new CFType(typeRef).CFObjectType();
            switch (desc)
            {
                case "CFBoolean":
                    return new CFBoolean(typeRef).ToBoolean().ToString();
                case "CFData":
                    return Convert.ToBase64String(new CFData(typeRef).ToByteArray());
            }
            return null;
        }
    }
}
