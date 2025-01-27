using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Modpack.UI.Layout
{
    public class SnapToBounds : MonoBehaviour
    {
        public RectTransform bounds;
        void Update()
        {
            if (bounds == null)
            {
                return;
            }
            Vector3 rectScale = transform.lossyScale;
            Vector3 boundsScale = bounds.lossyScale;
            RectTransform rect = transform as RectTransform;
            Vector3 rectSize = rect.sizeDelta * rectScale;
            Vector3 boundsSize = bounds.sizeDelta * boundsScale;
            Vector2 rectPos = (Vector2)transform.position;
            Vector2 boundsPos = (Vector2)bounds.position;

            Vector2 rectHalf = rectSize / 2f;
            Vector2 boundsHalf = boundsSize / 2f;

            Vector2 boundsMin = boundsPos - boundsHalf;
            Vector2 boundsMax = boundsPos + boundsHalf;

            Vector2 rectMin = rectPos - rectHalf;
            Vector2 rectMax = rectPos + rectHalf;

            Vector2 clampedMin = Vector2.Max(rectMin, boundsMin) + rectHalf;
            Vector2 clampedMax = Vector2.Min(rectMax, boundsMax) - rectHalf;

            float maxScaleX = Mathf.Min(boundsSize.x / rectSize.x, 1f);
            float maxScaleY = Mathf.Min(boundsSize.y / rectSize.y, 1f);
            if (maxScaleX != maxScaleY || maxScaleX != 1f)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x * maxScaleX, rect.sizeDelta.y * maxScaleY);
            }
            transform.position = new Vector3(Mathf.Clamp(rectPos.x, clampedMin.x, clampedMax.x), Mathf.Clamp(rectPos.y, clampedMin.y, clampedMax.y), 0f);
        }

    }
}