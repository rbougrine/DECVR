using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Printer : MonoBehaviour
{

    public Animator anim;
    public Animator animator;
    public GameObject printMagic;
    public string wantedItem;
    private bool print;

    // Start is called before the first frame update
    void Start()
    {
        printMagic.SetActive(false);
        print = true;

        anim = GetComponent<Animator>();
       
       
    }

    // Update is called once per frame
    void Update()
    {
     
        

    }

    public void chosenObject()
    {
        //get info from UI screen and set var 


        //wantedItem = ;
    }

    public IEnumerator Print(int n, float time)
    {
        while (n > 0 && print)
        {
            anim.Play("Printing", -1, 0F);
            --n;
            printMagic.SetActive(true);
            print = false;
            yield return new WaitForSeconds(time);
        }

        GameObject printResult = GameObject.Find("printResult");
        printObject printobject = printResult.GetComponent<printObject>();

        printobject.printOutcome(wantedItem);

    }
}
