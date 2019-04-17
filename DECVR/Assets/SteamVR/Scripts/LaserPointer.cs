using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean grabAction;

    public GameObject WalkingLaserPrefab;
    public GameObject InteractionLaserPrefab;
    private GameObject WalkingLaser;
    private GameObject InteractionLaser;
    private Transform WalkingLaserTransform;
    private Transform InteractionLaserTransform;
    private Vector3 hitPoint;


    public Transform cameraRigTransform;
    public GameObject teleportReticleGreenPrefab;
    public GameObject teleportReticleRedPrefab;
    private GameObject reticleGreen;
    private GameObject reticleRed;
    private Transform teleportReticleGreenTransform;
    private Transform teleportReticleRedTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    private bool shouldTeleport;


    private Transform seenObject;

    // Start is called before the first frame update
    void Start()
    {
        WalkingLaser = Instantiate(WalkingLaserPrefab);
        WalkingLaserTransform = WalkingLaser.transform;

        InteractionLaser = Instantiate(InteractionLaserPrefab);
        InteractionLaserTransform = InteractionLaser.transform;

        reticleGreen = Instantiate(teleportReticleGreenPrefab);
        teleportReticleGreenTransform = reticleGreen.transform;

        reticleRed = Instantiate(teleportReticleRedPrefab);
        teleportReticleRedTransform = reticleRed.transform;

    }

    // Update is called once per frame
    void Update()
    {    
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit, WalkingLaser, WalkingLaserTransform);
                checkTeleport(hit);
            }
        }
        else 
        {
            WalkingLaser.SetActive(false);
            reticleGreen.SetActive(false);
            reticleRed.SetActive(false);
        }

        if (grabAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit, InteractionLaser, InteractionLaserTransform);
                seenObject = hit.collider.gameObject.transform;

                if (seenObject.parent.name == "doorPivot")
                {
                    GameObject door = GameObject.Find("doorPivot");
                    decDoor decdoor = door.GetComponent<decDoor>();
                    
                     StartCoroutine(decdoor.DoorMovement());
                }

            }
           
        }
        else
        {
            InteractionLaser.SetActive(false);
        }

        if (teleportAction.GetStateUp(handType) && shouldTeleport )
        {
           Teleport();
        }

    }

    private void ShowLaser(RaycastHit hit, GameObject laser, Transform laserTransform)
    {
        
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);

    }

    private void checkTeleport(RaycastHit hit)
    {
        if (hit.collider.gameObject.tag == "cantTeleport")
        {
            reticleGreen.SetActive(false);
            reticleRed.SetActive(true);
            shouldTeleport = false;
            teleportReticleRedTransform.position = hitPoint + teleportReticleOffset;
        }
        else
        {
            reticleRed.SetActive(false);
            reticleGreen.SetActive(true);
            teleportReticleGreenTransform.position = hitPoint + teleportReticleOffset;
            shouldTeleport = true;
        }



    }

    private void Teleport()
    {        
        shouldTeleport = false;
        reticleGreen.SetActive(false);

        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        hitPoint.y = cameraRigTransform.position.y;
        cameraRigTransform.position = hitPoint + difference;
    }


}
