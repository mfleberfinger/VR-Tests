using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to aid in finding proper orientation of objects in 3D space by binding
// the object to a VR controller's transform.
public class OrientationTester : MonoBehaviour
{
	[Tooltip("The transform of the hand to follow.")]
	[SerializeField]
	private Transform controller = null;

	//[Tooltip("Scales the amount of tranform movement of the object under test" +
	//	" with respect to the controller.")]
	//[SerializeField]
	//private float transformScaler = 1f;

	private Vector3 m_prevControllerPos;

	private void Start()
	{
		m_prevControllerPos = controller.position;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			transform.localPosition = Vector3.zero;

		transform.Translate(controller.position - m_prevControllerPos, Space.World);
		transform.rotation = controller.rotation;
		
		m_prevControllerPos = controller.position;

		if (transform.localPosition.x > 10000 || 
				transform.localPosition.y > 10000 ||
				transform.localPosition.z > 10000)
				transform.localPosition = Vector3.zero;

		DebugMessenger.instance.SetDebugText(string.Format(
			"localPosition: {0}\n" +
			"localRotation: {1}\n" +
			"position: {2}\n" +
			"rotation: {3}", transform.localPosition.ToString(),
			transform.localRotation.eulerAngles.ToString(),
			transform.position.ToString(),
			transform.rotation.eulerAngles.ToString()));
	}
}
