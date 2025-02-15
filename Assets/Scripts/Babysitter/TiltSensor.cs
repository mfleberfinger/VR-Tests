﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Penalizes the player if the thing this is attached to translates or rotates
// too far.
public class TiltSensor : MonoBehaviour
{
	[Tooltip("Player is penalized if the object rotates further than this many" +
		" degrees on any axis.")]
	[SerializeField]
	private float maxRotation = 45f;
	[Tooltip("Player is penalized if the object translates further than this.")]
	[SerializeField]
	private float maxTranslation = 0.5f;
	[Tooltip("Amount of money lost by the player if this breaks.")]
	[SerializeField]
	private float priceTag = 1f;

	// The starting rotation of the object to which this script is attached.
	private float m_correctXRot, m_correctYRot, m_correctZRot;
	
	// The starting position of the object to which this script is attached.
	private Vector3 m_correctPosition;

	// Used to deduct pay for broken items.
	private ScoreKeeper score;

	/// <summary>
	/// True if the object has moved beyond the maximum allowed translation or
	/// rotation.
	/// </summary>
	public bool m_fallen { get; private set; }

	private void Awake()
	{
		Child.AddTiltSensor(this);
	}

	private void Start()
    {

		GameObject scoreGO = GameObject.FindGameObjectWithTag("GameController");
		
		if (scoreGO != null)
			score = scoreGO.GetComponent<ScoreKeeper>();
		else
			Debug.LogError("No GameObject with the tag \"GameController\" could be found.");

		if (maxRotation > 90)
			Debug.LogError("MaxRotation must be between 0 and 90");

		m_fallen = false;
        m_correctXRot = transform.rotation.eulerAngles.x;
		m_correctYRot = transform.rotation.eulerAngles.y;
		m_correctZRot = transform.rotation.eulerAngles.z;
		m_correctPosition = transform.position;
    }

    private void Update()
    {
		if (!m_fallen)
		{
			// Calculate the difference between the starting rotation and the
			// current rotation on each axis.
			float deltaX = 
				Mathf.Abs(Mathf.DeltaAngle(m_correctXRot, transform.rotation.eulerAngles.x));
			float deltaY =
				Mathf.Abs(Mathf.DeltaAngle(m_correctYRot, transform.rotation.eulerAngles.y));
			float deltaZ =
				Mathf.Abs(Mathf.DeltaAngle(m_correctZRot, transform.rotation.eulerAngles.z));
			
			if(deltaX > maxRotation || deltaY > maxRotation || deltaZ > maxRotation
				|| Vector3.Magnitude(transform.position - m_correctPosition) > maxTranslation)
			{
				//TODO: Take points and play a sound (glass breaking? laughing child?).
				Debug.Log("Item broken: " + gameObject.name + " " + "Cost: $" + priceTag);
				score.DockPay(priceTag);
				m_fallen = true;
			}
		}
    }
}
