using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to make a collider attached to the XR Rig size and position itself
// such that the top of the collider is at the top of the player's head and the
// bottom of the collider is at the player's feet.
public class PlayerCollider : MonoBehaviour
{
	[Tooltip("The player/XR Rig's collider.")]
	[SerializeField]
	private Collider collider = null;
}
