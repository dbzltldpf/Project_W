namespace S
{
    using System.Collections.Generic;
    using UnityEngine;
    public class PotionThrowIndicator : MonoBehaviour
    {
        public RectTransform dotPrefab;
        public RectTransform arrowPrefab;

        private float maxDistance = 2f;
        private float throwDistance;
        public float spacing = 0.5f;
        public float startOffset = 1f;
        [SerializeField]
        private List<RectTransform> dots = new List<RectTransform>();
        private RectTransform arrow;
        public void Awake()
        {
            arrow = Instantiate(arrowPrefab, transform);
            arrow.gameObject.SetActive(false);
        }
        public void UpdateAimIndicator(Vector2 origin, Vector2 aimDir)
        {
            Debug.Log(aimDir);
            if(aimDir == Vector2.zero)
            {
                Hide();
                return;
            }
            Vector2 finalPos = origin + aimDir * maxDistance;

            RaycastHit2D hit = Physics2D.Raycast(origin, aimDir, maxDistance, LayerMask.GetMask("Water"));
            if (hit.collider != null)
            {
                finalPos = hit.point;
            }

            float hitDistance = Vector2.Distance(origin, finalPos);
            int dotCount = Mathf.CeilToInt((hitDistance - startOffset) / spacing);

            while(dots.Count < dotCount)
            {
                var dot = Instantiate(dotPrefab, transform);
                dots.Add(dot);
            }

            for (int i = 0; i < dots.Count; i++)
            {
                Vector2 pos = origin + aimDir * (startOffset + spacing * i);
                dots[i].position = Camera.main.WorldToScreenPoint(pos);
                dots[i].gameObject.SetActive(true);
            }
            arrow.position = Camera.main.WorldToScreenPoint(finalPos);
            arrow.gameObject.SetActive(true);
        }
        public void Hide()
        {
            foreach(var dot in dots)
            {
                dot.gameObject.SetActive(false);
            }
            arrow.gameObject.SetActive(false);
        }

    }
}
