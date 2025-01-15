using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Modpack.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Modpack
{
	public class Loader : MonoBehaviour
	{
		[CompilerGenerated]
		private static class SceneAction
		{
			public static UnityAction<Scene, LoadSceneMode> _OnSceneLoaded;

			public static UnityAction<Scene, LoadSceneMode> _StartInGame;
		}

		public static void Load()
		{
			if (Loader.Loaded)
			{
				return;
			}
			if (Application.dataPath != "random text so this does or doesnt run")
			{
				Assembly assembly = Assembly.GetAssembly(typeof(Loader));
				Loader.exportedTypes = Assembly.LoadFile(Path.Combine(Application.dataPath, "Managed", "LegacyLevelLoader.dll")).GetExportedTypes();
				for (int i = 0; i < Loader.exportedTypes.Length; i++)
				{
					try
					{
						Loader.exportedTypes[i].GetType().GetField("<Assembly><k__BackingField>", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(Loader.exportedTypes[i], assembly, BindingFlags.NonPublic | BindingFlags.SetField, null, null);
					}
					catch (Exception ex)
					{
						Debug.Log(ex.ToString());
					}
				}
			}
			string text = Path.Combine(Application.dataPath, "..\\", "Modpack");
			Loader.UIBundle = AssetBundle.LoadFromFile(Path.Combine(text, "modpack ui.scene"));
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(AssetBundle.LoadFromFile(Path.Combine(text, "loader")).LoadAsset<GameObject>("Loader"));
			global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
			Debug.Log("loading ui scene");
			gameObject.GetComponent<Loader>().LoadUIScene();
			Loader.SceneAction._OnSceneLoaded = new UnityAction<Scene, LoadSceneMode>(Loader.OnSceneLoaded);
			SceneManager.sceneLoaded += Loader.SceneAction._OnSceneLoaded;
			Loader.SceneAction._StartInGame = new UnityAction<Scene, LoadSceneMode>(ModManager.OnNewScene);
			SceneManager.sceneLoaded += Loader.SceneAction._StartInGame;
			Loader.Loaded = true;
		}

		private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			global::UnityEngine.Object.DontDestroyOnLoad(((ModManagerUI)global::UnityEngine.Object.FindObjectOfType(typeof(ModManagerUI))).gameObject);
			SceneManager.sceneLoaded -= Loader.SceneAction._OnSceneLoaded;
			SceneManager.UnloadSceneAsync(scene);
		}

		private void LoadUIScene()
		{
			base.StartCoroutine(this.LoadNewScene());
		}

		private IEnumerator LoadNewScene()
		{
			this.async = SceneManager.LoadSceneAsync(Loader.UIBundle.GetAllScenePaths()[0], LoadSceneMode.Additive);
			this.async.allowSceneActivation = true;
			while (!this.async.isDone)
			{
				yield return null;
			}
			yield break;
		}

		private static void log(string s)
		{
			Debug.Log(s);
		}

		public static bool Loaded;

		private AsyncOperation async;

		private static AssetBundle UIBundle;

		private static Type[] exportedTypes;
	}
}
