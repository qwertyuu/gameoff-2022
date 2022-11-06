using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Navigation
{
    public class SampleCheat : MonoBehaviour
    {
        [SerializeField] private RailNode switchNode;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switchNode.SwitchRoute();
                enabled = false;
            }
        }
    }
}
