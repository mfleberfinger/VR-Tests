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

	[Tooltip("Time for which the child stays still after being hit, in seconds.")]
	[SerializeField]
	private float stunTime = 10f;

	[Tooltip("Child's SoundManager script component.")]
	[SerializeField]
	private SoundManager soundManager = null;

	[Tooltip("Sound to play when hit by the player.")]
	[SerializeField]
	private AudioClip hitSound = null;

	[Tooltip("Animator for this character.")]
	[SerializeField]
	private Animator anim = null;

	[Tooltip("Clips to choose from to play when something breaks.")]
	public AudioClip[] breakSounds;



	private static Queue<TiltSensor> m_tiltSensors;

	private NavMeshAgent m_agent;
	private float m_stunCountdown;

	private void Start()
	{
		if (m_tiltSensors == null)
			m_tiltSensors = new Queue<TiltSensor>();

		m_agent = GetComponent<NavMeshAgent>();

		// Initialize the destination to current position.
		m_agent.destination = transform.position;
		// Disable automatic NavMesh navigation.
		m_agent.updatePosition = false;
		m_agent.updateRotation = false;
		m_stunCountdown = 0f;
		anim.SetFloat("Speed", 0.5f);
	}

	// TiltSensors will identify themselves to Child. Choose the next
	// one and pathfind to it. Once it is broken, choose the next one
	// available. Repeat until out of things to break.
	private void Update()
	{
		if(Mathf.Approximately(transform.position.x, m_agent.destination.x) &&
			Mathf.Approximately(transform.position.z, m_agent.destination.z))
		{
			if (m_tiltSensors.Count > 0)
				m_agent.destination = m_tiltSensors.Dequeue().transform.position;
		}

		if (m_stunCountdown > 0)
			m_stunCountdown -= Time.deltaTime;

		// Get vector to the next NavMeshAgent position and set the vertical
		// part to 0.
		Vector3 pos = transform.position;
		pos = new Vector3(pos.x, 0f, pos.z);
		Vector3 next = m_agent.nextPosition;
		next = new Vector3(next.x, 0f, next.z);
		Vector3 heading = pos - next;

		// Get our forward vector and set the vertical part to 0.
		Vector3 forward = transform.forward;
		forward = new Vector3(forward.x, 0f, forward.z);

		// Get the angle between the forward vector and the heading vector and
		// set the steering variable proportionately.
		float angle = Vector3.SignedAngle(forward, heading, Vector3.up);
		
		anim.SetFloat("Turn", -(angle / 180));
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
		{
			// Play sound if tipping something that was previously undamaged.
			if(!other.GetComponent<TiltSensor>().m_fallen)
				soundManager.RandomizeSfx(breakSounds);

			other.GetComponent<Rigidbody>().
				AddForce(Random.insideUnitSphere * forceMagnitude, ForceMode.Impulse);
		}
	}

	/// <summary>
	/// Beats the child (with love).
	/// When this is called the child will be temporarily stunned and can be
	/// carried.
	/// </summary>
	public void Hit()
	{
		soundManager.PlaySingle(hitSound);
		m_agent.isStopped = true;
		m_stunCountdown = stunTime;
	}
}
