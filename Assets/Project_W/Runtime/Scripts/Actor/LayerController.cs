namespace S
{
    using UnityEngine;
    public class LayerController : MonoBehaviour
    {
        private SpriteRenderer sr;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            sr.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
        }
    }
}
