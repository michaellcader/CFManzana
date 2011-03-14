using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFoundation
{
    public class CFType
    {
        public const int _CFString=7;
        public const int _CFDictionary=17;
        public const int _CFArray=18;
        public const int _CFData=19;
        public const int _CFBoolean=21;
        public const int _CFNumber=22;
        public const int _CFError=27;
        public const int _CFUUID=31;
        
        internal IntPtr typeRef;

        public CFType() { }
        
        public CFType(IntPtr handle){this.typeRef = handle;}
        
        /// <summary>
        /// Returns the unique identifier of an opaque type to which a CoreFoundation object belongs
        /// </summary>
        /// <returns></returns>
        public int GetTypeID()
        {
            return CFLibrary.CFGetTypeID(typeRef);
        }
        /// <summary>
        /// Returns a textual description of a CoreFoundation object
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return new CFString(CFLibrary.CFCopyDescription(typeRef)).ToString();
        }

        /// <summary>
        /// Returns the CFObject type as a string
        /// </summary>
        /// <returns></returns>
        
        public string CFObjectType()
        {            
            switch (CFLibrary.CFGetTypeID(typeRef))
            {
                case _CFString:
                    return "CFString";
                case _CFDictionary:
                    return "CFDictionary";
                case _CFArray:
                    return "CFArray";
                case _CFData:
                    return "CFData";
                case _CFBoolean:
                    return "CFBoolean";
                case _CFNumber:
                    return "CFNumber";
                case _CFError:
                    return "CFError";
                case _CFUUID:
                    return "CFUUID";
            }
            return String.Empty;  
        }
    }
}
