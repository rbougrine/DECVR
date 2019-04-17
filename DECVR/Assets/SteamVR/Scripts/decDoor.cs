using System.Collections;
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

    /// <summary>
    /// Opens and closes the door automatically.
    /// </summary>
    /// <returns>An IEnumerator value</returns>

    public IEnumerator DoorMovement()
    {
            if (doorisClosed == true)
            {
                    animator.Play("OpenDoor");
                    doorisClosed = false;
                    yield return new WaitForSeconds(100);
                    animator.Play("closeDoor");
                    doorisClosed = true;

            }
    }
    
}
