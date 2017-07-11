using UnityEngine;
using System.Collections;
using System;

public class DroneController : MonoBehaviour
{
    [Header("drone settings")]
    public GameObject _prefab;
    public int maxHeight = 50;


    [Header("drone group settings")]
	public int dronesPerGroup = 3;
	int DronesPerHand;

    public int displacementX = 5;
    public int displacementZ = 5;

    [Header("drone group targets")]
    public GameObject PlayerTarget;
    public GameObject LeftDronesHome;
    public GameObject RightDronesHome;

    public float NoteHitForce = 10f;

    public int rightGloveMidiStart = 55;
    public int leftGloveMidiStart = 24;

    private GameObject[] LeftDrones;
    private GameObject[] RightDrones;

    private ZoneControllerSingleCamera ZoneController;

    // Use this for initialization
    void Start()
    {

        if (null == ZoneController)
        {
            ZoneController = GameObject.Find("ZONES").GetComponent<ZoneControllerSingleCamera>();
            if (null == ZoneController)
            {
                Debug.Log("ERROR: couldn't find ZoneController");
            }

        }

        DronesPerHand = dronesPerGroup * 5;
		LeftDrones = new GameObject[DronesPerHand];
		RightDrones = new GameObject[DronesPerHand];

        Vector3 target = PlayerTarget.GetComponent<Transform>().position;

		for (int i = 0; i < DronesPerHand; i++)
        {
            GameObject drone = Instantiate(_prefab, leftStart(i), Quaternion.identity) as GameObject;
            drone.name = "drone-left-" + i;
			drone.GetComponentInChildren<MeshRenderer>().material.color = Color.HSVToRGB(i / (float)DronesPerHand, .5f, .5f);
            //drone.GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target;
            LeftDrones[i] = drone;
        }

		for (int i = 0; i < DronesPerHand; i++)
        {
            GameObject drone = Instantiate(_prefab, rightStart(i), Quaternion.identity) as GameObject;
            drone.name = "drone-right-" + i;
			drone.GetComponentInChildren<MeshRenderer>().material.color = Color.HSVToRGB(i / (float)DronesPerHand, .5f, .5f);
            //drone.GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target;
            RightDrones[i] = drone;
        }


    }


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(GameControllerVR.HOTKEY_DRONE_TO_TARGET))
        {
            Debug.Log("targeting player");
            TargetPlayer();
        }

        if (Input.GetKeyDown(GameControllerVR.HOTKEY_DRONE_TO_HOME))
        {
            Debug.Log("targeting home");
            TargetHome();
        }


        if (Input.GetKeyDown(GameControllerVR.HOTKEY_DRONE_HIT))
        {
			for (int i = 0; i < DronesPerHand; i++)
            {
				HitDrone (LeftDrones [i]);
				HitDrone (RightDrones [i]);

                //LeftDrones[i].GetComponentInChildren<Animation>().Play();
                //RightDrones[i].GetComponentInChildren<Animation>().Play();
               
            }
        }
   
    }

	private Vector3 leftStart(int droneNumber)
	{
		Vector3 leftGroupStart = LeftDronesHome.GetComponent<Transform>().position;
		return leftGroupStart + leftOffset(droneNumber);
	}
	private Vector3 leftOffset(int droneNumber)
	{
		return new Vector3(-droneNumber/dronesPerGroup * displacementX, 0, droneNumber%dronesPerGroup * displacementZ);
	}


	private Vector3 rightStart(int droneNumber)
	{
		Vector3 rightGroupStart = RightDronesHome.GetComponent<Transform>().position;
		return rightGroupStart + rightOffset(droneNumber);
	}
	private Vector3 rightOffset(int droneNumber)
	{
		return new Vector3(droneNumber/dronesPerGroup * displacementX, 0, droneNumber%dronesPerGroup * displacementZ);
	}


    private void TargetHome()
    {
      //  Vector3 leftStart = LeftDronesHome.GetComponent<Transform>().position;
      //  Vector3 rightStart = RightDronesHome.GetComponent<Transform>().position;

		for (int i = 0; i < DronesPerHand; i++)
        {
            LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = leftStart(i);
            LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;
            RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = rightStart(i);
            RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;
        }
    }

	private void TargetPlayer()
	{
		Vector3 target = PlayerTarget.GetComponent<Transform>().position;
		TargetPoint (target);
	}

	private void TargetPoint( Vector3 target)
	{
		for (int i = 0; i < DronesPerHand; i++)
		{
			LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target + leftOffset(i) - new Vector3(0.5f*displacementX,0f,0f);
			LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;

			RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target + rightOffset(i) + new Vector3(0.5f*displacementX,0f,0f);
			RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;

		}
	}

    internal void FingerBend(int group, float fingerBendPercent)
    {

		var dronesPerGroup = DronesPerHand / 10;
        for (int i = 0; i < dronesPerGroup; i++)
        {
            var droneNumber = group * dronesPerGroup + i;
            GameObject drone = RightDrones[droneNumber];
            Transform transformToMove = drone.GetComponent<Transform>();

            float dist = Vector3.Distance(transformToMove.localPosition, PlayerTarget.transform.localPosition);

            float modifier = 0f;
            if (dist < 10.0)
            {
                modifier = 10f;
            }

            Vector3 pos = new Vector3(transformToMove.localPosition.x + modifier, fingerBendPercent * (float)maxHeight, transformToMove.localPosition.z + modifier);

            transformToMove.localPosition = pos;
        }
        return;

    }

	internal void LeftNoteHit(int droneGroup)
	{
        if (ZoneController.isDroneGloveLeft)
        {

            HitDroneGroup(droneGroup, LeftDrones);
        }
	}

	internal void RightNoteHit(int droneGroup)
	{
        if (ZoneController.isDroneGloveRight)
        {

            HitDroneGroup(droneGroup, RightDrones);
        }
	}

	internal void HitDroneGroup(int droneGroup, GameObject[] drones )
	{

		for (int i = 0; i < dronesPerGroup; i++)
		{
			var droneNumber = droneGroup * dronesPerGroup + i;
			GameObject drone = drones[droneNumber];
			HitDrone(drone);

		}
		return;
	}

    internal void HitDrone(GameObject drone)
    {

        Transform transformToMove = drone.GetComponent<Transform>();

        Vector3 pos = new Vector3(transformToMove.localPosition.x + NoteHitForce, transformToMove.localPosition.y, transformToMove.localPosition.z + NoteHitForce);

        //transformToMove.localPosition = pos;

        drone.GetComponentInChildren<Animation>().Play();

    }

}