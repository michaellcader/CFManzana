using System;

namespace Plist
{


	public class PlistDate : PlistObject<DateTime>
	{
		public PlistDate (DateTime value) : base(value)
		{
		}

		static readonly string plistDateFormat = "yyyy-mm-ddThh:mm:ssZ";
		
		public override void Write (System.Xml.XmlWriter writer)
		{
			writer.WriteElementString ("date", Value.ToUniversalTime ().ToString (plistDateFormat));
		}
	}
}
