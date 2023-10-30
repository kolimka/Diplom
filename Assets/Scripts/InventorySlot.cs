using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage;
    private bool occupied = false;

    public Canvas guide;
    public TextMeshProUGUI title; // Ссылка на текстовое поле для отображения имени
    public TextMeshProUGUI description; // Ссылка на текстовое поле для отображения описания
    public Button button;
    private Item currentItem;
    private void Awake()
    {
        // Добавляем обработчик клика на эту ячейку
        button.onClick.AddListener(ToggleItemInfo);
    }
    public void SetItem(Item newItem)
    {
        currentItem = newItem;
        itemImage.sprite = newItem.icon;
        itemImage.enabled = true;
        occupied = true;

        if (title != null)
        {
            title.text = newItem.name;
        }

        if (description != null)
        {
            description.text = newItem.description;
        }
    }
    public bool IsOccupied()
    {
        return occupied;
    }
    public void ToggleItemInfo()
    {
        if (currentItem != null)
        {
            // Переключаем видимость информации
            guide.gameObject.SetActive(!guide.gameObject.activeSelf);
        }
        if (guide.gameObject.activeSelf)
        {
            // При открытии окна, показываем информацию о текущем предмете
            if (title != null)
            {
                title.text = currentItem.name;
            }

            if (description != null)
            {
                description.text = currentItem.description;
            }
        }
    }

}
