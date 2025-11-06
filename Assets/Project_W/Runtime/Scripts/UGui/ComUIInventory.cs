namespace W
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.InputSystem;
    using System.Linq;

    public class ComUIInventory : ComUGui
    {
        [Header("Components")]
        [SerializeField, Tooltip("this is slot prefab.")]
        private GameObject slotPrefab;
        [SerializeField, Tooltip("transform of the parent where the slot will be created.")]
        private Transform contentTransform;
        [SerializeField]
        private GridLayoutGroup gridLayoutGroup = null;

        [Space()]
        [SerializeField] private ComUIHighlightController highlight;
        [SerializeField] private ComUIScrollViewRowController scroll;
        [SerializeField] private ComUIItemInfo info;

        [Header("Settings")]
        [SerializeField, Tooltip("determines the number of slots.")]
        private int slotCount;
        [SerializeField]
        private int visibleRowCount = 6;

        private bool isInit = false;
        private Vector2Int currentSlotIndex;
        private ComUISlot[,] slots = null;
        private Dictionary<ItemData, int> itemDatas = new Dictionary<ItemData, int>();

        #region Property
        public int VisibleRowCount => visibleRowCount;
        public int MaxRow { get; private set; }
        public int MaxCol { get; private set; }
        public ItemData GetItemData => slots[currentSlotIndex.y, currentSlotIndex.x].GetItemData;
        #endregion

        protected override void Awake() 
        {
            base.Awake();
            Initialize();
        }

        private void OnEnable()
        {
            currentSlotIndex = Vector2Int.zero;
            highlight.OnUpdate(currentSlotIndex, GetItemData);
        }

        private void Update()
        {
            if (!isOpen || Keyboard.current == null)
                return;

            Vector2Int direction = Vector2Int.zero;

            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                direction = Vector2Int.down;
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                direction = Vector2Int.up;
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                direction = Vector2Int.left;
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                direction = Vector2Int.right;

            if (direction != Vector2Int.zero)
            {
                currentSlotIndex = CalculateIndex(direction);
                scroll.ScrollToRowSmooth(currentSlotIndex.y);
                highlight.OnUpdate(currentSlotIndex, GetItemData);
                info.OnUpdate(GetItemData);
            }
        }

        private void Initialize()
        {
            if (!isInit)
            {
                InitSlots();
                UpdateSlot();

                isInit = true;
            }
        }

        private void InitSlots()
        {
            MaxCol = gridLayoutGroup.constraintCount;
            MaxRow = slotCount / MaxCol;

            slots = new ComUISlot[MaxRow, MaxCol];

            // Create
            if(contentTransform.childCount < slotCount)
            {
                for(int i = 0, length = slotCount - contentTransform.childCount; i < length; ++i)
                {
                    Instantiate(slotPrefab, contentTransform);
                }
            }

            // regist
            for(int i = 0, length = contentTransform.childCount; i < length; ++i)
            {
                if (contentTransform.GetChild(i).TryGetComponent<ComUISlot>(out var component))
                    slots[i / 5, i % 5] = component;
            }
        }

        private Vector2Int CalculateIndex(Vector2Int direction)
        {
            Vector2Int newIndex = currentSlotIndex + direction;

            // 위아래 래핑
            if (newIndex.y < 0)
                newIndex.y = MaxRow - 1;
            else if (newIndex.y >= MaxRow)
                newIndex.y = 0;

            // 좌우 래핑
            if (newIndex.x < 0)
                newIndex.x = MaxCol - 1;
            else if (newIndex.x >= MaxCol)
                newIndex.x = 0;

            return newIndex;
        }

        private void ClearAllSlots()
        {
            System.Array.ForEach(slots.Cast<ComUISlot>().ToArray(), slot => slot?.Clear());
        }

        private void UpdateSlot()
        {
            ClearAllSlots();

            int i = 0;
            foreach (var itemData in itemDatas)
            {
                slots[i / MaxCol, i % MaxCol].SetData(itemData.Key, itemData.Value);
                i++;
            }
        }

        public void AddItem(ItemData itemData, int count)
        {
            if (!isInit)
                Initialize();

            if (!itemDatas.ContainsKey(itemData))
                itemDatas.Add(itemData, 0);

            itemDatas[itemData] += count;
            UpdateSlot();
        }

        public void RemoveItem(ItemData itemData, int count)
        {
            Debug.Log($"<color=#00FF22>{itemData.Title}를 {count}개 제거 시도.</color>");

            if (!isInit)
                Initialize();

            if (!itemDatas.ContainsKey(itemData))
            {
                itemDatas.Add(itemData, 0);
                Debug.Log($"<color=#00FF22>{itemData.Title}를 컨테이너에 추가했습니다.</color>");
            }

            if (itemDatas[itemData] - count < 0)
                itemDatas[itemData] = 0;
            else
                itemDatas[itemData] -= count;

            UpdateSlot();
        }
    }
}