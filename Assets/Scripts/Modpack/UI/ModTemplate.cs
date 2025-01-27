using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modpack.UI
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Button))]
	public class ModTemplate : MonoBehaviour
	{
		private void Awake()
		{
			base.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.updateOptionsList));
			this.toggle.onValueChanged = new Toggle.ToggleEvent();
			this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.updateEnabled));
		}

		private void updateOptionsList()
		{
			this.modsUI.UpdateOptionsList(this.id);
		}

		private void updateEnabled(bool b)
		{
			this.modsUI.UpdateEnabled(this.id, b);
		}

		public Toggle toggle;

		public TextMeshProUGUI text;

		[HideInInspector]
		public ModManagerUI modsUI;

		[HideInInspector]
		public int id;
	}
}
