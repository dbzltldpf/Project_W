namespace W
{
    using UnityEngine;

    public abstract class ComUGui : MonoBehaviour
    {
        [Header("Define")]
        [SerializeField, Tooltip("this is unique id of ugui.")]
        protected UGuiId id;
        [SerializeField, Tooltip("Whether to activate at startup.")]
        private bool firstOnAwake = false;

        protected bool isOpen = false;

        public UGuiId GetId => id;
        public bool OnAwake => firstOnAwake;
        
        protected virtual void Awake()
        {
            UIManager.Instance.Regist(id, this);
        }

        protected virtual void OnDestroy()
        {
            UIManager.Instance.UnRegist(id);
        }

        public virtual void Open()
        {
            isOpen = true;
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            isOpen = false;
            gameObject.SetActive(false);
        }
    }
}