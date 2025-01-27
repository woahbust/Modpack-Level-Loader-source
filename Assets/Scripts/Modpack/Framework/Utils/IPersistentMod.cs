using System;
using UnityEngine.SceneManagement;

namespace Modpack.Utils
{
	internal interface IPersistentMod : IMod
	{
		void OnNewScene(Scene scene, LoadSceneMode loadSceneMode);
	}
}
