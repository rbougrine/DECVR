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
    private bool printed;

    // Start is called before the first frame update
    void Start()
    {
        printMagic.SetActive(false);
        paintBallGun.SetActive(false);
        printed = true;

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
        while (n > 0 && printed)
        {
            anim.Play("Printing", -1, 0F);
            --n;
            printMagic.SetActive(true);
            printed = false;
            yield return new WaitForSeconds(time);
        }

        GameObject printResult = GameObject.Find("printResult");
        printObject printobject = printResult.GetComponent<printObject>();

        printobject.printOutcome();

    }
}
