namespace W
{
    public abstract class ItemFactory<TData, TItem>
    {
        public abstract TItem Create(TData data);
    }
}