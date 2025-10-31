namespace W
{
    using UnityEngine;
    using UnityEngine.UI;

    public static class LayoutHelper
    {
        /// <summary>
        /// 현재 Row가 스크롤 기준으로 어느 위치에 있는지(0~range) 계산.
        /// </summary>
        public static float GetRowOffset(int currentRow, int totalRows, int visibleRows)
        {
            if (totalRows <= visibleRows)
                return 0f;

            float range = totalRows - visibleRows;
            float pos = Mathf.Clamp(currentRow - (visibleRows / 2f - 1), 0, range);
            return pos;
        }

        /// <summary>
        /// ScrollRect verticalScrollbar value 계산
        /// </summary>
        public static float GetScrollValue(int currentRow, int totalRows, int visibleRows)
        {
            if (totalRows <= visibleRows)
                return 1f;

            float scrollRange = totalRows - visibleRows;
            float scrollPos = Mathf.Clamp(currentRow - (visibleRows / 2f - 1), 0, scrollRange);
            return 1f - (scrollPos / scrollRange);
        }
    }
}
