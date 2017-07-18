using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsUser : MonoBehaviour {

  //  public Transform target;
    private Transform targetObject;
  //  public Transform source;
    public float speed;

    void Start()
    {
        targetObject = GameObject.Find("[CameraRig]").transform.GetChild(2);
        
    }

    void Update()
    {
        //float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, 0.0f);
        Orient();
    }


    void Orient()
    {
        //transform.LookAt(target.position);
       transform.LookAt(new Vector3(targetObject.position.x, transform.position.y, targetObject.position.z));
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        transform.rotation = Quaternion.Euler(rot);
        /*new 
          Vector3 targetDir = target.position - transform.position;
          float step = speed * Time.deltaTime;
          Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        //  Debug.DrawRay(transform.position, newDir, Color.red);
          transform.rotation = Quaternion.LookRotation(newDir);//*/
    }


}
