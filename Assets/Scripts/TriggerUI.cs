using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerUI : MonoBehaviour
{
	public GameObject _triggerCanvas;
	public TextMeshProUGUI message;
//	public GameObject sourceObject;

	private void OnTriggerEnter(Collider other)
	{
		_triggerCanvas.SetActive(true);
		message.text = gameObject.name;
	}
	private void OnTriggerExit(Collider other)
	{
		_triggerCanvas.SetActive(false);
	}
}
