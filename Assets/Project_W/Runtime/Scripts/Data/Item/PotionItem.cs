namespace W
{
    using System.Collections.Generic;

    public class PotionItem : BaseItem, IUseable
    {
        // 포션 기초 판매 가격
        public int SellPrice { get; private set; }
        // 아이템 제조법 순서
        public List<int> ManufactureOrder { get; private set; } = new();
        // 필요 온도
        public int Temperature { get; private set; }
        // 재료 섞기 이벤트의 방향
        public int MixDirection { get; private set; }
        // 제조시 필요한 재료
        public List<int> Materials { get; private set; } = new();
        
        public PotionItem(PotionItemData data)
        {
            ID = data.ID;
            Name = data.Name;
            Description = data.Desciption;
            Type = ItemType.Potion;
            ManufactureOrder = new List<int>(data.ManufacturOrder);
            Temperature = data.Temperature;
            MixDirection = data.MixDirection;
            Materials = new List<int>(data.Materials);
        }

        public virtual void Use()
        {
            UnityEngine.Debug.Log($"{Name} 포션을 사용.");
        }
    }
}