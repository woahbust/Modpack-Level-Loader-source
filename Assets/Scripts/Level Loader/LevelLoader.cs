using System.IO;
using System.Linq;
using UnityEngine;
using Modpack.LevelLoader.Utils;

namespace Modpack.LevelLoader
{
    public static class LevelLoader
    {
        //public static string LevelPath { get; private set; } = Path.Combine(Application.dataPath, "..\\", "Modpack", "Level Loader", "Levels");
        public static string LevelPath { get; private set; } = Path.Combine(Application.dataPath, "Modpack", "Level Loader", "Levels");
        public static bool Loading = false;
        public static LevelInfo[] GetLevelInfos()
        {
            return (Directory.GetFiles(LevelPath, "*.scene", SearchOption.AllDirectories)
                .Select(l => new LevelInfo(l))).ToArray();
        }
    }
}