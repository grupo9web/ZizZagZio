﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPlatInicial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;

            Destroy(this.gameObject, 1);
        }
    }


    
}
