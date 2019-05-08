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
    public SteamVR_Action_Boolean grabPickAction;
    public LayerMask teleportMask;
    public LayerMask interactionMask;
    public LayerMask grabMask;

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
    private Transform seenObject;

    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    private bool shouldTeleport;
    private int grabSensitivity;

    public GameObject paintBallGun;


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

      //  paintBallGun = GameObject.Find("paintBallGun");
      //  connectGun();
    }

    // Update is called once per frame
    void Update()
    {
        //walking code
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

        if (teleportAction.GetStateUp(handType) && shouldTeleport)
        {
            Teleport();
        }

        //interaction code
        if (grabAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, interactionMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit, InteractionLaser, InteractionLaserTransform);
                seenObject = hit.collider.gameObject.transform;
                grabSensitivity += 1;

           
                if (seenObject.parent.name == "doorPivot" && grabSensitivity > 20)
                {
                    GameObject door = GameObject.Find("doorPivot");
                    decDoor decdoor = door.GetComponent<decDoor>();

                    decdoor.DoorChoise();
                    grabSensitivity = 0;
                }
                
                if (seenObject.name == "3dprinter" && grabSensitivity > 20)
                {
                    GameObject cartridge = GameObject.Find("cartridge");
                    Printer Cartridge = cartridge.GetComponent<Printer>();

                    StartCoroutine(Cartridge.Print(5, 1F));
                    grabSensitivity = 0;           
                }
            }                      
        }
        else
        {
            InteractionLaser.SetActive(false);
        }

        //grab code
        if (grabPickAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, grabMask))
            {
                GameObject controllerRight = GameObject.Find("Controller (right)");
                ControllerGrabObject controllerCode = controllerRight.GetComponent<ControllerGrabObject>();

                paintBallGun = GameObject.Find("paintBallGun");

                if (controllerCode.objectInHand == paintBallGun)
                {
                    connectGun();
                }
            }
        }
    }

    private void connectGun()
    {
        paintBallGun.transform.position = new Vector3(controllerPose.transform.position.x, controllerPose.transform.position.y, controllerPose.transform.position.z);
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
