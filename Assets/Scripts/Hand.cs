using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	[Tooltip("The axis ID of this hand's grip button.")]
	[SerializeField]
	private string m_gripID;
	
	[Tooltip("The object to spawn at this hand, if any.")]
	[SerializeField]
	private GameObject m_block;

	private Dictionary<int, Transform> m_heldObjects;

	//TODO: Remove testing code.
	string m_debugText;

	private void Start()
	{
		m_heldObjects = new Dictionary<int, Transform>();
	}

	private void OnTriggerStay(Collider other)
	{
		int id = other.GetInstanceID();

		// Grab any objects touching the hand if the grip button is pressed and 
		// the objects have rigidbody components.
		if(Input.GetAxis(m_gripID) >= 1
			&& other.gameObject.GetComponent<Collider>() != null
			&& !m_heldObjects.ContainsKey(id))
		{
			// Make the grabbed object move with this hand.
			other.transform.parent = transform;
			// Remember that we're holding this object.
			m_heldObjects.Add(other.GetInstanceID(), other.transform);
			// Have the grabbed object stop reponding to physics.
			other.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	private void FixedUpdate()
	{
		//_____________________TODO: Remove test code._____________________
		m_debugText = "Left: " + Input.GetAxis("Grip - Left").ToString();
		m_debugText += "\nRight: " + Input.GetAxis("Grip - Right").ToString();

		if(m_gripID == "Grip - Right")
		{
			m_debugText += "\nR Pos: " + transform.position.ToString();
			
			if(m_heldObjects.Count > 0)
			{
				var enumerator = m_heldObjects.GetEnumerator();
				enumerator.MoveNext();
				m_debugText += "\nR Vel: " +
					enumerator.Current.Value.GetComponent<Rigidbody>().velocity.ToString();
			}
		}
		
		DebugMessenger.instance.SetDebugText(m_debugText);
		//_____________________TODO: Remove test code._____________________


		// Release held objects when the grip button is released.
		if(Input.GetAxis(m_gripID) < 1)
		{
			foreach(KeyValuePair<int, Transform> pair in m_heldObjects)
			{
				// Drop the object if it is still held by this hand.
				if (pair.Value.parent == transform)
				{	
					// Stop the held object from moving with the hand.
					pair.Value.parent = null;
					// Have the grabbed object start responding to physics.
					pair.Value.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
			// Clear the list of held objects since we've dropped them all.
			m_heldObjects.Clear();
		}

		// Spawn blocks.
		if(m_block != null && Input.GetButtonDown("Fire1"))
		{
			GameObject.Instantiate(m_block, transform.position, transform.rotation);
		}
	}
}
