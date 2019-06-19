using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision col)
    {
        GameObject cartridge = GameObject.Find("printResult");
        Printer printer = cartridge.GetComponent<Printer>();

        if (col.gameObject.tag == "printed")
        {
            printer.emptyPrint = false;
        }
    }

    public void OnCollisionExit(Collision col)
    {
        GameObject controller = GameObject.Find("Controller (left)");
        LaserPointer laserPointer = controller.GetComponent<LaserPointer>();

        GameObject cartridge = GameObject.Find("printResult");
        Printer printer = cartridge.GetComponent<Printer>();

        if (col.gameObject.tag == "printed")
        {
            printer.printingAllowed = true;
            printer.animator.Play("Reverse");
            printer.emptyPrint = true;

            printer.cantPrint.SetActive(false);
            laserPointer.pcScreen.SetActive(true);
        }
    }
}
