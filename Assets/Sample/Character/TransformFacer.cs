using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFacer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float ease;
    
    void Update()
    {
        transform.LookAt(target.position);
    }
}
