using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenCap : MonoBehaviour
{
    private enum PenStates { Bald, Capped };
    PenStates State = PenStates.Bald;
    private bool held = false;
    // Start is called before the first frame update
    void Start() { }

    private void OnTriggerEnter(Collider other)
    {
        if (State == PenStates.Bald
            && other.tag == "Cap")
        {
            Rigidbody orb = other.GetComponent<Rigidbody>();
            Transform otf = other.GetComponent<Transform>();
            other.enabled = false;
            orb.isKinematic = true;
            otf.position = transform.position;
            otf.rotation = transform.rotation;
            otf.parent = transform.parent;
            State = PenStates.Capped;
        }
    }
}
