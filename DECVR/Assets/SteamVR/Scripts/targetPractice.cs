using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPractice : MonoBehaviour
{
    public GameObject splashPrefab;


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
        if (col.gameObject.name == "projectile(Clone)")
        {
            Vector3 shotFired = col.transform.position;
            Color shotColor = col.gameObject.GetComponent<Renderer>().material.color;
            Destroy(col.gameObject);
            Debug.Log(shotFired);
            Debug.Log(shotColor);
            GameObject splash = Instantiate(splashPrefab) as GameObject;
            splash.transform.position = shotFired;
            splash.GetComponent<Renderer>().material.color = shotColor;

        }
    }
}
