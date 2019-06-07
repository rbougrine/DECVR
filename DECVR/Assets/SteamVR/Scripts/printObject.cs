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
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        GameObject cartridge = GameObject.Find("cartridge");
        Printer printer = cartridge.GetComponent<Printer>();

        printMagic = printer.printMagic;
        //paintballGun.SetActive(false);
        ball.SetActive(false);
        kubus.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void printOutcome(string wantedItem)
    {
        printMagic.SetActive(false);
        animator.Play("Extracting");
        printedItem(wantedItem);
    }

    public void printedItem(string wantedItem)
    {
        switch (wantedItem)
        {
            case "paintballGun":
                paintballGun.SetActive(true);
                break;
            case "kubus":
                kubus.SetActive(true);
                break;
            case "ball":
                ball.SetActive(true);
                break;
        } 
    }
}
