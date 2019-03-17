using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Anim : MonoBehaviour
{
	Animator anim;
	//right palm bone slot
	public GameObject rPalmSlot;
	public bool _hasRightHandObject = false;

	//left palm bone slot
	public GameObject lPalmSlot;
	public bool _hasLeftHandObject = false;


	//Stomach bone slot
	public GameObject beltSlot;
	
	//magnifying glass
	public GameObject mag;

	//Pickupable items
	public GameObject _rock;
	GameObject _rockClone;
	public float _mass = 1.0f;

	//Lantern light source and mesh
	public GameObject lantern;


	// Start is called before the first frame update

	public float _throwForce = 10.0f;
	
	void Start()
    {
		_rockClone = new GameObject();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	public void Update()
	{
		//Activates Left-Right Movement
		anim.SetFloat("hspeed", Input.GetAxis("Horizontal"));
		//Activates Walking from Idle
		anim.SetFloat("vspeed", Input.GetAxis("Vertical"));

		//Jump
		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool("jumping", true);
			Invoke("StopJumping", 0.1f);
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			if(!_hasRightHandObject)
			{
				anim.Play("Look");
				Grab_Mag();
				Slot_Mag();
			}
			else
			{
				NotPossible();
			}
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			if(!_hasRightHandObject)
			{
				anim.Play("PickUp");
			}
			else
			{
				NotPossible();
			}
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			if(_hasRightHandObject)
			{
				anim.Play("rThrow");
				OnThrow();
			}
			else
			{
				NotPossible();
			}
			
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			if(!_hasLeftHandObject)
			{
				//anim.Play("lHand_Grip_Raise_Arm", 2);
				anim.Play("lHand_Grip_Raise_Arm");
				TakeLantern();
			}
			else
			{
				NotPossible();
			}

		}
		if (_hasRightHandObject)
		{
			anim.SetLayerWeight(anim.GetLayerIndex("RHand Grip Layer"), 1.0f);
		}
		else
		{
			anim.SetLayerWeight(1, 0.0f);
		}
		if(_hasLeftHandObject)
		{
			anim.SetLayerWeight(anim.GetLayerIndex("LHand Grip Raise Arm Layer"), 1.0f);
		}
		else
		{
			anim.SetLayerWeight(2, 0.0f);
		}
	}

	void LateUpdate()
	{
		//	//	//Animator animator = GetComponent<Animator>();
		//	//	AnimatorStateInfo currentAnimatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
		//	//	float playbackTime = currentAnimatorStateInfo.normalizedTime * currentAnimatorStateInfo.length;
		//	//	if (currentAnimatorStateInfo.IsName("Look") && playbackTime > 0.5f)
		//	//	{
		//	//		mag.transform.position = rPalmHolder.transform.position;
		//	//	}
		//	Grab_Mag();

	}
	//void OnTriggerEnter(Collider other)
	//{
	//	if (other.gameObject.tag == "Carryable")
	//	{
	//		Debug.Log(other.gameObject.name);
	//	}
	//}
	//void OnTriggerStay(Collider other)
	//{

	//	if (other.gameObject.tag == "Carryable" && !_hasLeftHandObject)
	//	{

	//		//var otherTransform = other.gameObject.transform;
	//		//otherTransform.parent = lPalmSlot.transform;
	//		//otherTransform.localPosition = Vector3.zero;
	//		//otherTransform.localRotation = Quaternion.identity;
	//		//otherTransform.localScale = Vector3.one;
	//		//other.GetComponent<Rigidbody>().useGravity = false;
	//		//other.GetComponent<Rigidbody>().isKinematic = true;
	//			if (Input.GetKeyDown(KeyCode.B))
	//			{
	//				TakeLantern();
	//			}

	//	}
	//	else
	//	{
	//		if (Input.GetKeyDown(KeyCode.B))
	//		{
	//			NotPossible();
	//		}
			
	//	}
	//}
	void StopJumping()
	{
		anim.SetBool("jumping", false);
	}
	//On frame 5 of the look animation the players hand will be by their belt
	void Grab_Mag()
	{
		Debug.Log("Grab_Mag");
		//Creates a variable based on the magnifying glass's current location
		//and reparents it to the hand
		var magTransform = mag.transform;
		magTransform.parent = rPalmSlot.transform;
		magTransform.localPosition = Vector3.zero;
		magTransform.localRotation = Quaternion.AngleAxis(270.0f, Vector3.forward);
		magTransform.localScale = Vector3.one;
		
		//TODO Add shader references 
	}
	//Animation event on frame 37 places magnifying glass object back
	void Slot_Mag()
	{
		Debug.Log("Slot_Mag");
		var magTransform = mag.transform;
		magTransform.parent = beltSlot.transform;
		magTransform.localPosition = Vector3.zero;
		magTransform.localRotation = Quaternion.identity;
		magTransform.localScale = Vector3.one;
	}
	void Pickup_Obj()
	{
		Debug.Log("Got a rock from the pile");
		_rockClone = Instantiate(_rock);
		_rockClone.GetComponent<Rigidbody>().useGravity = false;
		_rockClone.GetComponent<Rigidbody>().isKinematic = true;
		var _rc = _rockClone.transform;
		_rc.parent = rPalmSlot.transform;
		_rc.localPosition = Vector3.zero;
		_rc.localRotation = Quaternion.identity;
		_rc.localScale = new Vector3(.5f, .5f, .5f);
		//Player now has a rock on hand
		_hasRightHandObject = true;

		//Prevents Player from picking up second rock
		//Note: Theoretically I could add in a second animation that is 
		//a mirror image version of the right hand pick up (as in left hand pickup)
		//but I'm planning on leaving the left hand to primarily pickup and grip lanterns

	}
	void OnThrow()
	{
		//If player is holding rock they can throw it
		var _rc = _rockClone.transform;
		_rc.parent = null;
		Rigidbody rb = _rockClone.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.isKinematic = false;
		rb.mass = _mass;
		Vector3 fwd = _rc.up;
		//rb.AddRelativeForce(transform.forward*_throwForce, ForceMode.Impulse);
		rb.AddForceAtPosition(transform.forward*_throwForce, _rc.position, ForceMode.Impulse);
		_hasRightHandObject = false;
		//If not the character will shake their head instead
	}
	//void OnTriggerEnter(Collider other)
	//{
	//	Debug.Log(other.gameObject.name);
	//	if (other.gameObject.tag == "Carryable")
	//	{
	//		Debug.Log(other.gameObject.name);
	//		var otherTransform = other.gameObject.transform;
	//		otherTransform.parent = rPalmSlot.transform;
	//		otherTransform.localPosition = Vector3.zero;
	//		otherTransform.localRotation = Quaternion.AngleAxis(270.0f, Vector3.forward);
	//		otherTransform.localScale = Vector3.one;
	//		other.gameObject.SetActive(false);
	//	}
	//}

	void TakeLantern()
	{
		var lTransform = lantern.transform;
		lTransform.parent = lPalmSlot.transform;
		lTransform.localPosition = Vector3.zero;
		lTransform.localRotation = Quaternion.Euler(180.0f, 45.0f, 0.0f);
		lTransform.localScale = Vector3.one;
		Rigidbody childRB = lTransform.GetComponentInChildren<Rigidbody>();
		childRB.isKinematic = true;
		childRB.useGravity = false;
		Rigidbody rb = lantern.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.useGravity = false;
		_hasLeftHandObject = true;
	}
	//Animation of the player character shaking their head will play
	//if player attempts an action the character cannot do based on 
	//whether the player character is holding or not holding an item
	void NotPossible()
	{
		anim.Play("No");
	}
}
