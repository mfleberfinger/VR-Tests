using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyPipe : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        Debug.Log(other.gameObject.tag);
        
        if (other.gameObject.tag == transform.tag) 
        {
            Transform mail = other.gameObject.transform.parent;
            
            mail.parent = null;
            mail.GetComponent<Rigidbody>().isKinematic = false;
            other.enabled = false;
            mail.GetComponent<Collider>().enabled = false;
            mail.GetComponent<Rigidbody>().useGravity = false;
            mail.GetComponent<Rigidbody>().freezeRotation = true;
            mail.rotation = transform.rotation;
            mail.GetComponent<Rigidbody>().velocity = transform.up * 8f;
            StartCoroutine(DieAlone(mail));
        }
    }

    IEnumerator DieAlone(Transform mail)
    {
        yield return new WaitForSeconds(1f);
        Destroy(mail.gameObject);
    }

}
