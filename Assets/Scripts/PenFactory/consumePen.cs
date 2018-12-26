using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumePen : MonoBehaviour
{
    public int pensCompleted = 0;
    public int fuckups = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cap")
        {
            ++fuckups;
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Pen")
        {
            Transform o = other.GetComponent<Transform>();
            if (other.transform.parent != null)
                ++fuckups;
            else
                ++pensCompleted;
            Destroy(other.gameObject);
        }
    }
}
