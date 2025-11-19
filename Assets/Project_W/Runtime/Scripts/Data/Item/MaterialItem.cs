namespace W
{
    using UnityEngine;

    public class MaterialItem : BaseItem, IManufacture
    {
        // 가공에 필요한 아이템 수량
        public int NecessaryValue { get; private set; }
        // 가공 후 출력되는 결과 아이템의 번호
        public int ResultID { get; private set; }
        // 가공 후 출력되는 결과 아이템의 수량
        public int ResultValue { get; private set; }

        public MaterialItem(MaterialItemData data)
        {
            ID = data.ID;
            Name = data.Name;
            Description = data.Description;
            Type = ItemType.Material;
            NecessaryValue = data.NecessaryValue;
            ResultID = data.ResultID;
            ResultValue = data.ResultValue;
        }

        public void Manufacture()
        {
            var result = DataManager.Instance.GetItemData<MaterialItemData>(Type, (ItemID)ResultID);

            Debug.Log($"{Name} 아이템 가공 시작");
            Debug.Log($"{Name} 가공 시 필요 수량 -> {NecessaryValue}");
            Debug.Log($"{Name} 가공 후 결과 아이템 -> {(result == null ? "결과 값 없음" : result.Name)}");
            Debug.Log($"{Name} 가공 후 결과 아이템 수량 -> {(result == null ? "결과 값 없음" : ResultValue)}");
        }
    }
}