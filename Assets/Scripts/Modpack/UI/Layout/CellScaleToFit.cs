using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Modpack.UI.Layout
{
    [RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
    public class CellScaleToFit : MonoBehaviour
    {
        private RectTransform rectTransform;
        private GridLayoutGroup grid;

        [SerializeField]
        private float padding = 0f;
        [SerializeField]
        private bool keepRatio = false;
        [SerializeField]
        private float ratio = 1f;

        // Start is called before the first frame update
        void Awake()
        {
            rectTransform = gameObject.transform.parent.transform as RectTransform;
            grid = transform.GetComponent<GridLayoutGroup>();
        }

        // Update is called once per frame
        void OnGUI()
        {
            if (grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
            {
                float x = (rectTransform.rect.width - padding * 2) / grid.constraintCount;
                float y = (keepRatio) ? x / ratio : grid.cellSize.y;
                grid.cellSize = new Vector2(x, y);
            }
            if (grid.constraint == GridLayoutGroup.Constraint.FixedRowCount)
            {
                float y = rectTransform.rect.height - padding * 2;
                float x = (keepRatio) ? y * ratio : grid.cellSize.x;
                grid.cellSize = new Vector2(x, y / grid.constraintCount);
            }
        }
    }
}