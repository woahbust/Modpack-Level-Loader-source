using System;
using System.Collections.Generic;
using Modpack.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modpack.UI.OptionTemplates
{
	[DisallowMultipleComponent]
	public class TemplateList : OptionTemplate
	{
		public override void Init()
		{
			this.creator = base.gameObject.GetComponent<OptionCreator>();
			this.left.onClick = new Button.ButtonClickedEvent();
			this.left.onClick.AddListener(new UnityAction(this.Left));
			this.right.onClick = new Button.ButtonClickedEvent();
			this.right.onClick.AddListener(new UnityAction(this.Right));
			foreach (OptionTemplate optionTemplate in this.creator.optionTemplates)
			{
				Button component = optionTemplate.gameObject.GetComponent<Button>();
				component.onClick = new Button.ButtonClickedEvent();
				component.onClick.AddListener(new UnityAction(this.UpdateIndex));
				component.interactable = this.option.selectable;
				optionTemplate.gameObject.SetActive(false);
			}
		}

		private void Left()
		{
			this.UpdateSelected(this.selected - 1);
		}

		private void Right()
		{
			this.UpdateSelected(this.selected + 1);
		}

		private void UpdateSelected(int j)
		{
			this.creator.optionObjects[this.selected].SetActive(false);
			this.selected = j;
			if (this.selected < 0)
			{
				this.selected = ((List<ModOption>)this.option.value).Count - 1;
			}
			if (this.selected > ((List<ModOption>)this.option.value).Count - 1)
			{
				this.selected = 0;
			}
			this.creator.optionTemplates[this.selected].text.color = ((this.option.selectable && this.selected == this.option.index) ? new Color(0f, 1f, 0f) : new Color(1f, 1f, 1f));
			this.creator.optionObjects[this.selected].SetActive(true);
		}

		private void UpdateIndex()
		{
			this.creator.optionTemplates[this.selected].text.color = new Color(0f, 1f, 0f);
			base.OnOptionChanged(this.option.Index(this.selected));
		}

		public override void UpdateValues()
		{
			this.creator.UpdateOptions();
			this.UpdateSelected(this.option.index);
		}

		public override void Default(bool b)
		{
			this.creator.DefaultOptions(b);
		}
		public override ModOption.Flags type { get; } = ModOption.Flags.LIST;

		public GameObject content;

		public Button left;

		public Button right;

		private OptionCreator creator;

		private int selected;
	}
}
