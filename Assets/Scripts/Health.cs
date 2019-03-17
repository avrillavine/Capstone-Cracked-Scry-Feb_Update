using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Borrows code from a unity tutorial https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
//Differences will be including the anim script with the movement script and maybe implementing in an
//attack script seperately
public class Health : MonoBehaviour
{
	//Total player health
	public int totalHealth = 100;              
	//Health player has at the moment
	public int currentHealth;       
	//UI element that controls the health bars movement
	public Slider healthSlider;

	//Does an effect with a flashing health color 
	//Not implemented yet
	//bool _damaged;
	//TODO Add in death/life states from Tutorial
	//Do animations for damage and death
	// Start is called before the first frame update
	void Start()
    {
		//Player starts off with full health
		currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TakeDamage(int amount)
	{
		// Set the damaged flag so the screen will flash.
		//damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		//playerAudio.Play();

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if (currentHealth <= 0 /*&& !isDead*/)
		{
			// ... it should die.
			//Death();
		}
	}
}
