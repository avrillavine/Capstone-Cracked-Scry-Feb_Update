using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BearTrap : MonoBehaviour
{
	Animator anim;
	AudioSource _audio;
	public bool _isTriggered = false;
	public AudioClip shut_sound;
	public AudioClip open_sound;

	//Damage related variables
	GameObject _player;
	bool _setOffByPlayer;
	//Player's health script
	Health _health;
	public int _damage = 10;
	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		_audio = GetComponent<AudioSource>();
		_player = GameObject.FindGameObjectWithTag("Player");
		_health = _player.GetComponent<Health>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!_isTriggered)
		{
			anim.SetBool("Set", true);
			anim.SetBool("Triggered", false);
		}
		if(_isTriggered)
		{
			anim.SetBool("Set", false);
			anim.SetBool("Triggered", true);
		}
		// If the player has zero or less health...
		//if (_health.currentHealth <= 0)
		//{
		//	// ... tell the animator the player is dead.
		//	//anim.SetTrigger("PlayerDead");
		//	Debug.Log("Add player death state");
		//}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Rock")
		{
			
			anim.SetTrigger("shut");
			_isTriggered = true;
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			anim.SetTrigger("open");
			_isTriggered = false;
		}
	}
	void OnTriggerExit(Collider other)
	{
	}
	//Animation related events 
	void On_Shut()
	{
		_audio.clip = shut_sound;
		_audio.Play();
	}

	void On_Open()
	{
		_audio.clip = open_sound;
		_audio.Play();
	}

	//Player takes damage
	//void DmgPlayer()
	//{

	//	// If the player has health to lose...
	//	if (_health.currentHealth > 0)
	//	{
	//		// ... damage the player.
	//		_health.TakeDamage(_damage);
	//	}
	//}


}
