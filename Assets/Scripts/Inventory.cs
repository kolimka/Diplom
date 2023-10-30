using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots; // Массив ячеек инвентаря
    public void AddItem(Item newItem)
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.IsOccupied()) // Проверяем, не занята ли ячейка
            {
                slot.SetItem(newItem);
                return; // Мы добавили предмет, поэтому можем выйти из цикла
            }
        }
    }
}
