using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMail : MonoBehaviour
{
    public GameObject m_Mail = null;
    private bool m_empty = false;

    public void OnTriggerExit(Collider other)
    {
        m_empty = true;
        StartCoroutine(SpawnCheckSpawn());
    }

    public void OnTriggerEnter(Collider other)
    {
        m_empty = false;
    }

    IEnumerator SpawnCheckSpawn()
    {
        yield return new WaitForSeconds(.5f);
        if (m_empty)
        {
            Instantiate(m_Mail, transform.position, transform.rotation);
        }
    }

}
