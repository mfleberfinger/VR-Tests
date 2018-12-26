using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	[Tooltip("The axis ID of this hand's grip button.")]
	[SerializeField]
	private string m_gripID = "";
	
	[Tooltip("The object to spawn at this hand, if any.")]
	[SerializeField]
	private GameObject m_block = null;

    // States the hand can be in
    enum HandState : byte {Empty, Hold};
    HandState m_State = HandState.Empty;

	// Number of data points to use for the velocity moving average.
	private const int m_TicksToAverage = 10;
	// Multiply the velocity imparted to a thrown object to make it "feel right."
	private const float m_ThrowMultiplier = 2.5f;

	// The currently held object (if any).
	private Transform m_heldObject;
	
	// The velocity of this hand calculated based on position and fixedDeltaTime.
	private Vector3 m_velocity;
	// Position at the end of the last FixedUpdate.
	private Vector3 m_lastPosition;
	// We will track a moving average of velocity over some number of fixed updates.
	private Vector3 m_sumOfVelocities;
	private Queue<Vector3> m_velocities;

	//TODO: Remove testing code.
	string m_debugText;

	private void Start()
	{
		m_velocities = new Queue<Vector3>();
		
		for (int i = 0; i < m_TicksToAverage; i++)
			m_velocities.Enqueue(Vector3.zero);

		m_heldObject = null;
		m_velocity = Vector3.zero;
	}

	private void OnTriggerStay(Collider other)
	{
		// Grab any objects touching the hand if the grip button is pressed and 
		// the objects have rigidbody components.
		if (m_State == HandState.Hold
			&& other.gameObject.GetComponent<Rigidbody>() != null
			&& m_heldObject == null)
		{
			// Make the grabbed object move with this hand.
			other.transform.parent = transform;
			// Remember that we're holding this object.
			m_heldObject = other.transform;
			// Have the grabbed object stop responding to physics.
			other.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

    private void Update()
    {
        float rawInput = Input.GetAxisRaw(m_gripID);
        if (rawInput < 1)
        {
            m_State = HandState.Empty;
        }
        else
        {
            m_State = HandState.Hold;
        }
        if (Input.GetButtonDown("Fire1"))
        {
			GameObject.Instantiate(m_block, transform.position, transform.rotation);
        }
    }

	private void FixedUpdate()
	{
		//_____________________TODO: Remove test code._____________________
		if (m_gripID == "Grip - Right")
		{
			m_debugText =	"\nR Axis: " + Input.GetAxis(m_gripID).ToString();
			m_debugText +=	"\nR Pos: " + transform.position.ToString();
			m_debugText +=	"\nR Vel: " + m_velocity.ToString();
			m_debugText +=	"\nDelta:" + Time.fixedDeltaTime.ToString();

			DebugMessenger.instance.SetDebugText(m_debugText);
		}
		//_____________________TODO: Remove test code._____________________

		//Calculate velocity and update data for the moving average.
		m_velocity = (transform.position - m_lastPosition) / Time.fixedDeltaTime;
		m_sumOfVelocities += m_velocity;
		m_sumOfVelocities -=  m_velocities.Dequeue();
		m_velocities.Enqueue(m_velocity);

		// Release any held object when the grip button is released.
		if (m_State == HandState.Empty)
		{
			if (m_heldObject != null)
			{
				Rigidbody rigidbody = m_heldObject.GetComponent<Rigidbody>();
				// Release the object if it is still held by this hand.
				if (m_heldObject.parent == transform)
				{	
					// Stop the held object from moving with the hand.
					m_heldObject.parent = null;
					// Have the grabbed object start responding to physics.
					rigidbody.isKinematic = false;
					//Apply the hand's recent average velocity to the object.
					rigidbody.velocity =
						(m_sumOfVelocities / m_TicksToAverage) * m_ThrowMultiplier;
				}

				m_heldObject = null;
			}
		}

		m_lastPosition = transform.position;
	}
}
