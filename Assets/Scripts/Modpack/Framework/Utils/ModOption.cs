using System;
using System.Collections.Generic;
using Modpack.Utils.ModOptions;
using UnityEngine;

namespace Modpack.Utils
{
	[Serializable]
	public class ModOption
	{
		public enum Flags
		{
			NONE = -1,
			INT,
			RANGED_FLOAT,
			STRING,
			COLOR,
			BOOL,
			LIST,
			BUTTON
		}

		public ModOption()
		{
			this.title = "";
			this.index = 0;
			this.value = null;
			this.type = ModOption.Flags.NONE;
			this.editable = true;
			this.selectable = false;
			this.cosmetic = false;
			this.gameplay = true;
		}

		public int Count()
		{
			return ((List<ModOption>)this.value).Count;
		}

		public ModOption Add(ModOption option)
		{
			((List<ModOption>)this.value).Add(option);
			this.cosmetic |= option.cosmetic;
			this.gameplay |= option.gameplay;
			return this;
		}

		public ModOption UpdateSettings(ModOption modOption)
		{
			this.value = modOption.value;
			this.type = modOption.type;
			this.index = modOption.index;
			return this;
		}

		public ModOption DefaultSettings()
		{
			this.UpdateSettings(this.defaultOption);
			return this;
		}

		public ModOption Title(string t)
		{
			this.title = t;
			return this;
		}

		public ModOption Index(int i)
		{
			this.index = i;
			return this;
		}

		public ModOption Editable(bool b)
		{
			this.editable = b;
			if (this.type == ModOption.Flags.LIST)
			{
				foreach (ModOption modOption in ((List<ModOption>)this.value))
				{
					modOption.Editable(b);
				}
			}
			return this;
		}

		public ModOption Selectable(bool b)
		{
			this.selectable = b;
			return this;
		}

		public ModOption Cosmetic(bool b)
		{
			this.cosmetic = b;
			if (this.type == ModOption.Flags.LIST)
			{
				if (!b)
				{
					return this;
				}
				this.gameplay = false;
				using (List<ModOption>.Enumerator enumerator = ((List<ModOption>)this.value).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ModOption modOption = enumerator.Current;
						modOption.Cosmetic(true);
					}
					return this;
				}
			}
			this.gameplay = !b;
			return this;
		}

		public ModOption(List<ModOption> val)
		{
			this.value = val;
			this.type = ModOption.Flags.LIST;
			this.cosmetic = false;
			this.gameplay = false;
			foreach (ModOption modOption2 in val)
			{
				this.cosmetic |= modOption2.cosmetic;
				this.gameplay |= modOption2.gameplay;
			}
			this.defaultOption = new ModOption().UpdateSettings(this);
		}

		public static ModOption.Flags getFlag(object val)
		{
			if (val is int)
			{
				return ModOption.Flags.INT;
			}
			if (val is RangedFloat)
			{
				return ModOption.Flags.RANGED_FLOAT;
			}
			if (val is string)
			{
				return ModOption.Flags.STRING;
			}
			if (val is Color)
			{
				return ModOption.Flags.COLOR;
			}
			if (val is bool)
			{
				return ModOption.Flags.BOOL;
			}
			if (val is List<ModOption>)
			{
				return ModOption.Flags.LIST;
			}
			if (val is Button)
			{
				return ModOption.Flags.BUTTON;
			}
			return ModOption.Flags.NONE;
		}

		public ModOption(object val) : this()
		{
			this.value = val;
			this.type = ModOption.getFlag(val);
			this.defaultOption = new ModOption().UpdateSettings(this);
		}

		public ModOption(object val, string name) : this(val)
		{
			this.Title(name);
		}

		public object value;

		public ModOption.Flags type;

		public int index;

		public string title;

		public bool editable;

		public bool selectable;

		public bool cosmetic;

		public bool gameplay;

		public ModOption defaultOption;
	}
}
