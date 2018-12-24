using System;
using UnityEngine;

public class ID : MonoBehaviour
{
    public Guid guid;

    void Awake()
    {
        guid = Guid.NewGuid();
    }
}
