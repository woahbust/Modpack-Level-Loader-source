using System;
using UnityEngine;

namespace Modpack.UI
{
	[DisallowMultipleComponent]
	public class ModManagerUI : MonoBehaviour
	{
		private void Awake()
		{
			ModManagerUI.Instance = this;
			this.modList.modsUI = this;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.M) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
			{
				this.window.SetActive(!this.window.activeSelf);
				this.window.transform.localPosition = Vector3.zero;
			}
		}

		public void UpdateOptionsList(int id)
		{
			if (this.currentId == id)
			{
				return;
			}
			this.currentId = id;
			this.optionList.mod = ModManager.modObjects[id];
			this.optionList.Init();
		}

		public void UpdateEnabled(int id, bool enabled)
		{
			ModManager.UpdateEnabled(id, enabled);
		}

		public GameObject window;

		public OptionList optionList;

		public static ModManagerUI Instance;

		private int currentId = -1;

		public ModList modList;
	}
}
