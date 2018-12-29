using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discipliner : MonoBehaviour
{
	// This must be a trigger because two kinematic rigidbodies won't detect
	// collisions with each other.
	private void OnTriggerEnter(Collider col)
	{
		Child m_child = col.gameObject.GetComponent<Child>();

		// If this collides with a child, hit the child.
		if (m_child != null)
			m_child.Hit();
	}
}
