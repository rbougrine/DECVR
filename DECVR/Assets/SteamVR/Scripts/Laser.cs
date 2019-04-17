//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Valve.VR;

//public class Laser : MonoBehaviour
//{
//    public SteamVR_Input_Sources handType;
//    public SteamVR_Behaviour_Pose controllerPose;

//    public GameObject laserPrefab;
//    private GameObject laser;
//    private Vector3 hitPoint;
//    private Transform laserTransform;
    

//    private SteamVR_Action_Boolean teleportAction;
//    private LayerMask teleportMask;
//    private bool shouldTeleport;


//    public GameObject reticleGreen;
//    public GameObject reticleRed;
//    public GameObject teleportReticleGreenPrefab;
//    public GameObject teleportReticleRedPrefab;
//    public Transform teleportReticleGreenTransform;
//    public Transform teleportReticleRedTransform;

//   LaserPointer laserPointer = new LaserPointer();

//    // Start is called before the first frame update
//    void Start()
//    {
//        GameObject theController = GameObject.Find("Controller (left)");
//        LaserPointer laserPointer = theController.GetComponent<LaserPointer>();

//        laser = Instantiate(laserPrefab);
//        laserTransform = laser.transform;

//        reticleGreen = Instantiate(teleportReticleGreenPrefab);
//        teleportReticleGreenTransform = reticleGreen.transform;

//        reticleRed = Instantiate(teleportReticleRedPrefab);
//        teleportReticleRedTransform = reticleRed.transform;

//        teleportAction = laserPointer.teleportAction;
//        shouldTeleport = laserPointer.shouldTeleport;
//        teleportMask = laserPointer.teleportMask;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (teleportAction.GetState(handType))
//        {
//            RaycastHit hit;

//            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask))
//            {
//                hitPoint = hit.point;
//                ShowLaser(hit);
//                laserPointer.checkTeleport(hit, hitPoint);
//            }
//        }
//        else
//        {
//            laser.SetActive(false);
//            reticleGreen.SetActive(false);
//            reticleRed.SetActive(false);
//        }

//        if (teleportAction.GetStateUp(handType) && shouldTeleport)
//        {
//            laserPointer.Teleport(hitPoint);
//        }
//    }

//    public void ShowLaser(RaycastHit hit)
//    {
   
//        laser.SetActive(true);
        
//        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
//        laserTransform.LookAt(hitPoint);
//        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
//                                                laserTransform.localScale.y,
//                                                hit.distance);
       
//    }
//}
