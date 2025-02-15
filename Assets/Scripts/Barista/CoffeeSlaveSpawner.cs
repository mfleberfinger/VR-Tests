﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSlaveSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject coffeeLine;
    [SerializeField]
    public GameObject endLine;
    [SerializeField]
    public GameObject prefab1;
    [SerializeField]
    public GameObject prefab2;
    [SerializeField]
    public GameObject prefab3;
    [SerializeField]
    public GameObject spawnLoc;
    [SerializeField]
    public float spawnTimer = 05.0f;

    private float m_coolDownTime = 05.0f;
    private List<GameObject> m_prefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_prefabs.Add(prefab1);
        m_prefabs.Add(prefab2);
        m_prefabs.Add(prefab3);
    }

    // Update is called once per frame
    void Update()
    {
        m_coolDownTime -= Time.deltaTime;
        if (m_coolDownTime < 0)
        {
            m_coolDownTime = spawnTimer;
            GameObject gobj = Instantiate(
                m_prefabs[Random.Range(0,m_prefabs.Count)],
                spawnLoc.transform.position,
                Quaternion.identity);
            CoffeeSlave cs = gobj.GetComponent<CoffeeSlave>();
            cs.coffeeLine = coffeeLine;
            cs.finishLine = endLine;
            cs.startTrek();
        }
    }
}
