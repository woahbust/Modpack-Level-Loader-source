using System;
using Modpack.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateBoolean : OptionTemplate
	{
		public void Start()
		{
			this.toggle.onValueChanged = new Toggle.ToggleEvent();
			this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleChanged));
		}

		public override void UpdateValues()
		{
			this.toggle.isOn = (bool)this.option.value;
			this.toggle.interactable = this.option.editable;
		}

		public void ToggleChanged(bool b)
		{
			base.OnOptionChanged(new ModOption(b));
		}
		public override ModOption.Flags type { get; } = ModOption.Flags.BOOL;

		public Toggle toggle;
	}
}
