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
	private float rotationSnapAngle = 40f;
	[Tooltip("Time that the player must wait between rotation increments.")]
    [SerializeField]
    private float rotationSnapCooldown = .35f;
	[Tooltip("XR Rig's rigidbody.")]
    [SerializeField]
	private Rigidbody rb = null;

    private float m_rotationCooldownTimer = 0.0f;
	private float m_fwdInput, m_sideInput;
	private Vector3 m_fwd;
	

	private void Update()
	{
		// Translation
		m_fwdInput = -Input.GetAxis(primaryStickIDY) * translationScaleFactor;
        m_sideInput = -Input.GetAxis(primaryStickIDX) * translationScaleFactor;

		// Rotation
		m_fwd = leftHand.transform.forward;
        m_fwd.y = 0;
        m_fwd.Normalize();
	}

	void FixedUpdate()
    {
		// Translation
        Vector3 thisMove = new Vector3(0, 0, 0);
        Vector3 right = Vector3.Cross(m_fwd, new Vector3(0, 1, 0));
        thisMove += m_fwdInput * m_fwd;
        thisMove += m_sideInput * right;
        rb.MovePosition(transform.position + thisMove);

        // Rotation
        float rotInput = Input.GetAxis(secondaryStickIDX);
        if (Mathf.Abs(rotInput) > .5f && m_rotationCooldownTimer > rotationSnapCooldown)
        {
            //transform.Rotate(Vector3.up, Mathf.Sign(rotInput) * rotationSnapAngle);
			Quaternion rot = new Quaternion();
			rot.eulerAngles = new Vector3(0, Mathf.Sign(rotInput) * rotationSnapAngle)
				+ rb.rotation.eulerAngles;
			rb.rotation = rot;
            m_rotationCooldownTimer = 0;
        }
        m_rotationCooldownTimer += Time.deltaTime;
    }
}
