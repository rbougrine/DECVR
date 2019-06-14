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
    private bool printingAllowed;

    // Start is called before the first frame update
    void Start()
    {
        printingAllowed = true;

        anim = GetComponent<Animator>();
        GameObject printResult = GameObject.Find("printResult");
        printObject printobject = printResult.GetComponent<printObject>();

        animator = printobject.animator;
    }

    // Update is called once per frame
    void Update()
    {
     
        

    }

    public void chosenObject(string chosenItem)
    {
         wantedItem = chosenItem;
         StartCoroutine(Print(10, 4F));
    }

    public IEnumerator Print(int n, float time)
    {
        while (n > 0 && printingAllowed)
        {
            anim.Play("Printing", -1, 0F);
            --n;
            printMagic.SetActive(true);
            printingAllowed = false;
            yield return new WaitForSeconds(time);
           
        }
            animator.Play("Extracting");
            yield return new WaitForSeconds(1F);
            printMagic.SetActive(false);

            GameObject printResult = GameObject.Find("printResult");
            printObject printobject = printResult.GetComponent<printObject>();
        
            printobject.printedItem(wantedItem);
            printingAllowed = true;

    }
}
