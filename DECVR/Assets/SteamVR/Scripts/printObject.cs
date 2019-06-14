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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void printOutcome(string wantedItem)
    {
        printMagic.SetActive(false);

        printedItem(wantedItem);
        
    }

    public void printedItem(string wantedItem)
    {
        switch (wantedItem)
        {
            case "paintballGun":
                Instantiate(paintballGun);
                break;
            case "kubus":
                Instantiate(kubus).GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                break;
            case "ball":
                Instantiate(ball).GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                break;
        } 
    }
}
