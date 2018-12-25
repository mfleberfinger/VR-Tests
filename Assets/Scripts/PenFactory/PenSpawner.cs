using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PenSpawner : MonoBehaviour
{
    [Tooltip("Time between spawns of the item, in float seconds")]
    [SerializeField]
    private float spawnRate = 0;
    [Tooltip("The object to spawn at the spawnpoint")]
    [SerializeField]
    private GameObject spawnObject = null;

    private float spawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            GameObject.Instantiate(spawnObject, transform.position, transform.rotation);
            spawnTimer = 0;
        }
    }
}
