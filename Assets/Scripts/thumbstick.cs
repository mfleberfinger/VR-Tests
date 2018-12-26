using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the player to move the XR rig with the thumbsticks on the controllers.
public class thumbstick : MonoBehaviour
{
    [Tooltip("Thumbstick to use for X movement")]
    [SerializeField]
    private string primaryStickIDX = "null";
    [Tooltip("Thumbstick to use for Z movement")]
    [SerializeField]
    private string primaryStickIDY = "null";
    [SerializeField]
    private string secondaryStickIDX = "null";
    [Tooltip("Left Hand to Normalize Movement Against")]
    [SerializeField]
    GameObject leftHand = null;
	[Tooltip("Multiplier/coefficient to control player movement speed.")]
    [SerializeField]
	private float translationScaleFactor = 0.04f;
	[Tooltip("Multiplier/coefficient to control player movement speed.")]
    [SerializeField]
	private float rotationSnapAngle = 30f;
    [SerializeField]
    private float rotationSnapTimer = .22f;

    private float m_rotationCooldown = 0.0f;

    void Update()
    {
		// Translation
        float fwdInput = -Input.GetAxis(primaryStickIDY) * translationScaleFactor;
        float sideInput = -Input.GetAxis(primaryStickIDX) * translationScaleFactor;
        Vector3 thisMove = new Vector3(0, 0, 0);
        Vector3 fwd = leftHand.transform.forward;
        fwd.y = 0;
        fwd.Normalize();
        Vector3 right = Vector3.Cross(fwd, new Vector3(0, 1, 0));
        thisMove += fwdInput * fwd;
        thisMove += sideInput * right;
        transform.position += thisMove;

        // Rotation
        float rotInput = Input.GetAxis(secondaryStickIDX);
        if (Mathf.Abs(rotInput) > .5f && m_rotationCooldown > rotationSnapTimer)
        {
            transform.Rotate(Vector3.up, Mathf.Sign(rotInput)*rotationSnapAngle);
            m_rotationCooldown = 0;
        }
        m_rotationCooldown += Time.deltaTime;
    }
}
