using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Based off of code from here https://answers.unity.com/questions/171492/how-to-make-a-pause-menu-in-c.html
public class Pause : MonoBehaviour
{
	bool paused = false;
	public Movement _movementScript;
	public Canvas pauseMenu;
	void Start()
	{
		//_movementScript = _movementScript.GetComponent<Movement>();
		pauseMenu = pauseMenu.GetComponent<Canvas>();
		pauseMenu.enabled = false;
		_movementScript.enabled = true;
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//Pause2();
			paused = TogglePause();
			//UnPause();
			//_movementScript.enabled = false;
			//pauseMenu.enabled = true;
			//paused = true;
		}
		//TODO Add return to menu button
	}

	//void OnGUI()
	//{
	//	if (paused)
	//	{
	//		GUILayout.Label("Game is paused!");
	//		if (GUILayout.Button("Click me to unpause"))
	//			paused = TogglePause();
	//	}
	//}

	bool TogglePause()
	{
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			_movementScript.enabled = true;
			pauseMenu.enabled = false;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
			return (false);
		}
		else
		{
			Time.timeScale = 0f;
			_movementScript.enabled = false;
			pauseMenu.enabled = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			return (true);
		}
	}


}
