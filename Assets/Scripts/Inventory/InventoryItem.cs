using System;

[Serializable]
public class InventoryItem
{
    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(InventoryItemData source)
    {
        data = source;
        AddToItemStack();
    }

    public void AddToItemStack()
    {
        stackSize = stackSize + 1;
    }

    public void RemoveFromItemStack()
    {
        stackSize = stackSize - 1;
    }
}