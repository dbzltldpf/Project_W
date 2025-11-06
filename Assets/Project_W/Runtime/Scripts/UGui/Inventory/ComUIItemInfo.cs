namespace W
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ComUIItemInfo : MonoBehaviour
    {
        #region Define
        [System.Serializable]
        public class InfoRoot
        {
            public Transform root;
        }

        [System.Serializable]
        public class Title : InfoRoot
        {
            public TMPro.TextMeshProUGUI title;
        }

        [System.Serializable]
        public class Effect : InfoRoot
        {
            public Image icon;
            public TMPro.TextMeshProUGUI value;
        }

        [System.Serializable]
        public class Context : InfoRoot
        {
            public TMPro.TextMeshProUGUI context;
        }
        #endregion

        [Header("Components.")]
        [SerializeField] private Title title;
        [SerializeField] private Effect effect;
        [SerializeField] private InfoRoot detailRoot;
        [SerializeField] private Effect effectDetail;
        [SerializeField] private Effect effectDuration;
        [SerializeField] private Context effectContext;
        [SerializeField] private Context context;

        /// <summary>
        /// TODO: 인벤토리가 Open일때, Ctrl키 입력 시 정보창 On/Off 설정하기.
        /// </summary>
        public void SetShow(bool isShow)
        {
            gameObject.SetActive(isShow);
        }

        public void OnUpdate(in ItemData itemData)
        {
            if (itemData == null)
                return;

            SetFrom(itemData);
            UpdateTitle(itemData.Title);
            UpdateEffect(itemData);
            UpdateDetail(itemData);
        }

        /// <summary>
        /// 아이템의 정보에 따라서 표시되는 폼이 다름.
        /// 구분: 기본 아이템, 사용 아이템, 효과가 있는 사용 아이템
        /// </summary>
        private void SetFrom(in ItemData itemData)
        {
            // 사용 아이템일 경우, effect 창 확장.
            bool isFlag = itemData.IsUseable;
            effect.root.gameObject.SetActive(isFlag);

            // 사용 아이템이고, 효과가 있을 경우 effectDetail, effectContext 창 확장.
            isFlag &= itemData.HasEffect;
            detailRoot.root.gameObject.SetActive(isFlag);
            effectContext.root.gameObject.SetActive(isFlag);
        }

        private void UpdateTitle(string itemName)
        {
            this.title.title.text = itemName;
        }

        private void UpdateEffect(in ItemData itemData)
        {
            if (!itemData.IsUseable)
                return;

            effect.value.text = $"{itemData.Value}";
        }

        private void UpdateDetail(in ItemData itemData)
        {
            if (!itemData.HasEffect)
                return;

            effectDetail.value.text = $"{itemData.Effect_Value}";
            effectDuration.value.text = $"{itemData.Effect_Duration}";
            effectContext.context.text = $"{itemData.Effect_Content}";
        }
    }
}