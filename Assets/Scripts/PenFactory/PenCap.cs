using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenCap : MonoBehaviour
{
    public enum PenStates { Bald, Capped };
    public PenStates State = PenStates.Bald;
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
            otf.rotation = transform.rotation;
            otf.Rotate(Vector3.left, 180);
            otf.position = transform.position;
            //otf.localPosition = new Vector3(0,0,-0.1335f);
            otf.parent = transform;
            State = PenStates.Capped;
        }
    }
}
