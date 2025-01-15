using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Modpack.UI.Layout
{
	[RequireComponent(typeof(RectTransform), typeof(LayoutElement))]
	[ExecuteAlways]
	public class MinScaleToContent : MonoBehaviour
	{
		[Serializable]
		private class OffsetRect
        {
			public RectTransform rectTransform;
			public float offSet;
        }

		[SerializeField]
		private OffsetRect[] content = new OffsetRect[0];
		[SerializeField]
		private bool setMinimumWidthToContentWidth = false;
		[SerializeField]
		private bool setMinimumHeightToContentHeight = false;
		[SerializeField]
		private float minimumWidthOffset = 0.0f;
		[SerializeField]
		private float minimumHeightOffset = 0.0f;

		private LayoutElement layoutElement;
		private RectTransform rect;

		public void Awake()
		{
			layoutElement = GetComponent<LayoutElement>();
			rect = transform as RectTransform;
		}

		public void OnGUI()
		{
			if (setMinimumWidthToContentWidth)
			{
				if (rect.rect.width < 9999)
				{
					float w = 0f;
					foreach (OffsetRect rect in content)
                    {
						w += (rect.rectTransform.rect.width + rect.offSet) * (rect.rectTransform.gameObject.activeSelf ? 1f : 0f) ;
					}
					layoutElement.minWidth = w + minimumWidthOffset;
				}
			}
			if (setMinimumHeightToContentHeight)
			{
				if (rect.rect.height < 9999)
				{
					float h = 0f;
					foreach (OffsetRect rect in content)
					{
						h += (rect.rectTransform.rect.width + rect.offSet) * (rect.rectTransform.gameObject.activeSelf ? 1f : 0f) + rect.offSet;
					}
					layoutElement.minHeight = h + minimumHeightOffset;
				}
			}
		}

	}
}