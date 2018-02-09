using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaonSlot : Inventory {
    public Item Weapon = null;
    Image m_Image;

    void Start()
    {
        //Gets the image component of the object
        m_Image = GetComponent<Image>();        
    }

    public void ChangeWeapon(Item newWeapon)
    {
        //If there is a weapon in the slot and it is not the same that it is to be swaped with, 
        //then add the old item to the inventory and replace it with the new
        if (Weapon != null && Weapon != newWeapon)
        {
            print("Swap Weapons");
            Inventory.instance.Add(Weapon);
        }
        else
        {
            print("Add weapon");

            //If it is the first weapon make the image visible (If this was not here the image would be a white square when set to "null"
            if (m_Image.color.a != 255) { m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 255); }
        }
        m_Image.sprite = newWeapon.icon;

        Weapon = newWeapon;
    }
}
