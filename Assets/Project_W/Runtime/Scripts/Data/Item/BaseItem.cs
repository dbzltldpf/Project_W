namespace W
{
    public abstract class BaseItem
    {
        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public ItemType Type { get; protected set; }
    }
}