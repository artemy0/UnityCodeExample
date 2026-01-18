using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UI.General.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General.Elements.Root
{
    public class UIRoot : MonoBehaviour, IDisposable
    {
        private readonly Dictionary<int, RectTransform> sortOrderToParent = new();

        [SerializeField] [Title("Main:")]
        private Canvas targetCanvas;

        [SerializeField]
        private CanvasScaler targetScaler;

        public Canvas Canvas => targetCanvas;
        public Vector2 Resolution => targetScaler.referenceResolution;

        public RectTransform GetParent(int sortOrder)
        {
            if (!sortOrderToParent.TryGetValue(sortOrder, out var parent))
            {
                var sortOrderParent = CreateSortOrderParent(sortOrder);
                sortOrderToParent.Add(sortOrder, sortOrderParent);

                var siblingIndex = GetSiblingIndex(sortOrder);
                sortOrderParent.SetSiblingIndex(siblingIndex);

                return sortOrderParent;
            }

            return parent;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private RectTransform CreateSortOrderParent(int sortOrder)
        {
            var sortOrderGameObject = new GameObject($"SORT_ORDER_{sortOrder}", typeof(RectTransform));

            var sortOrderRectTransform = sortOrderGameObject.GetComponent<RectTransform>();
            sortOrderRectTransform.SetFullScreen(targetCanvas.transform);

            return sortOrderRectTransform;
        }

        private int GetSiblingIndex(int sortOrder)
        {
            return sortOrderToParent.Keys.Count(parentSortOrder => sortOrder > parentSortOrder);
        }
    }
}