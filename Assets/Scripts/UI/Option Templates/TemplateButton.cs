using System;
using Modpack.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateButton : OptionTemplate
	{
		public override void Init()
		{
			this.button.onClick = new global::UnityEngine.UI.Button.ButtonClickedEvent();
			this.button.onClick.AddListener(new UnityAction(this.Click));
		}

		private void Click()
		{
			((Modpack.Utils.ModOptions.Button)this.option.value).clicked = true;
			base.OnOptionChanged(this.option);
			((Modpack.Utils.ModOptions.Button)this.option.value).clicked = false;
		}

		public override void UpdateValues()
		{
			this.text.text = this.option.title;
		}

		public override ModOption.Flags type { get; } = ModOption.Flags.BUTTON;

		public UnityEngine.UI.Button button;
	}
}
