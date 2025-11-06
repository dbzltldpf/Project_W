namespace W
{
    using UnityEngine;
    using UnityEngine.UI;

    public abstract class ToolBase : MonoBehaviour, ITool
    {
        [Header("Components")]
        [SerializeField] private Image icon;
        [SerializeField] private TMPro.TextMeshProUGUI key;

        [Header("Settings")]
        [SerializeField] private ToolType type;

        protected virtual void Awake()
        {
            ToolManager.Instance.Regist(type, this);
        }

        public abstract void Use();

        public void Equip()
        {
            // TODO: 1. 매뉴창에 현재 착용하고 있는 도구의 이미지로 업데이트.
            // TODO: 2. 도구에 맞게 캐릭터 애니메이션 수정.
        }
    }
}