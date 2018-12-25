using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	[Tooltip("The axis ID of this hand's grip button.")]
	[SerializeField]
	private string gripID;
	
	[Tooltip("The object to spawn at this hand, if any.")]
	[SerializeField]
	private GameObject block;

	private Dictionary<Guid, Transform> heldObjects;


	private void Start()
	{
		heldObjects = new Dictionary<Guid, Transform>();
	}

	private void OnTriggerStay(Collider other)
	{
		ID id = other.GetComponent<ID>();

		//Grab any objects touching the hand if the grip button is pressed and 
		// the objects have rigidbody components.
		if(Input.GetAxis(gripID) > 0
			&& id != null
			&& other.gameObject.GetComponent<Collider>() != null
			&& !heldObjects.ContainsKey(id.guid))
		{
			//Debug.Log("Attempted to grab with " + gripID);
			//Make the grapped object snap to this hand.
			//collision.transform.position = transform.position;

			//Make the grabbed object move with this hand.
			other.transform.parent = transform;
			//Remember that we're holding this object.
			heldObjects.Add(other.GetComponent<ID>().guid, other.transform);
			//Disable gravity on the grabbed object.
			//other.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
			//Have the grabbed object stop reponding to physics.
			other.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	private void Update()
	{
		//if(Input.GetAxis(gripID) != 0)
			//Debug.Log(gripID + ": " + Input.GetAxis(gripID));

		//Release held objects when the grip button is released.
		if(Input.GetAxis(gripID) == 0)
		{
			foreach(KeyValuePair<Guid, Transform> pair in heldObjects)
			{
				//Stop the held object from moving with the hand.
				pair.Value.parent = null;
				//Enable gravity on the grabbed object.
				//pair.Value.GetComponent<Rigidbody>().useGravity = true;
				//Have the grabbed object start reponding to physics.
				pair.Value.GetComponent<Rigidbody>().isKinematic = false;
			}
			//Clear the list of held objects since we've dropped them all.
			heldObjects.Clear();
		}

		//Spawn blocks.
		if(block != null && Input.GetButtonDown("Fire1"))
		{
			GameObject.Instantiate(block, transform.position, transform.rotation);
		}
	}
}
