﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullEye : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
          //  StartCoroutine(RobotMovement(""));
        }
        else if(col.gameObject.name == "Robot")
        {
            Debug.Log("Robot YASSSSS");
            //  StartCoroutine(RobotMovement(""));
        }
    }

    private IEnumerator RobotMovement(string robotAction)
    {
        animator.Play(robotAction);
        yield return new WaitForSeconds(5);
    }


}
