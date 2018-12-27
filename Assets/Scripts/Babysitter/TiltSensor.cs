using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltSensor : MonoBehaviour
{
	[Tooltip("Player is penalized if the object rotates further than this many" +
		" degrees on any axis.")]
	[SerializeField]
	private float maxRotation = 45f;
	[Tooltip("Amount of money lost by the player if this breaks.")]
	[SerializeField]
	private float priceTag = 1f;

	// The starting rotation of the object to which this script is attached.
	private float m_correctX, m_correctY, m_correctZ;
	// Has this item been disturbed too much?
	private bool m_fallen;
	//// Describes whether we assume that this item stopped moving after the initial
	//// scene load and physics settling?
	//private bool m_settled;
	//private float m_settleTimer;
	
	// Net rotation on each axis since the start of play.
	float rotX, rotY, rotZ;

    void Start()
    {
		//m_settleTimer = 0f;
		//m_settled = false;
		rotX = rotY = rotZ;
		m_fallen = false;
        m_correctX = transform.rotation.eulerAngles.x;
		m_correctY = transform.rotation.eulerAngles.y;
		m_correctZ = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
		//// Give the object a moment to stop moving after the scene loads.
		//if (!m_settled)
		//{
		//	m_settleTimer += Time.deltaTime;

		//	if (m_settleTimer > 2f)
		//		m_settled = true;
		//}
		
		////TODO: remove test code
		//if (!m_settled && gameObject.name.Contains("RFAIPP_Lamp"))
		//{
		//	Debug.Log("angle: " + transform.rotation.eulerAngles.ToString());
		//	if(m_settled)
		//		Debug.Log("Settled");
		//}

		if (!m_fallen/* && m_settled*/)
		{
			

			if (true)
			{
				//TODO: Take points and play a sound (glass breaking? laughing child?).
				Debug.Log("Item broken: " + gameObject.name + " " + "Cost: $" + priceTag);
				m_fallen = true;
			}
		}
    }

	
}
