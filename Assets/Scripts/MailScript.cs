using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailScript : MonoBehaviour
{
    public Material Red = null;
    public Material Blue = null;
    public Material Green = null;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(transform.GetInstanceID());
        GameObject kid = transform.GetChild(0).gameObject;

        switch (Random.Range(1,4))
        {
            case 1:
                kid.GetComponent<Renderer>().material = Blue;
                kid.tag = "Blue";
                break;
            case 2:
                kid.GetComponent<Renderer>().material = Green;
                kid.tag = "Green";
                break;
            case 3:
                kid.GetComponent<Renderer>().material = Red;
                kid.tag = "Red";
                break;
        }
        
    }
}
