using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;
    public Image dublikateIcon;
    public Text dublikateText;

	public Item item;	// Current item in the slot

   // public int stack = 1;

	// Add item to the slot
	public void AddItem (Item newItem)
	{
		item = newItem;
        
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	// Clear the slot
	public void ClearSlot ()
	{
            item = null;

            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;

    
	}

	// If the remove button is pressed, this function will be called.
	public void RemoveItemFromInventory ()
	{
        print(item.stack);
        if (item.stack == 1 || !item.canStack)
        {
            dublikateIcon.gameObject.SetActive(false);
            Inventory.instance.Remove(item);
        }
        else
        {
            item.stack -= 2;
            dublikate();
       }
    }

	// Use the item
	public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
		}
	}

    public void dublikate()
    {
        item.stack++;
        if(!dublikateIcon.gameObject.active)
            dublikateIcon.gameObject.SetActive(true);
        if (item.stack == 1) dublikateIcon.gameObject.SetActive(false);
        dublikateText.text = ""+item.stack;
    }

}
