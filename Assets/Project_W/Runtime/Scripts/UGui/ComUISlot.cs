namespace W
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ComUISlot : MonoBehaviour
    {
        [SerializeField, Tooltip("put the icon component here.")]
        private Image icon;
        [SerializeField, Tooltip("put the TMP(count) component here.")]
        private TMPro.TextMeshProUGUI count;

        private RectTransform rectTransform = null;
        private ItemData itemData = null;

        public ItemData GetItemData => itemData;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            Clear();
        }

        // TODO Set icon sprite.
        public void SetData(in ItemData itemData, int count)
        {
            this.itemData = itemData;
            this.count.text = count > 0 ? $"{count}" : string.Empty;
        }

        public void Clear()
        {
            icon.sprite = null;
            icon.color = Color.clear;
            count.text = string.Empty;
        }
    }
}