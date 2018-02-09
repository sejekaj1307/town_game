using UnityEngine;

/* An Item that can be consumed. So far that just means regaining health */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]
public class Consumable : Item {

	public int healthGain;		// How much health?
    public float poisonChance = 0;    //Chance of getting poisoned
    
	// This is called when pressed in the inventory
	public override void Use()
	{
        // Heal the player
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        Z_Movement playerScript = player[0].GetComponent<Z_Movement>();

        if (Random.Range(0f, 1f) < poisonChance) { healthGain *= -1; }
        
        playerScript.Heal(healthGain);

		Debug.Log(name + " consumed!");

		RemoveFromInventory();	// Remove the item after use
	}

}
