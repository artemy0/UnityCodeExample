using UnityEngine;

namespace UI.General.Sort
{
    public class SortableUIUnit : MonoBehaviour
    {
        [SerializeField]
        private int sortOrder;

        public int SortOrder
        {
            get => sortOrder;
            set => sortOrder = value;
        }
    }
}