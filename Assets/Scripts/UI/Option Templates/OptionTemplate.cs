using System;
using Modpack.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Button))]
	public abstract class OptionTemplate : MonoBehaviour
	{
		public virtual void Init()
		{
		}

		public void OnOptionChanged(ModOption modOption)
		{
			this.option.UpdateSettings(modOption);
			this.optionCreator.OnOptionChanged(this.id, this.option);
		}

		public void UpdateInfo()
		{
			this.text.text = this.option.title;
			this.UpdateValues();
		}

		public abstract void UpdateValues();

		public virtual void Default(bool b)
		{
			this.OnOptionChanged(this.option.DefaultSettings());
			this.UpdateValues();
		}

		public TextMeshProUGUI text;

		[HideInInspector]
		public abstract ModOption.Flags type { get; }

		[HideInInspector]
		public OptionCreator optionCreator;

		[HideInInspector]
		public ModOption option;

		[HideInInspector]
		public int id;
	}
}
