namespace W
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ComUIHighlightController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ComUIInventory inventory = null;
        [SerializeField] private GridLayoutGroup grid = null;

        [Header("Highlight")]
        [SerializeField] private RectTransform highlight = null;
        [SerializeField] private TMPro.TextMeshProUGUI title = null;

        public void OnUpdate(Vector2Int slotIndex, ItemData itemData)
        {
            UpdatePosition(slotIndex);
            UpdateText(itemData);
        }

        /// <summary>
        /// 하이라이트 상단 아이템 이름 변경.
        /// </summary>
        private void UpdateText(ItemData itemData)
        {
            title.text = itemData is null ? string.Empty : itemData.Title;
        }

        /// <summary>
        /// 하이라이트 위치 업데이트.
        /// </summary>
        private void UpdatePosition(Vector2Int slotIndex)
        {
            highlight.anchoredPosition = CalculatePosition(slotIndex);
        }

        /// <summary>
        /// 스크롤이 움직일 때, 하이라이트의 y 위치는 보간.
        /// </summary>
        private Vector2 CalculatePosition(Vector2Int slotIndex)
        {
            var pos = new Vector2(grid.padding.left, -grid.padding.top);

            float rowOffset = LayoutHelper.GetRowOffset(
                slotIndex.y,
                inventory.MaxRow,
                inventory.VisibleRowCount
            );

            float cellWidth = grid.cellSize.x + grid.spacing.x;
            float cellHeight = grid.cellSize.y + grid.spacing.y;

            pos.x += cellWidth * slotIndex.x;
            pos.y -= cellHeight * (slotIndex.y - rowOffset);

            return pos;
        }
    }
}