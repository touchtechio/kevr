using UnityEngine;
using System.Collections;
using System;

public class DroneInstantiator : MonoBehaviour
{

    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    int _amount = 100;

    [SerializeField]
    int displacementX = 5;

    [SerializeField]
    int displacementZ = 5;

    [SerializeField]
    int maxHeight = 50;

    [SerializeField]
    GameObject[] drones;

    [SerializeField]
    GameObject player;


    // Use this for initialization
    void Start()
    {
        drones = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {
            GameObject drone = Instantiate(_prefab, transform.position + new Vector3(i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            drone.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(i / (float)_amount, .9f, .5f);
            drones[i] = drone;
        }

        //Destroy(this);
    }

    internal void FingerBend(int group, float fingerBendPercent)
    {

        var dronesPerGroup = _amount / 10;
        for (int i = 0; i < dronesPerGroup; i++)
        {
            var droneNumber = group * dronesPerGroup + i;
            GameObject drone = drones[droneNumber];
            Transform transformToMove = drone.GetComponent<Transform>();

            float dist = Vector3.Distance(transformToMove.localPosition, player.transform.localPosition);

            float modifier = 0f;
            if (dist < 10.0)
            {
                modifier = 10f;
            }

            Vector3 pos = new Vector3(transformToMove.localPosition.x+modifier, fingerBendPercent * (float)maxHeight, transformToMove.localPosition.z+modifier);

            transformToMove.localPosition = pos;
        }
        return;

    }
}