using System;

namespace Plist
{
	public class PlistString : PlistObject<string>
	{
		public PlistString(string value) : base(value)
		{
		}
		
		public override void Write (System.Xml.XmlWriter writer)
		{
			writer.WriteElementString ("string", Value);
		}
	}
}
