using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem(item);
                Destroy(gameObject); // Уничтожаем объект предмета в сцене.
            }
        }
    }
}
