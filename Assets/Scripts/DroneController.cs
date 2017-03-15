using UnityEngine;
using System.Collections;
using System;

public class DroneController : MonoBehaviour
{
    [Header("drone settings")]
    public GameObject _prefab;
    public int maxHeight = 50;


    [Header("drone group settings")]
    public int _amount = 15;
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



    // Use this for initialization
    void Start()
    {
        LeftDrones = new GameObject[_amount];
        RightDrones = new GameObject[_amount];

        Vector3 leftStart = LeftDronesHome.GetComponent<Transform>().position;
        Vector3 rightStart = RightDronesHome.GetComponent<Transform>().position;

        Vector3 target = PlayerTarget.GetComponent<Transform>().position;

        for (int i = 0; i < _amount; i++)
        {
            GameObject drone = Instantiate(_prefab, leftStart + new Vector3(-i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            drone.name = "drone-left-" + i;
            drone.GetComponentInChildren<MeshRenderer>().material.color = Color.HSVToRGB(i / (float)_amount, .5f, .5f);
            //drone.GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target;
            LeftDrones[i] = drone;
        }

        for (int i = 0; i < _amount; i++)
        {
            GameObject drone = Instantiate(_prefab, rightStart + new Vector3(i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            drone.name = "drone-right-" + i;
            drone.GetComponentInChildren<MeshRenderer>().material.color = Color.HSVToRGB(i / (float)_amount, .5f, .5f);
            //drone.GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target;
            RightDrones[i] = drone;
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("targeting player");
            TargetPlayer();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("targeting home");
            TargetHome();
        }

    }

    private void TargetHome()
    {
        Vector3 leftStart = LeftDronesHome.GetComponent<Transform>().position;
        Vector3 rightStart = RightDronesHome.GetComponent<Transform>().position;

        for (int i = 0; i < _amount; i++)
        {
            LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = leftStart;
            LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;
            RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = rightStart;
            RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;
        }
    }

    private void TargetPlayer()
    {
      
        Vector3 leftStart = LeftDronesHome.GetComponent<Transform>().position;
        Vector3 rightStart = RightDronesHome.GetComponent<Transform>().position;

        Vector3 target = PlayerTarget.GetComponent<Transform>().position;

        for (int i = 0; i < _amount; i++)
        {
            LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target;
            LeftDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;

            RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().TargetPoint = target;
            RightDrones[i].GetComponent<UnitySteer.Behaviors.SteerForPoint>().enabled = true;

        }
    }

    internal void FingerBend(int group, float fingerBendPercent)
    {

        var dronesPerGroup = _amount / 10;
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


    internal void LeftMidiNoteHit(int note)
    {
        int droneGroup = 5 - (note - leftGloveMidiStart);
        LeftNoteHit(droneGroup);
    }

   internal void LeftNoteHit(int droneGroup)
    {

        var dronesPerGroup = _amount / 5;
        for (int i = 0; i < dronesPerGroup; i++)
        {
            var droneNumber = droneGroup * dronesPerGroup + i;
            Debug.Log("index:" + droneNumber);
            GameObject drone = LeftDrones[droneNumber];
            Transform transformToMove = drone.GetComponent<Transform>();

            Vector3 pos = new Vector3(transformToMove.localPosition.x + NoteHitForce, transformToMove.localPosition.y, transformToMove.localPosition.z + NoteHitForce);

            transformToMove.localPosition = pos;
        }
        return;
    }

    internal void RightMidiNoteHit(int note)
    {
        int droneGroup = note - rightGloveMidiStart - 5;
        RightNoteHit(droneGroup);
    }

    internal void RightNoteHit(int droneGroup)
    {

        var dronesPerGroup = _amount / 5;
        for (int i = 0; i < dronesPerGroup; i++)
        {
            var droneNumber = droneGroup * dronesPerGroup + i;
            GameObject drone = RightDrones[droneNumber];
            Transform transformToMove = drone.GetComponent<Transform>();

            Vector3 pos = new Vector3(transformToMove.localPosition.x + NoteHitForce, transformToMove.localPosition.y, transformToMove.localPosition.z + NoteHitForce);

            transformToMove.localPosition = pos;
        }
        return;
    }
}