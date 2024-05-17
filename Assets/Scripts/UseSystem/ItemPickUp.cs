public class ItemPickUp : BaseUsable
{
    public ItemSO ItemSO;
    public int Quantity = 1;

    public override void Use()
    {
        Inventory.Instance.AddToInventory(ItemSO, Quantity);
        Destroy(gameObject, 0.1f);
    }
}
