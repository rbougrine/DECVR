﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class decDoor : MonoBehaviour
{
    
    public Animator animator;
    private bool doorisClosed = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoorChoose()
    {
        
        if (doorisClosed)
        {
            StartCoroutine(DoorMovement("OpenDoor"));
            doorisClosed = false;
        }
        else
        {
            StartCoroutine(DoorMovement("CloseDoor"));
            doorisClosed = true;
        }


    }

    /// <summary>
    /// Opens the door.
    /// </summary>
    /// <returns>An IEnumerator value</returns>

    private IEnumerator DoorMovement(string DoorAction)
    {
            animator.Play(DoorAction);
            yield return new WaitForSeconds(5);
    }
    
}
