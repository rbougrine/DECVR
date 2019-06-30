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
    public SteamVR_Action_Boolean squeezeAction;
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
    private bool gunConnected;
    private int grabSensitivity;
    public AudioSource audioData;
    public GameObject pcScreen;

    public GameObject paintBallGun;
    public GameObject reverseBack;
    public GameObject markers;
    public GameObject rangeBoards;
    public GameObject gameInfo;
    public GameObject hand;
    public GameObject usedWeapon;


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

                if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
                {
                    hitPoint = hit.point;

                    ShowLaser(hit, InteractionLaser, InteractionLaserTransform);
                    seenObject = hit.collider.gameObject.transform;

                    GameObject cartridge = GameObject.Find("printResult");
                    Printer printer = cartridge.GetComponent<Printer>();

                    grabSensitivity += 1;


                    if (gunConnected && grabSensitivity > 30)
                    {
                        paintBall paintBall = usedWeapon.GetComponent<paintBall>();
                        paintBall.Shoot(hit,controllerPose);
                        grabSensitivity = 0;
                    }

                    if (seenObject.parent.name == "doorPivot" && grabSensitivity > 20 && !gunConnected)
                    {
                        GameObject door = GameObject.Find("doorPivot");
                        decDoor decdoor = door.GetComponent<decDoor>();

                        decdoor.DoorChoose();
                        grabSensitivity = 0;
                    }

                    if (seenObject.parent.name == "doorPivotTwo" && grabSensitivity > 20 && !gunConnected)
                    {
                        GameObject doorTwo = GameObject.Find("doorPivotTwo");
                        decDoor decdoorTwo = doorTwo.GetComponent<decDoor>();

                        decdoorTwo.DoorChoose();
                        grabSensitivity = 0;
                    }

                if (seenObject.tag == "screen" && grabSensitivity > 20 && !gunConnected)
                    {
                        GameObject screen = GameObject.Find("diaScreen");
                        infoScreen info = screen.GetComponent<infoScreen>();

                        info.showScreen(hit.collider.gameObject);
                    }

                    if (seenObject.name == "3dprinter" && grabSensitivity > 20 && !gunConnected)
                    {
                       audioData = GetComponent<AudioSource>();
                       audioData.Play(0);

                        if (printer.printingAllowed)
                        {
                          pcScreen.SetActive(true);
                        }
                          grabSensitivity = 0;
                    }

                    if (seenObject.parent.name == "printButtons" && grabSensitivity > 20 && !gunConnected)
                    {
                        if (printer.printingAllowed)
                        {
                            printer.chosenObject(seenObject.name);
                        }
                        else
                        {
                            pcScreen.SetActive(false);
                            printer.cantPrint.SetActive(true);
                        }
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

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                GameObject cartridge = GameObject.Find("printResult");
                Printer printer = cartridge.GetComponent<Printer>();

                ControllerGrabObject UsedController = controllerPose.GetComponent<ControllerGrabObject>();

                if (UsedController.objectInHand.name == "paintBallGun(Clone)")
                {
                    usedWeapon = UsedController.objectInHand;
                    GameObject paint = GameObject.Find("paintBallGun(Clone)");
                    collisionCheck check = paint.GetComponent<collisionCheck>();
                    gunConnected = true;

                    UsedController.objectInHand.SetActive(false);
                    check.retract();
                    handWeapon(controllerPose);
                }

                if (UsedController.objectInHand.name == "reverseBack")
                {
                    reverseBack.SetActive(false);
                    markers.SetActive(false);
                    rangeBoards.SetActive(false);
                    gameInfo.SetActive(false);
                    paintBallGun.SetActive(false);
                    hand.SetActive(true);

                    gunConnected = false;
                }

            }
        }
    }

        private void handWeapon(SteamVR_Behaviour_Pose controllerPose)
        {
            int children = controllerPose.transform.childCount;

            for (int i = 0; i < children; ++i)
            {
                switch (controllerPose.transform.GetChild(i).name)
                {
                    case "rightHand":
                        GameObject rightHand = GameObject.Find(controllerPose.transform.GetChild(i).name);
                        rightHand.SetActive(false);
                        paintBallGun.SetActive(true);
                        reverseBack.SetActive(true);
                        markers.SetActive(true);
                        rangeBoards.SetActive(true);
                        gameInfo.SetActive(true);
                        break;
                    case "leftHand":
                        GameObject leftHand = GameObject.Find(controllerPose.transform.GetChild(i).name);
                        leftHand.SetActive(false);
                        paintBallGun.SetActive(true);
                        reverseBack.SetActive(true);
                        markers.SetActive(true);
                        rangeBoards.SetActive(true);
                        gameInfo.SetActive(true);
                        break;

                }
            }
        }
 
        public void ShowLaser(RaycastHit hit, GameObject laser, Transform laserTransform)
        {
            if (!gunConnected)
            {
                laser.SetActive(true);
                laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
                laserTransform.LookAt(hitPoint);
                laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                                    laserTransform.localScale.y,
                                                                    hit.distance);
            }
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

