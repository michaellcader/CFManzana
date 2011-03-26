using System;

namespace Plist
{
	public class PlistBoolean : PlistObject<bool>
	{
		public PlistBoolean(bool value) : base(value)
		{
		}
		
		public override void Write (System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement (Value ? "true" : "false");
			writer.WriteEndElement ();
		}
	}
}
