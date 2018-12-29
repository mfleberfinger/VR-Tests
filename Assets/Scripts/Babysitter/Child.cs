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

	[Tooltip("NavMesh Agent for this character.")]
	[SerializeField]
	private NavMeshAgent agent = null;

	[Tooltip("Animator for this character.")]
	[SerializeField]
	private Animator anim = null;

	[Tooltip("Clips to choose from to play when something breaks.")]
	public AudioClip[] breakSounds;

	private static Queue<TiltSensor> m_tiltSensors;
	private float m_stunCountdown;
	//Transform values from the last frame. Used for shoddy animation.
	private Vector3 m_LastPosition;
	private Vector3 m_LastRotation;

	private void Start()
	{
		if (m_tiltSensors == null)
			m_tiltSensors = new Queue<TiltSensor>();

		// Initialize the destination value of NavMesh to allow us to check it
		// properly in the if statement in Update().
		agent.destination = transform.position;
		m_stunCountdown = 0f;
		m_LastPosition = transform.position;
		m_LastRotation = transform.rotation.eulerAngles;
	}

	private void Update()
	{
		if (Vector3.Magnitude(m_LastPosition - transform.position) > float.Epsilon)
			anim.SetFloat("Speed", 1f);
		else
			anim.SetFloat("Speed", 0f);

		if (Vector3.Magnitude(m_LastRotation - transform.rotation.eulerAngles) > 0.1f)
		{
			if (Mathf.Sign(Vector3.SignedAngle(transform.rotation.eulerAngles, m_LastRotation, Vector3.up)) < 0)
				anim.SetFloat("Turn", -1f);
			else
				anim.SetFloat("Turn", 1f);
		}
		else
			anim.SetFloat("Turn", 0f);

		// TiltSensors will identify themselves to Child. Choose the next
		// one and pathfind to it. Once it is broken, choose the next one
		// available. Repeat until out of things to break.
		if (Mathf.Approximately(transform.position.x, agent.destination.x) &&
			Mathf.Approximately(transform.position.z, agent.destination.z) &&
			m_tiltSensors.Count > 0 && m_stunCountdown <= 0)
		{
			agent.isStopped = false;
			anim.SetBool("Crouch", false);
			agent.destination = m_tiltSensors.Dequeue().transform.position;
		}
		else if (m_tiltSensors.Count == 0)
		{
			agent.isStopped = false;
			anim.SetBool("Crouch", false);
		}
		else if (m_stunCountdown > 0)
			m_stunCountdown -= Time.deltaTime;
		else if (m_stunCountdown <= 0)
		{
			agent.isStopped = false;
			anim.SetBool("Crouch", false);
		}
		
		m_LastPosition = transform.position;
		m_LastRotation = transform.rotation.eulerAngles;
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
		agent.isStopped = true;
		m_stunCountdown = stunTime;
		anim.SetBool("Crouch", true);
	}
}
