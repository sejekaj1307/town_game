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

    public void ChangeWeapon(Item newWeapon, Vector3 WeaponOffset, GameObject weaponTest)
    {
        //If there is a weapon in the slot and it is not the same that it is to be swaped with, 
        //then add the old item to the inventory and replace it with the new
        if (Weapon != null && Weapon != newWeapon)
        {
            //Remove the old weapon
            GameObject[] activeWeapon = GameObject.FindGameObjectsWithTag("Weapon");
            foreach(GameObject Item in activeWeapon)
            {
                Destroy(Item);
            }

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

        //Add the weapon to the players hand
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(player.Length);
        GameObject spawnedWep = Instantiate(weaponTest, player[0].transform.position + WeaponOffset, player[0].transform.rotation);
        spawnedWep.transform.parent = player[0].transform;
        spawnedWep.tag = "Weapon";
        spawnedWep.transform.eulerAngles = new Vector3(spawnedWep.transform.eulerAngles.x, 248f, spawnedWep.transform.eulerAngles.z);

        Weapon = newWeapon;
    }

}
