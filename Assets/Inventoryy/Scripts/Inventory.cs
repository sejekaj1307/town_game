using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 20;	// Amount of item spaces
    public string dublikate = "";

    // Our current list of items in the inventory
    public List<Item> items = new List<Item>();

	// Add a new item if enough room
	public bool Add (Item item)
	{
        
		if (item.showInInventory) {
			if (items.Count >= space) {
				Debug.Log ("Not enough room.");
				return false;
			}
            
            for(int i = 0; i < items.Count; i++)
            {
                if(item.name == items[i].name && item.canStack)
                {
                    print("Dublikate");
                    dublikate = item.name;
                }
            }
            if(dublikate == "")
            {
                item.stack = 1;
                items.Add(item);
            }

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
        return true;
	}

	// Remove an item
	public void Remove (Item item)
	{
        if (item.stack == 1 || !item.canStack)
        {
            print("Remove!");
            items.Remove(item);
        }
        else { item.stack -= 2; dublikate = item.name; }

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

}
