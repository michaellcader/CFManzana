using System;

namespace Plist
{
	public abstract class PlistObject<T> : PlistObjectBase
	{
		private T value;

		public PlistObject (T value)
		{
			Value = value;
		}

		public virtual T Value {
			get { return value; }
			set { this.value = value; }
		}
	}
}
