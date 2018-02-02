using UnityEngine;
using System.Collections;

public class ItemLoader : MonoBehaviour {

    public const string path = "items";

	// Use this for initialization
	void Start () 
    {
        ItemContainer ic = ItemContainer.Load(path);

        foreach (Item_wep item in ic.items)
        {
            print(item.name);
        }
	}
	
	
}
