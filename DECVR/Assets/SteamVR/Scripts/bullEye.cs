﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullEye : MonoBehaviour
{





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "bullEye")
        {
            Debug.Log("YASSSSS");
        }
    }




}
