using System;
using Modpack.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateString : OptionTemplate
	{
		public void Start()
		{
			this.inputText.onValueChanged.AddListener(new UnityAction<string>(this.TextChanged));
		}

		public override void UpdateValues()
		{
			if (this.option.title.Equals(""))
			{
				if (!this.option.editable)
				{
					this.text.text = (string)this.option.value;
					this.inputText.gameObject.SetActive(false);
					return;
				}
				this.text.gameObject.SetActive(false);
			}
			this.inputText.text = (string)this.option.value;
			this.inputText.interactable = this.option.editable;
		}

		public void TextChanged(string s)
		{
			base.OnOptionChanged(new ModOption(s));
		}
		public override ModOption.Flags type { get; } = ModOption.Flags.STRING;

		public TMP_InputField inputText;
	}
}
