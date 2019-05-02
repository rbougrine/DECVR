using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Printer : MonoBehaviour
{

    public Animator anim;
    public Animator animator;
    public GameObject printMagic;
    public GameObject paintBallGun;

    // Start is called before the first frame update
    void Start()
    {
        printMagic.SetActive(false);
        paintBallGun.SetActive(false);

        anim = GetComponent<Animator>();
       
       
    }

    // Update is called once per frame
    void Update()
    {
     
        

    }

    public void AlertObservers(string message)
    {
        if (message.Equals("AnimationEnded"))
        {
            paintBallGun.SetActive(true);
        }
    }


    public IEnumerator Print(int n, float time)
    {
        while (n > 0)
        {
            anim.Play("Printing", -1, 0F);
            --n;
            printMagic.SetActive(true);
            yield return new WaitForSeconds(time);
        }

        GameObject printResult = GameObject.Find("printResult");
        printObject printobject = printResult.GetComponent<printObject>();

        printobject.printOutcome();

    }


    /*
    private IEnumerable printCube()
    {
        yield return new WaitForSeconds(5);
    }

    private IEnumerable printBall()
    {
        yield return new WaitForSeconds(5);
    }
    */

}
