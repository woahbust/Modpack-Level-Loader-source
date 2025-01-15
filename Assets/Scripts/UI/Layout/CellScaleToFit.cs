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
                float x = rectTransform.rect.width - padding * 2;
                grid.cellSize = new Vector2(x / grid.constraintCount, grid.cellSize.y);
            }
            if (grid.constraint == GridLayoutGroup.Constraint.FixedRowCount)
            {
                float y = rectTransform.rect.height - padding * 2;
                grid.cellSize = new Vector2(grid.cellSize.x, y / grid.constraintCount);
            }
        }
    }
}