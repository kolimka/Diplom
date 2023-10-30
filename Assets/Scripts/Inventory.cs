using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots; // ������ ����� ���������
    public void AddItem(Item newItem)
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.IsOccupied()) // ���������, �� ������ �� ������
            {
                slot.SetItem(newItem);
                return; // �� �������� �������, ������� ����� ����� �� �����
            }
        }
    }
}
