using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the player to move the XR rig with the thumbsticks on the controllers.
public class thumbstick : MonoBehaviour
{
    [Tooltip("Thumbstick to use for X movement")]
    [SerializeField]
    private string thumbStickIDX = "null";
    [Tooltip("Thumbstick to use for Z movement")]
    [SerializeField]
    private string thumbStickIDY = "null";
    [Tooltip("Left Hand to Normalize Movement Against")]
    [SerializeField]
    GameObject leftHand = null;
	[Tooltip("Multiplier/coefficient to control player movement speed.")]
    [SerializeField]
	private float translationScaleFactor = 0.04f;
	[Tooltip("Multiplier/coefficient to control player movement speed.")]
    [SerializeField]
	private float rotationSnapAngle = 30f;

    void Update()
    {
		// Translation
        float fwdInput = -Input.GetAxis(thumbStickIDY) * translationScaleFactor;
        float sideInput = -Input.GetAxis(thumbStickIDX) * translationScaleFactor;
        Vector3 thisMove = new Vector3(0, 0, 0);
        Vector3 fwd = leftHand.transform.forward;
        fwd.y = 0;
        fwd.Normalize();
        Vector3 right = Vector3.Cross(fwd, new Vector3(0, 1, 0));
        thisMove += fwdInput * fwd;
        thisMove += sideInput * right;
        transform.position += thisMove;

		// Rotation
    }
}
