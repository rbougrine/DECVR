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
    public bool printingAllowed;
    public GameObject cantPrint;
    public bool emptyPrint;

    // Start is called before the first frame update
    void Start()
    {
        printingAllowed = true;
        emptyPrint = true;
    }

    // Update is called once per frame
    void Update()
    {
  
    } 

    public void chosenObject(string chosenItem)
    {
        wantedItem = chosenItem;

        if (emptyPrint)
        {
            StartCoroutine(Print(10, 2F));
        }
    }

    public IEnumerator Print(int n, float time)
    {
        if (printingAllowed)
        {
            printingAllowed = false;
            anim.Play("Printing", -1, 0F);
            printMagic.SetActive(true);
            yield return new WaitForSeconds(time);

        }
        animator.Play("Extracting");
        yield return new WaitForSeconds(1F);

        printMagic.SetActive(false);
       
        GameObject printResult = GameObject.Find("printResult");
        printObject printobject = printResult.GetComponent<printObject>();

        printobject.printedItem(wantedItem);

    }

}
