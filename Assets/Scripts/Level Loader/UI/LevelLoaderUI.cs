using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modpack.LevelLoader.Utils;

namespace Modpack.LevelLoader.UI
{
    [DisallowMultipleComponent]
    public class LevelLoaderUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            levels = LevelLoader.GetLevelInfos();
        }

        // Update is called once per frame
        void Update()
        {

        }

        [SerializeField]
        public GameObject window;

        [SerializeField]
        public GameObject contents;

        [SerializeField]
        public LevelInfo template;

        [SerializeField]
        public LevelInfo sideBar;

        [SerializeField]
        public LevelInfo[] levels;

    }
}