using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoffeeSlave : MonoBehaviour
{
    [SerializeField]
    private GameObject coffeeLine;
    [SerializeField]
    private GameObject finishLine;
    [SerializeField]
    private NavMeshAgent agent;

    private static Queue<int> m_List;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
