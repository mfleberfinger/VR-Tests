using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContractSign : MonoBehaviour
{
    public int sceneToLoad = 1;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Debug.Log(other.tag);

        if (other.tag == "Pen") 
        {
            SceneManager.LoadScene(sceneToLoad);
            
        }
    }
}
