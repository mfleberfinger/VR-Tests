using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple script to drop paintings if something collides with them.
// Assumes that all paintings start with rotation and translation frozen on all
// axes and unfreezes on all axes when something collides with the painting.
public class PaintingDropper : MonoBehaviour
{
	[Tooltip("This painting's rigidbody.")]
	[SerializeField]
	private Rigidbody rb = null;

	private Vector3 initialPosition;

	private void Start()
	{
		initialPosition = transform.position;
	}

	private void Update()
	{
		// Drop painting if moved.
		if (Mathf.Abs(Vector3.Magnitude(transform.position - initialPosition)) > 0.05f)
			rb.constraints = RigidbodyConstraints.None;
	}

	private void OnCollisionEnter(Collision collision)
	{
		// Drop painting on collision.
		if(collision.gameObject.tag != "HouseStructure")
			rb.constraints = RigidbodyConstraints.None;
	}
}
