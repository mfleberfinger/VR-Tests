using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consumePen : MonoBehaviour
{
    private int pensCompleted = 0;
    private int fuckups = 0;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        Destroy(other.gameObject);
        /*
        if (other.gameObject.tag == "Cap")
        {
            ++fuckups;
            Destroy(other.gameObject);
        } else if (other.gameObject.tag == "Pen")
        {
            if (other.transform.childCount == 2)
                ++fuckups;
            else
                ++pensCompleted;
            Destroy(other.gameObject);
        }*/
    }
}
