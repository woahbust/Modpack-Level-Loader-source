using System;
using Modpack.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateColor : OptionTemplate
	{
		public override void Init()
		{
			this.red.onValueChanged = new Slider.SliderEvent();
			this.red.onValueChanged.AddListener(new UnityAction<float>(this.ColorChanged));
			this.green.onValueChanged = new Slider.SliderEvent();
			this.green.onValueChanged.AddListener(new UnityAction<float>(this.ColorChanged));
			this.blue.onValueChanged = new Slider.SliderEvent();
			this.blue.onValueChanged.AddListener(new UnityAction<float>(this.ColorChanged));
			this.alpha.onValueChanged = new Slider.SliderEvent();
			this.alpha.onValueChanged.AddListener(new UnityAction<float>(this.ColorChanged));
		}

		public override void UpdateValues()
		{
			this.updating = true;
			this.red.value = ((Color)this.option.value).r;
			this.green.value = ((Color)this.option.value).g;
			this.blue.value = ((Color)this.option.value).b;
			this.alpha.value = ((Color)this.option.value).a;
			this.red.interactable = this.option.editable;
			this.green.interactable = this.option.editable;
			this.blue.interactable = this.option.editable;
			this.alpha.interactable = this.option.editable;
			this.updating = false;
			this.ColorChanged(0f);
		}

		public void ColorChanged(float h)
		{
			if (this.updating)
			{
				return;
			}
			this.color = new Color(this.red.value, this.green.value, this.blue.value, this.alpha.value);
			base.OnOptionChanged(new ModOption(this.color));
		}

		public override ModOption.Flags type { get; } = ModOption.Flags.COLOR;

		public Slider red;

		public Slider green;

		public Slider blue;

		public Slider alpha;

		private Color color;

		private bool updating;
	}
}
