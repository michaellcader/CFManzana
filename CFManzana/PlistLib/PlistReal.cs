using System;

namespace Plist
{
	public class PlistReal : PlistObject<double>
	{
		public PlistReal (double value) : base(value)
		{
		}

		public override void Write (System.Xml.XmlWriter writer)
		{
			writer.WriteElementString ("real", Value.ToString ());
		}
	}
}
