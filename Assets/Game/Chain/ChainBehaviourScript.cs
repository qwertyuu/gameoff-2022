using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChainBehaviourScript : MonoBehaviour
{
    public Transform player; //mon point de depart = joueur ? camera ? vue premiere personne ?
    public float force;
    public int numberOfLinks; //le nombre de maillons - pour plus tard
    
    bool readyToThrow;
    //mettre un cooldown ?

    private Rigidbody rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        readyToThrow = true;
        rbody.isKinematic = true;        
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.isKinematic) {
            transform.position = player.position;
        }

        if (Input.GetMouseButtonDown(0) && readyToThrow) //clique gauche
        {
            Throw();
        }
    }

    private void Throw()
    {
        Debug.Log("throw");

        readyToThrow = false;
        rbody.isKinematic = false;
        rbody.AddForce(player.forward * force + transform.up * force/2, ForceMode.Impulse);


        readyToThrow = true;
    }


}
