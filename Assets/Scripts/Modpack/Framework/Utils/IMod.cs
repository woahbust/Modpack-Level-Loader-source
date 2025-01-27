using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modpack.Utils
{
	internal interface IMod
	{
		bool Load();

		bool UnLoad();

		bool OnOptionChanged();

		string name { get; }

		string version { get; }

		string author { get; }

		bool cosmetic { get; }

		bool enabled { get; set; }

		List<ModOption> options { get; set; }

		int id { get; set; }

		string Update(GameObject player);

		string FixedUpdate(GameObject player);

		string OnGUI(GameObject player, bool menuShown);
	}
}
