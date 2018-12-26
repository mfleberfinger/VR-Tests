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
	private Transform colliderTransform = null;
	[Tooltip("The XR Rig's Floor Offset.")]
	[SerializeField]
	private Transform floor = null;
	[Tooltip("The XR Rig's camera.")]
	[SerializeField]
	private Transform camTransform = null;

	// For saving the current x and z scale of the collider.
	private float m_xScale, m_zScale;

	private void FixedUpdate()
	{
		// Place the collider's center halfway between the real-world floor
		// and the player's headset.
		float yPos = (camTransform.localPosition.y - floor.localPosition.y) * 0.5f;
		colliderTransform.localPosition = new Vector3(camTransform.localPosition.x,
			yPos, camTransform.localPosition.z);

		// Scale the collider's height to be equal to the current distance
		// between the floor and the headset unless that size is smaller than
		// some minimum.
		m_xScale = colliderTransform.localScale.x;
		m_zScale = colliderTransform.localScale.z;
		colliderTransform.localScale =
			new Vector3(m_xScale,
				camTransform.localPosition.y - floor.localPosition.y, m_zScale);
		
		if(colliderTransform.localScale.y < 0.1f)
			colliderTransform.localScale = new Vector3(m_xScale, 0.1f, m_zScale);
	}
}
