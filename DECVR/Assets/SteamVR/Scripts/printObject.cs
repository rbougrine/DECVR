using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printObject : MonoBehaviour
{
    public Animator animator;
    private GameObject printMagic;
    public GameObject paintballGun;
    public GameObject kubus;
    public GameObject ball;
    public string wantedItem;
    private bool printingFinished;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        GameObject cartridge = GameObject.Find("cartridge");
        Printer printer = cartridge.GetComponent<Printer>();
        printMagic = printer.printMagic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AlertObservers(string message)
    {
        if (message.Equals("AnimationEnded"))
        {
            printingFinished = true;
        }
    }

    public void printOutcome(string wantedItem)
    {
        printMagic.SetActive(false);

       
            printedItem(wantedItem);
            printingFinished = false;
        
    }

    public void printedItem(string wantedItem)
    {
        Debug.Log(1);
        switch (wantedItem)
        {
            case "paintballGun":
                Instantiate(paintballGun);
                break;
            case "kubus":
                Instantiate(kubus);
                break;
            case "ball":
                Instantiate(ball);
                break;
        } 
    }
}
