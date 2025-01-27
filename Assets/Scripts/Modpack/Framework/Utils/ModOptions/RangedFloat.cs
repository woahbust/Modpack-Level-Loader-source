using System;

namespace Modpack.Utils.ModOptions
{
	public class RangedFloat
	{
		public RangedFloat(float minimum, float value, float maximum)
		{
			this.min = minimum;
			this.max = maximum;
			this.val = value;
		}

		public RangedFloat(float minimum, float maximum)
		{
			this.min = minimum;
			this.max = maximum;
			this.val = minimum / 2f + maximum / 2f;
		}

		public static implicit operator float(RangedFloat from)
		{
			return from.val;
		}

		public float min;

		public float max;

		public float val;
	}
}
