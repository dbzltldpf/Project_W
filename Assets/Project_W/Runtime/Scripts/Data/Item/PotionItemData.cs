[UnityEngine.SerializeField]
public class PotionItemData : IItemData
{
    // 아이템의 고유 번호
    public int ID;
    // 아이템의 이름
    public string Name;
    // 아이템의 설명
    public string Desciption;
    // 제조 순서
    public int[] ManufacturOrder;
    // 제조 시 온도 제어 값
    public int Temperature;
    // 제조 시 회전 방향 값
    public int MixDirection;
    // 제조 시 필요한 재료 아이템의 번호
    public int[] Materials;

    int IItemData.ID => ID;
}