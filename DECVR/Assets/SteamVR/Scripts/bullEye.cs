using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullEye : MonoBehaviour
{
    public Animator animator;
    public GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        animator = robot.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "projectile(Clone)")
        {
            StartCoroutine(RobotMovement("dancing"));
        }
    }

    private IEnumerator RobotMovement(string robotAction)
    {
        animator.Play(robotAction);
        yield return new WaitForSeconds(5);
    }


}
