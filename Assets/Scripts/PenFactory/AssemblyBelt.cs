using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyBelt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay(Collision c)
    {
        Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
        rb.drag = 5;
        rb.velocity = new Vector3(.4f,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
