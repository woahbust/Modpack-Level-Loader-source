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
		[SerializeField]
		private RectTransform content;
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

		public void Start()
		{
			layoutElement = GetComponent<LayoutElement>();
		}

		public void OnGUI()
		{
			layoutElement = GetComponent<LayoutElement>();
			rect = transform as RectTransform;
			if (content == null)
			{
				return;
			}
			if (setMinimumWidthToContentWidth)
			{
				layoutElement.minWidth = rect.sizeDelta.x + content.sizeDelta.x + minimumWidthOffset;
			}
			if (setMinimumHeightToContentHeight)
			{
				layoutElement.minHeight = rect.sizeDelta.y + content.sizeDelta.y + minimumHeightOffset;
			}
		}

	}
}