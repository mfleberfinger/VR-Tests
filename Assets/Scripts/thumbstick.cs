using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thumbstick : MonoBehaviour
{
    [Tooltip("Thumbstick to use for X movement")]
    [SerializeField]
    private string thumbStickIDX=null;
    [Tooltip("Thumbstick to use for Z movement")]
    [SerializeField]
    private string thumbStickIDY=null;
    [Tooltip("Left Hand to Normalize Movement Against")]
    [SerializeField]
    GameObject leftHand=null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float fwdInput = -Input.GetAxis(thumbStickIDY)/25f;
        float sideInput = -Input.GetAxis(thumbStickIDX)/25f;
        Vector3 thisMove = new Vector3(0, 0, 0);
        Vector3 fwd = leftHand.transform.forward;
        fwd.y = 0;
        fwd.Normalize();
        Vector3 right = Vector3.Cross(fwd, new Vector3(0, 1, 0));
        thisMove += fwdInput * fwd;
        thisMove += sideInput * right;
        transform.position += thisMove;
    }
}
