using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Plist;
using System.Xml;

namespace CoreFoundation
{
    public class CFDictionary 
    {
        internal IntPtr theDict;
        public CFDictionary() { }

        public CFDictionary(IntPtr dictionary){this.theDict = dictionary;}

        unsafe public CFDictionary(string[] keys,string[] values)
        {
            IntPtr[] keyz = new IntPtr[keys.Length];
            IntPtr[] valuez = new IntPtr[values.Length];
            int i;
            for (i = 0; i < keys.Length; i++)
            {
                keyz[i] = new CFString(keys[i]).ToIntPtr();
                valuez[i] = new CFString(values[i]).ToIntPtr();
            }
                        
            theDict = CFLibrary.CFDictionaryCreate(IntPtr.Zero,keyz,valuez,keys.Length,IntPtr.Zero,IntPtr.Zero);            
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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Length(); i++)
            {                
                keyz[i] = new CFString(keys[i]).ToString();
                sb.Append(keyz[i] + "\n");
                valuez[i] = parseValue(values[i]);                
                sb.Append(valuez[i] + "\n"); 
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
                case "CFArray":
                    return null; //refer to cf tinyumbrella
                case "CFBoolean":
                    return new CFBoolean(typeRef).ToBoolean().ToString();                
                case "CFString":
                    return new CFString(typeRef).ToString();
                case "CFNumber":
                    return new CFNumber(typeRef).ToString();
                case "CFData":
                    return Convert.ToBase64String(new CFData(typeRef).ToByteArray());
                case "CFDictionary":
                    return null;
                case "CFDate":
                    return null;
                default:
                    throw new ArgumentException("Unknown Value:" + desc + ":" + typeRef);

            }            
        }
    }

            
        public delegate IntPtr CFDictionaryRetainCallBack(IntPtr allocator, IntPtr type); 
        public delegate IntPtr CFDictionaryReleaseCallBack(IntPtr allocator, IntPtr type);
        public delegate IntPtr CFDictionaryCopyDescriptionCallBack(IntPtr type);
        public delegate IntPtr CFDictionaryEqualCallBack(IntPtr type1,IntPtr type2);
        public delegate int CFDictionaryHashCallBack(IntPtr type);
        public struct CFDictionaryKeyCallBacks
        {
            CFIndex version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
            CFDictionaryHashCallBack hash;
        };

        public struct CFDictionaryValueCallBacks
        {
            CFIndex version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
        };

        

    
}
