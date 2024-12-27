using System;
using System.Collections.Generic;
using Modpack.UI.OptionTemplates;
using Modpack.Utils;
using UnityEngine;

namespace Modpack.UI
{
	public class OptionCreator : MonoBehaviour
	{
		public virtual void Awake()
		{
			this.optionObjects = new List<GameObject>();
			this.optionTemplates = new List<OptionTemplate>();
		}

		public void UpdateOptions()
		{
			foreach (OptionTemplate optionTemplate in this.optionTemplates)
			{
				optionTemplate.UpdateInfo();
			}
		}

		internal void ClearOptions()
		{
			foreach (GameObject gameObject in this.optionObjects)
			{
				global::UnityEngine.Object.DestroyImmediate(gameObject);
			}
			this.optionObjects.Clear();
			this.optionTemplates.Clear();
		}

		internal void CreateOptions(ModOption modOption)
		{
			this.ClearOptions();
			OptionTemplate[] array = this.templates;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(true);
			}
			List<ModOption> list = (List<ModOption>)modOption.value;
			for (int j = 0; j < list.Count; j++)
			{
				GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.GetTemplate(list[j].type).gameObject, this.content.transform);
				gameObject.SetActive(true);
				OptionTemplate component = gameObject.GetComponent<OptionTemplate>();
				component.optionCreator = this;
				component.option = list[j];
				component.id = j;
				this.optionObjects.Add(gameObject);
				this.optionTemplates.Add(component);
				if (list[j].type == ModOption.Flags.LIST)
				{
					this.CreateOptionCreator(gameObject, list[j]);
				}
				component.Init();
			}
			array = this.templates;
			for (int k = 0; k < array.Length; k++)
			{
				array[k].gameObject.SetActive(false);
			}
			this.UpdateOptions();
		}

		internal void CreateOptionCreator(GameObject gameObject, ModOption modOption)
		{
			OptionCreator optionCreator = gameObject.AddComponent<OptionCreator>();
			optionCreator.content = gameObject.GetComponent<TemplateList>().content;
			optionCreator.templates = this.templates;
			optionCreator.CreateOptions(modOption);
		}

		internal virtual void OnOptionChanged(int id, ModOption modOption)
		{
			OptionTemplate component = base.gameObject.GetComponent<OptionTemplate>();
			List<ModOption> list = new List<ModOption>();
			for (int i = 0; i < this.optionTemplates.Count; i++)
			{
				list.Add(this.optionTemplates[i].option);
			}
			ModOption modOption2 = component.option.UpdateSettings(new ModOption(list).Index(component.option.index));
			component.OnOptionChanged(modOption2);
		}

		public void DefaultCosmeticOptions()
		{
			this.DefaultOptions(true);
		}

		public void DefaultGameplayOptions()
		{
			this.DefaultOptions(false);
		}

		public void DefaultOptions()
		{
			this.DefaultOptions(false);
			this.DefaultOptions(true);
		}

		public void DefaultOptions(bool b)
		{
			foreach (OptionTemplate optionTemplate in this.optionTemplates)
			{
				if (optionTemplate.option.cosmetic == b || optionTemplate.option.gameplay == !b)
				{
					optionTemplate.Default(b);
				}
			}
		}

		public List<GameObject> optionObjects { get; private set; }

		public List<OptionTemplate> optionTemplates { get; private set; }

		private OptionTemplate GetTemplate(ModOption.Flags type)
		{
			foreach (OptionTemplate optionTemplate in this.templates)
			{
				if (optionTemplate.type == type)
				{
					return optionTemplate;
				}
			}
			throw new Exception("template of type " + type.ToString() + " not found");
		}

		public GameObject content;

		public OptionTemplate[] templates;
	}
}
