using System;
using Modpack.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modpack.UI
{
	[DisallowMultipleComponent]
	public class OptionList : OptionCreator
	{
		internal override void OnOptionChanged(int id, ModOption modOption)
		{
			ModManager.UpdateOption(this.mod.id, id, modOption);
		}

		public void Start()
		{
			this.defaultCosmeticSettings.onClick = new Button.ButtonClickedEvent();
			this.defaultCosmeticSettings.onClick.AddListener(new UnityAction(base.DefaultCosmeticOptions));
			this.defaultGameplaySettings.onClick = new Button.ButtonClickedEvent();
			this.defaultGameplaySettings.onClick.AddListener(new UnityAction(base.DefaultGameplayOptions));
			this.defaultSettings.onClick = new Button.ButtonClickedEvent();
			this.defaultSettings.onClick.AddListener(new UnityAction(base.DefaultOptions));
			this.panel.SetActive(false);
		}

		public void Update()
		{
			if (this.mod is IDynamicMod)
			{
				IDynamicMod dynamicMod = (IDynamicMod)this.mod;
				if (dynamicMod.shouldUpdateUI)
				{
					base.UpdateOptions();
					dynamicMod.shouldUpdateUI = false;
				}
				if (dynamicMod.shouldRefreshUI)
				{
					this.Init();
					dynamicMod.shouldRefreshUI = false;
				}
			}
		}

		public void Init()
		{
			this.panel.SetActive(this.mod.options.Count != 0);
			if (this.mod.options.Count == 0)
			{
				return;
			}
			this.defaultCosmeticSettings.gameObject.SetActive(false);
			this.defaultGameplaySettings.gameObject.SetActive(false);
			this.defaultSettings.gameObject.SetActive(false);
			bool flag = false;
			bool flag2 = false;
			foreach (ModOption modOption in this.mod.options)
			{
				flag |= modOption.cosmetic;
				flag2 |= modOption.gameplay;
			}
			if (flag ^ flag2)
			{
				this.defaultSettings.gameObject.SetActive(true);
			}
			else
			{
				this.defaultCosmeticSettings.gameObject.SetActive(true);
				this.defaultGameplaySettings.gameObject.SetActive(true);
			}
			ModOption modOption2 = new ModOption(this.mod.options);
			base.CreateOptions(modOption2.Cosmetic(this.mod.cosmetic));
		}

		public GameObject panel;

		public Button defaultCosmeticSettings;

		public Button defaultGameplaySettings;

		public Button defaultSettings;

		[HideInInspector]
		internal IMod mod;
	}
}
