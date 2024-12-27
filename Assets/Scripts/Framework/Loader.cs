using System;
using System.Collections;
using System.IO;
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
			string text = "Assets/Modpack";
			UIBundle = AssetBundle.LoadFromFile(Path.Combine(text, "modpack ui.scene"));
			GameObject gameObject = Instantiate(AssetBundle.LoadFromFile(Path.Combine(text, "loader")).LoadAsset<GameObject>("Loader"));
			DontDestroyOnLoad(gameObject);
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
	}
}
