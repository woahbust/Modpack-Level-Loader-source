using System;
using System.Collections.Generic;
using Modpack.Utils;
using Modpack.Utils.ModOptions;
using UnityEngine;

namespace Modpack.Mods
{
	public class ModExample2 : IMod
	{
		public bool Load()
		{
			return true;
		}

		public ModExample2()
		{
			this.name = "Example Mod";
			this.version = "1.0";
			this.author = "Author";
			this.options = new List<ModOption>
			{
				new ModOption(new RangedFloat(0.5f, 1f, 3f), "amplitude"),
				new ModOption(new RangedFloat(0.5f, 1f, 5f), "wavelength"),
				new ModOption(new RangedFloat(0f, 0.25f, 2f), "offset")
			};
			this.cosmetic = false;
		}

		public bool UnLoad()
		{
			ConstantsManager.removeGravityModifier("waveyGrav");
			return true;
		}

		public string name { get; }

		public string version { get; }

		public string author { get; }

		public bool enabled { get; set; }

		public bool cosmetic { get; set; }

		public int id { get; set; }

		public bool OnOptionChanged()
		{
			Debug.Log("option changed");
			return true;
		}

		public List<ModOption> options { get; set; }

		public string Update(GameObject player)
		{
			return "";
		}

		public string FixedUpdate(GameObject player)
		{
			this.time += Time.deltaTime * 3.1415927f / (RangedFloat)this.options[1].value;
			this.time %= 6.2831855f / (RangedFloat)this.options[1].value;
			float num = Mathf.Sin(this.time) * ((RangedFloat)this.options[0].value / 2f) + (float)this.options[0].value / 2f;
			ConstantsManager.modifyGravity("waveyGrav", num);
			return "";
		}

		public string OnGUI(GameObject player, bool menuShown)
		{
			return "";
		}

		private float time;
	}
}
