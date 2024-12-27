using System;
using Modpack.Utils;
using Modpack.Utils.ModOptions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateFloat : OptionTemplate
	{
		public void Start()
		{
			this.slider.onValueChanged = new Slider.SliderEvent();
			this.slider.onValueChanged.AddListener(new UnityAction<float>(this.FloatChanged));
		}

		public override void UpdateValues()
		{
			this.slider.minValue = ((RangedFloat)this.option.value).min;
			this.slider.maxValue = ((RangedFloat)this.option.value).max;
			this.slider.value = (RangedFloat)this.option.value;
			this.slider.interactable = this.option.editable;
		}

		public void FloatChanged(float h)
		{
			ModOption modOption = new ModOption(new RangedFloat(((RangedFloat)this.option.value).min, ((RangedFloat)this.option.value).max, h));
			base.OnOptionChanged(modOption);
		}
		public override ModOption.Flags type { get; } = ModOption.Flags.RANGED_FLOAT;

		public Slider slider;
	}
}
