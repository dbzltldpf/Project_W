namespace W
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class ComUIScrollViewRowController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ComUIInventory inventory;
        [SerializeField] private ScrollRect scrollRect;

        [Header("Settings")]
        [SerializeField] private float duration = 0.15f;

        private Coroutine scrollCoroutine;

        #region Validate
        [Header("Validate")]
        public bool onValidate = false;
        public int targetRowIndex = 0;

        private void OnValidate()
        {
            if(onValidate)
            {
                ScrollToRowSmooth(targetRowIndex);

                onValidate = false;
            }
        }
        #endregion

        /// <summary>
        /// targetRowIndex로 지정한 시간(duration)에 맞춰 부드럽게 스크롤 이동
        /// </summary>
        public void ScrollToRowSmooth(int targetRowIndex)
        {
            float targetValue = LayoutHelper.GetScrollValue(targetRowIndex, inventory.MaxRow, inventory.VisibleRowCount);

            if (scrollCoroutine != null)
                StopCoroutine(scrollCoroutine);

            scrollCoroutine = StartCoroutine(CoScrollToRowSmooth(targetValue));
        }

        /// <summary>
        /// 지정 시간 동안 Scrollbar.value를 Lerp로 보간
        /// </summary>
        private IEnumerator CoScrollToRowSmooth(float targetValue)
        {
            float startValue = scrollRect.verticalScrollbar.value;
            float elapsed = 0f;

            while(elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                scrollRect.verticalScrollbar.value = Mathf.Lerp(startValue, targetValue, t);
                yield return null;
            }

            scrollRect.verticalScrollbar.value = targetValue;
            scrollCoroutine = null;
        }
    }
}