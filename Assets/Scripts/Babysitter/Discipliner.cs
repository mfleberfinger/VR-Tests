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

		// If this collides with a child while held by the player, hit the child.
		if (gameObject.layer == 9 && m_child != null)
			m_child.Hit();
	}
}
