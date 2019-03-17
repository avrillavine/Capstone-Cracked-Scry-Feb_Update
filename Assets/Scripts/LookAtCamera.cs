using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	public Camera SourceCamera;
	public GameObject sourceObject;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 pos = transform.position;

		transform.rotation = SourceCamera.transform.rotation;
		pos = sourceObject.transform.position;
		pos.y = sourceObject.transform.position.y + 4;
	}
}
