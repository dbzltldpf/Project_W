namespace W
{
    public class MaterialItemFactory : ItemFactory<MaterialItemData, MaterialItem>
    {
        public override MaterialItem Create(MaterialItemData data)
        {
            return new MaterialItem(data);
        }
    }
}