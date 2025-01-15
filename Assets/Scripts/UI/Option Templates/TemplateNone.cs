using System;
using Modpack.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateNone : OptionTemplate
	{
		public override void UpdateValues() {}

		public override ModOption.Flags type { get; } = ModOption.Flags.NONE;
	}
}
