using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class paintBall : MonoBehaviour
{
    private GameObject bullet;
    public GameObject barrel;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(RaycastHit hit, SteamVR_Behaviour_Pose controllerPose)
    {
        GameObject bullet = Instantiate(projectilePrefab) as GameObject;
        bullet.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        bullet.transform.position = Vector3.Lerp(controllerPose.transform.position, hit.point, .5f);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = controllerPose.transform.forward * 10;
    } 
}
