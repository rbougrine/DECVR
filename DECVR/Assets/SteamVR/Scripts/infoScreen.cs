using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoScreen : MonoBehaviour
{
    public GameObject pepperScreen;
    public GameObject tvScreen;
    public GameObject diaScreen;
    public GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showScreen(GameObject screen)
    {
        var objectName = screen.transform.name;

        switch (objectName)
        {
            case "RobotOne":
                pepperScreen.SetActive(true);
                robotShot robotScript = robot.GetComponent<robotShot>();
                StartCoroutine(robotScript.RobotMovement("waving"));
                break;
            case "smartTV":
                tvScreen.SetActive(true);
                break;
            case "diaScreen":
                diaScreen.SetActive(true);
                break;
        }
    }
         
    public void removeScreen(GameObject screen)
    {
        screen.SetActive(false);
    }
}
