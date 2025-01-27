using System;
using UnityEngine;

namespace Modpack.UI
{
	[DisallowMultipleComponent]
	public class ModList : MonoBehaviour
	{
		private void Start()
		{
			this.createMods();
		}

		private void createMods()
		{
			this.template.gameObject.SetActive(true);
			for (int i = 0; i < ModManager.modObjects.Count; i++)
			{
				ModTemplate component = global::UnityEngine.Object.Instantiate<GameObject>(this.template.gameObject, this.content.transform).GetComponent<ModTemplate>();
				component.modsUI = this.modsUI;
				component.text.text = ModManager.modObjects[i].name;
				component.toggle.isOn = ModManager.modObjects[i].enabled;
				component.id = i;
			}
			this.template.gameObject.SetActive(false);
		}

		public ModTemplate template;

		public GameObject content;

		[HideInInspector]
		public ModManagerUI modsUI;
	}
}
