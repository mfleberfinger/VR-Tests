using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	[Tooltip("The axis ID of this hand's grip button.")]
	[SerializeField]
	private string gripID;
	
	[Tooltip("The object to spawn at this hand, if any.")]
	[SerializeField]
	private GameObject block;

	private Dictionary<int, Rigidbody> m_heldObjects;

    // keep track of velocity in last 32 frames and use weighted average in release velocity
    int m_arrInd = 0;
    private Vector3[] m_velocityHistory = new Vector3[32];
    Vector3 m_lastPos;
    //the above should possibly be in its own class or something whatevewr

	private void Start()
	{
		m_heldObjects = new Dictionary<int, Rigidbody>();
	}

	private void OnTriggerStay(Collider other)
	{
		int id = other.GetInstanceID();

		//Grab any objects touching the hand if the grip button is pressed and 
		// the objects have rigidbody components.
		if(Input.GetAxis(gripID) > 0.99f
			&& other.gameObject.GetComponent<Rigidbody>() != null
			&& !m_heldObjects.ContainsKey(id))
		{
			m_heldObjects.Add(other.GetInstanceID(), other.attachedRigidbody);
            other.attachedRigidbody.drag = 15;
		}
	}

	private void Update()
	{
        Vector3 cur = transform.position;
        ++m_arrInd;
        if (m_arrInd >= m_velocityHistory.Length)
            m_arrInd = 0;
        m_velocityHistory[m_arrInd] = cur - m_lastPos;
        m_lastPos = cur;

		//Spawn blocks.
		if(block != null && Input.GetButtonDown("Fire1"))
		{
			GameObject.Instantiate(block, transform.position, transform.rotation);
		}
	}
    private void FixedUpdate()
    {
        Vector3 cur = transform.position;
        if (m_heldObjects.Count != 0 && Input.GetAxis(gripID) > 0.99f)
        {
            foreach (KeyValuePair<int, Rigidbody> pair in m_heldObjects)
            {
                Vector3 Force = (cur - pair.Value.position) / Time.deltaTime;
                Force *= Force.magnitude;
                pair.Value.AddForce(Force);
            }
        }
        //Release held objects when the grip button is released.
        else if (m_heldObjects.Count != 0 && Input.GetAxis(gripID) <= 0.99f)
        {
            //get average velocity
            Vector3 vel = new Vector3(0, 0, 0);
            for (int i = 0; i < m_velocityHistory.Length; ++i)
                vel += m_velocityHistory[i];

            foreach (KeyValuePair<int, Rigidbody> pair in m_heldObjects)
            {

                pair.Value.velocity = Vector3.zero;
                pair.Value.AddForce(10.0f * vel / Time.deltaTime );
                pair.Value.drag = 1;
            }
            //Clear the list of held objects since we've dropped them all.
            m_heldObjects.Clear();
        }

    }
}
