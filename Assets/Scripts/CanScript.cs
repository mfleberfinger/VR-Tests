using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanScript : MonoBehaviour
{
    public GameObject Message = null;
    private bool m_GameOver = false;
    private int m_canStack = 0;

    // Start is called before the first frame update
    void Start()
    {
        Message.GetComponent<Text>().text = "Stack the shelf!!!!";

    }

    // Update is called once per frame
    void Update()
    {
        int countDown = 60 - (int)Time.time;
        if (countDown >= 0)
        {
            Message.GetComponent<Text>().text = "Stack the shelf!!!!\n" + countDown.ToString();
        }
        if (countDown < 0 && m_GameOver != true)
        {
            Message.GetComponent<Text>().text = "You stacked " + m_canStack.ToString() + " cans";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Can")
        {
            m_canStack++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Can")
        {
            m_canStack--;
        }

    }
}
