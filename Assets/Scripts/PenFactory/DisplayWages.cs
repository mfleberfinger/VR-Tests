using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWages : MonoBehaviour
{
    [Tooltip("The Text object to use for debug messages.")]
    [SerializeField]
    private Text debugText = null;
    [Tooltip("The thing that has the score")]
    [SerializeField]
    private GameObject scoreObject = null;

    // Update is called once per frame
    void Update()
    {
        consumePen cp = scoreObject.GetComponent<consumePen>();
        debugText.text =
            "Pens Assembled " +  cp.pensCompleted +
            "\n           Wages " + (cp.pensCompleted - cp.fuckups/3.0)/20.0 +
            "\n         Fuckups " + cp.fuckups; 
    }
}
