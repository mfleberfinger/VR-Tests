using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoffeeSlave : MonoBehaviour
{
    [SerializeField]
    public GameObject coffeeLine;
    [SerializeField]
    public GameObject finishLine;
    [SerializeField]
    private NavMeshAgent agent;

    public enum CoffeeState : byte {navigating, waiting};

    public int linePosition = 0;
    private static Queue<CoffeeSlave> m_List = new Queue<CoffeeSlave>();
    public CoffeeState m_State = CoffeeState.waiting;
    Animator m_Animator;

    public void setDest(Vector3 dest)
    {
        agent.SetDestination(dest);
    }

    //part of spawn init
    public void startTrek()
    {
        setDest(nextLinePos());
        m_State = CoffeeState.navigating;
    }


    void animateToNode()
    {
        Vector3 dir = (agent.nextPosition - transform.position).normalized;
        float fwd = Vector3.Dot(dir, transform.forward)*
            (agent.destination != finishLine.transform.position ? .7f : 1.0f);
        fwd = fwd < 0 ? 0 : fwd;
        float turn = Vector3.Dot(dir, transform.right);
        m_Animator.SetFloat("Speed", fwd);
        m_Animator.SetFloat("Turn", turn);
    }

    public Vector3 nextLinePos()
    {
        return coffeeLine.transform.position +
            coffeeLine.transform.forward * linePosition * 1.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        linePosition = m_List.Count;
        m_List.Enqueue(this);
        m_Animator = GetComponent<Animator>();
        if (m_Animator == null)
            throw new System.Exception();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (linePosition == 0 && other.tag == "coffee")
        {
            Destroy(other.gameObject);
            setDest(finishLine.transform.position);
            m_State = CoffeeState.navigating;
            m_List.Dequeue();
            foreach (CoffeeSlave cs in m_List)
            {
                --cs.linePosition;
                cs.setDest(cs.nextLinePos());
                cs.m_State = CoffeeState.navigating;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case CoffeeState.waiting:
                m_Animator.SetFloat("Speed", 0f);
                m_Animator.SetFloat("Turn", 0f);
                break;
            case CoffeeState.navigating:
                animateToNode();
                if (Vector3.Magnitude(agent.destination - transform.position) < 0.8)
                    m_State = CoffeeState.waiting;
                break;
        }
    }
}
