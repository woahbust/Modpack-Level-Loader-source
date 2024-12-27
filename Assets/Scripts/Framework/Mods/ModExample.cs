using System;
using System.Collections.Generic;
using Modpack.Utils;
using Modpack.Utils.ModOptions;
using UnityEngine;

namespace Modpack.Mods
{
	public class ModExample : IMod
	{
		public bool Load()
		{
			Debug.Log("load");
			return true;
		}

		public ModExample()
		{
			Debug.Log("example");
			this.name = "Example Mod";
			this.version = "1.0";
			this.author = "Author";
			this.options = new List<ModOption>
			{
				new ModOption(3, "int title"),
				new ModOption(new RangedFloat(1f, 2.5f, 3f), "float"),
				new ModOption("default text", "string"),
				new ModOption(new Color(0.1f, 1f, 0.1f), "color cosmetic").Cosmetic(true),
				new ModOption(true, "boolean"),
				new ModOption(new List<ModOption>
				{
					new ModOption("what text", "string"),
					new ModOption(new Color(0.1f, 1f, 0.1f), "color"),
					new ModOption(new List<ModOption>
					{
						new ModOption(true, "boolean1 cosmetic").Cosmetic(true),
						new ModOption(true, "boolean2")
					}, "list within a list")
				}, "list")
			};
			this.cosmetic = false;
		}

		public bool UnLoad()
		{
			Debug.Log("unload");
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
			return "";
		}

		public string OnGUI(GameObject player, bool menuShown)
		{
			return "";
		}
	}
}
