using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons")]
public class Weapons : Item {
    public GameObject Weapon = null;
    public Vector3 WeaponOffset;
    public Sprite icon1 = null;

    //Weapon values
    public float damage;
    public float range;
    public int cycle;
    public short minShots;
    public short maxShots;
    public float interval;
    public float angle;

    // This is called when pressed in the inventory
    public override void Use()
    {
        //Find the particle system that controls the bullets and change them
        GameObject[] test = GameObject.FindGameObjectsWithTag("FireWeapon_Particle");
        Particle_test par_test = test[0].GetComponent<Particle_test>();
        par_test.WeaponType(cycle, minShots, maxShots, interval, angle, range);

        //Find the WeaponSlot and change the equiped weapon
        GameObject[] test2 = GameObject.FindGameObjectsWithTag("Weapon_Sprite");
        WeaonSlot wep = test2[0].GetComponent<WeaonSlot>();
        wep.ChangeWeapon(this);

        /*GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(player.Length);
        GameObject spawnedWep = Instantiate(Weapon,player[0].transform.position + WeaponOffset, player[0].transform.rotation);
        spawnedWep.transform.parent = player[0].transform;
        */    
    
        Debug.Log(name + " Weapon clicked!");

        RemoveFromInventory();  // Remove the item after use
    }

}
