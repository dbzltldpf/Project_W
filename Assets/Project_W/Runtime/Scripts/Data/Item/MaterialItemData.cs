[UnityEngine.SerializeField]
public class MaterialItemData : IItemData
{
    // 아이템 고유 번호
    public int ID;
    // 아이템 이름
    public string Name;
    // 아이템 설명
    public string Description;
    // 가공 시 필요 수량
    public int NecessaryValue;
    // 가공 시 결과 아이템의 번호
    public int ResultID;
    // 가공 시 결과 아이템의 출력 수량
    public int ResultValue;

    int IItemData.ID => ID;
}