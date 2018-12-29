using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public GameObject particleSpawn = null;

    private ParticleSystem m_particleSystem;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pen")
            m_particleSystem.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_particleSystem = particleSpawn.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
