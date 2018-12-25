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

	private Dictionary<int, Transform> heldObjects;

	//TODO: Remove testing code.
	string axisValues;

	private void Start()
	{
		heldObjects = new Dictionary<int, Transform>();
	}

	private void OnTriggerStay(Collider other)
	{
		int id = other.GetInstanceID();

		// Grab any objects touching the hand if the grip button is pressed and 
		// the objects have rigidbody components.
		if(Input.GetAxis(gripID) >= 1
			&& other.gameObject.GetComponent<Collider>() != null
			&& !heldObjects.ContainsKey(id))
		{
			// Make the grabbed object move with this hand.
			other.transform.parent = transform;
			// Remember that we're holding this object.
			heldObjects.Add(other.GetInstanceID(), other.transform);
			// Have the grabbed object stop reponding to physics.
			other.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	private void FixedUpdate()
	{
		//TODO: Remove test code.
		axisValues = "Left: " + Input.GetAxis("Grip - Left").ToString();
		axisValues += "\nRight: " + Input.GetAxis("Grip - Right").ToString();
		DebugMessenger.instance.SetDebugText(axisValues);

		// Release held objects when the grip button is released.
		if(Input.GetAxis(gripID) < 1)
		{
			foreach(KeyValuePair<int, Transform> pair in heldObjects)
			{
				// Drop the object if it is still held by this hand.
				if (pair.Value.parent == transform)
				{	
					// Stop the held object from moving with the hand.
					pair.Value.parent = null;
					// Have the grabbed object start reponding to physics.
					pair.Value.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
			// Clear the list of held objects since we've dropped them all.
			heldObjects.Clear();
		}

		// Spawn blocks.
		if(block != null && Input.GetButtonDown("Fire1"))
		{
			GameObject.Instantiate(block, transform.position, transform.rotation);
		}
	}
}
