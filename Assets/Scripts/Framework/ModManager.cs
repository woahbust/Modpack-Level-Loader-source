using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Modpack.UI;
using Modpack.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modpack
{
	[DisallowMultipleComponent]
	public class ModManager : MonoBehaviour
	{
		public static string AssetPath { get; private set; }

		private void Awake()
		{
			ModManager.Instance = this;
			ModManager.AssetPath = Path.Combine(Application.dataPath, "..\\", "Modpack", "Mod Assets");
		}

		public static void UpdateEnabled(int id, bool enabled)
		{
			if (ModManager.modObjects[id].enabled == enabled)
			{
				return;
			}
			ModManager.modObjects[id].enabled = enabled;
			if (ModManager.InGame || ModManager.modObjects[id] is IPersistentMod)
			{
				if (enabled)
				{
					ModManager.modObjects[id].Load();
					return;
				}
				ModManager.modObjects[id].UnLoad();
			}
		}

		public static void UpdateOption(int id, int index, ModOption option)
		{
			ModManager.modObjects[id].options[index].UpdateSettings(option);
			if (ModManager.modObjects[id].enabled && (ModManager.InGame || ModManager.modObjects[id] is IPersistentMod))
			{
				ModManager.modObjects[id].OnOptionChanged();
			}
		}

		private void FixedUpdate()
		{
			for (int i = 0; i < ModManager.modObjects.Count; i++)
			{
				if ((ModManager.InGame || ModManager.modObjects[i] is IPersistentMod) && ModManager.modObjects[i].enabled)
				{
					ModManager.modObjects[i].FixedUpdate(ModManager.player);
				}
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.H))
			{
				foreach (IMod mod in ModManager.modObjects)
				{
					Debug.Log(mod.name + " " + mod.enabled.ToString());
				}
			}
			for (int i = 0; i < ModManager.modObjects.Count; i++)
			{
				if ((ModManager.InGame || ModManager.modObjects[i] is IPersistentMod) && ModManager.modObjects[i].enabled)
				{
					ModManager.modObjects[i].Update(ModManager.player);
				}
			}
		}

		public static void OnNewScene(Scene scene, LoadSceneMode loadSceneMode)
		{
			if (scene.name == "Mian")
			{
				ModManager.player = GameObject.Find("Player");
				ModManager.InGame = true;
				ModManager.LoadEnabledMods();
				ModManager.ModsLoaded = true;
			}
			for (int i = 0; i < ModManager.persistentModObjects.Count; i++)
			{
				ModManager.persistentModObjects[i].OnNewScene(scene, loadSceneMode);
			}
		}

		public static GameObject player { get; private set; }

		public static ModManager Instance { get; private set; }

		public static bool ModsLoaded { get; private set; }

		public static bool InGame { get; private set; }

		private void OnGUI()
		{
			for (int i = 0; i < ModManager.modObjects.Count; i++)
			{
				if ((ModManager.InGame || ModManager.modObjects[i] is IPersistentMod) && ModManager.modObjects[i].enabled)
				{
					ModManager.modObjects[i].OnGUI(ModManager.player, ModManagerUI.Instance.window.activeSelf);
				}
			}
		}

		private static void LoadEnabledMods()
		{
			if (ModManager.InGame)
			{
				for (int i = 0; i < ModManager.modObjects.Count; i++)
				{
					if (ModManager.modObjects[i].enabled && !(ModManager.modObjects[i] is IPersistentMod))
					{
						try
						{
							ModManager.modObjects[i].Load();
						}
						catch (Exception ex)
						{
							Debug.Log(ex);
						}
					}
				}
				return;
			}
			Debug.Log("loading mods when not in game");
		}

		private static void UnLoadEnabledMods()
		{
			if (ModManager.InGame)
			{
				for (int i = 0; i < ModManager.modObjects.Count; i++)
				{
					if (ModManager.modObjects[i].enabled && !(ModManager.modObjects[i] is IPersistentMod))
					{
						ModManager.modObjects[i].UnLoad();
					}
				}
				return;
			}
			Debug.Log("unloading mods when not in game");
		}

		static ModManager()
		{
			for (int i = 0; i < ModManager.modObjects.Count; i++)
			{
				ModManager.modObjects[i].id = i;
				if (ModManager.modObjects[i] is IPersistentMod)
				{
					ModManager.persistentModObjects.Add((IPersistentMod)ModManager.modObjects[i]);
				}
			}
		}

		internal static List<IMod> modObjects = (from type in Assembly.GetExecutingAssembly().GetTypes()
												 where typeof(IMod).IsAssignableFrom(type) && type.IsClass && type.Namespace == "Modpack.Mods"
												 select (IMod)Activator.CreateInstance(type)).ToList<IMod>();

		internal static List<IPersistentMod> persistentModObjects = new List<IPersistentMod>();
	}
}
