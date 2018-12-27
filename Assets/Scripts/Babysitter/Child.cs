using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Controls the children in the babysitter game.
public class Child : MonoBehaviour
{
	[Tooltip("Child applies a force of this magnitude to \"Breakable\" items.")]
	[SerializeField]
	private float forceMagnitude = 10f;

	private static Queue<TiltSensor> m_tiltSensors;

	private NavMeshAgent m_agent;

	private void Start()
	{
		if (m_tiltSensors == null)
			m_tiltSensors = new Queue<TiltSensor>();

		m_agent = GetComponent<NavMeshAgent>();

		// Initialize the destination value of NavMesh to allow us to check it
		// properly in the if statement in Update().
		m_agent.destination = transform.position;
	}

	private void Update()
	{
		// TiltSensors will identify themselves to Child. Choose the next
		// one and pathfind to it. Once it is broken, choose the next one
		// available. Repeat until out of things to break.
		if (Mathf.Approximately(transform.position.x, m_agent.destination.x) &&
			Mathf.Approximately(transform.position.z, m_agent.destination.z) &&
			m_tiltSensors.Count > 0)
		{
			m_agent.destination = m_tiltSensors.Dequeue().transform.position;
		}
		else if (m_tiltSensors.Count == 0)
			m_agent.enabled = false;
	}

	/// <summary>
	/// Add a TiltSensor reference to the Child class so that all objects of
	/// Child can identify and pathfind to the TiltSensor's transform. 
	/// </summary>
	/// <param name="tiltSensor">The tilt sensor to add to the static queue.</param>
	public static void AddTiltSensor(TiltSensor tiltSensor)
	{
		if (m_tiltSensors == null)
			m_tiltSensors = new Queue<TiltSensor>();

		m_tiltSensors.Enqueue(tiltSensor);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Breakable")
			other.GetComponent<Rigidbody>().
				AddForce(Random.insideUnitSphere * forceMagnitude, ForceMode.Impulse);
	}
}
