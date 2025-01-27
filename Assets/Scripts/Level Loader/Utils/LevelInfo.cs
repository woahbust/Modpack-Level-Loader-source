using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Modpack.LevelLoader.Utils
{
    [Serializable]
    public class LevelInfo
    {
        public Texture2D thumbnail;

        public Dictionary<SettingType, string> settings;

        public string path;

        public enum SettingType
        {
            TITLE = 0,
            AUTHOR = 1,
            DESCRIPTION = 2,
            SCENE = 3,
        }
        private enum SettingTypeSynonym
        {
            TITLE = SettingType.TITLE,
            NAME = TITLE,
            MAPNAME = TITLE,
            AUTHOR = SettingType.AUTHOR,
            CREDIT = AUTHOR,
            DESCRIPTION = SettingType.DESCRIPTION,
            SCENE = SettingType.SCENE,
            SCENENAME = SCENE,
            STARTINGSCENE = SCENE,
        }

        public LevelInfo(string path)
        {
            this.path = path;
            string infoPath = GetOtherExtensions(path, ".txt");
            string thumbPath = GetOtherExtensions(path, ".jpg", ".jpeg", ".png");
            if (infoPath != null)
            {
                SetInfosFromFile(infoPath);
            }
            if (thumbPath != null)
            {
                SetThumbnailFromFile(thumbPath);
            }
        }

        private string GetOtherExtensions(string path, params string[] extensions)
        {
            for (int i = 0; i < extensions.Length; i++)
            {
                string p = Path.ChangeExtension(path, extensions[i]);
                if (File.Exists(p))
                {
                    return p;
                }
            }
            return null;
        }

        private void SetInfosFromFile(string path)
        {
            settings = (File.ReadAllLines(path)
                          .Where(l => l.Contains('='))
                          .Select(l => l.Split(new char[] { '=' }, 2))
                          .Where(l => Enum.IsDefined(typeof(SettingTypeSynonym), l[0].ToUpper()))
                          .GroupBy(l => Enum.Parse(typeof(SettingTypeSynonym), l[0].ToUpper())))
                          .ToDictionary(l => (SettingType)l.Key, l => l.Last()[1]);
            return;
        }
        private void SetThumbnailFromFile(string path)
        {
            byte[] array;
            try
            {
                array = File.ReadAllBytes(path);
                Texture2D texture2D = new Texture2D(2, 2);
                texture2D.LoadImage(array);
                thumbnail = texture2D;
            }
            catch (Exception e)
            {
                Debug.Log("oops! " + e.ToString());
            }
            return;
        }
    }
}