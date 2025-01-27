using System;
using Modpack.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateInt : OptionTemplate
	{
		public override void Init()
		{
			this.inputText.onValueChanged.AddListener(new UnityAction<string>(this.TextChanged));
		}

		public override void UpdateValues()
		{
			this.inputText.text = Convert.ToString((int)this.option.value);
			this.inputText.interactable = this.option.editable;
		}

		public void TextChanged(string s)
		{
			int num;
			int.TryParse(s, out num);
			base.OnOptionChanged(new ModOption(num));
		}
		public override ModOption.Flags type { get; } = ModOption.Flags.INT;

		public TMP_InputField inputText;
	}
}
