namespace W
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ComUIPotions : MonoBehaviour
    {
        private RectTransform rt;
        private List<GameObject> potions = new();

        [SerializeField] 
        private Transform target = null;
        [SerializeField]
        private Vector2 pivot = Vector2.zero;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if(target is not null)
                Move();
        }

        private void Move()
        {
            rt.anchoredPosition = Camera.main.ScreenToWorldPoint(target.position);
            rt.anchoredPosition += pivot;
        }
    }
}