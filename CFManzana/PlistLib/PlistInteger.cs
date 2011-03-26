using System;

namespace Plist
{
	public class PlistInteger : PlistObject<int>
	{
		public PlistInteger (int value) : base(value)
		{
		}

		public override void Write (System.Xml.XmlWriter writer)
		{
			writer.WriteElementString ("integer", Value.ToString ());
		}
	}
}
