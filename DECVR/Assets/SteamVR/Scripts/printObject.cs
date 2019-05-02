using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printObject : MonoBehaviour
{
    public Animator animator;
    private GameObject printMagic;

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


    public void printOutcome()
    {
        printMagic.SetActive(false);
        animator.Play("Extracting");

    }
}
