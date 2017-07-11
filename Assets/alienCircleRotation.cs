using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienCircleRotation : MonoBehaviour {

    Animator circleAnimator;
    // int openHash = Animator.StringToHash("PyramidProjectionOpen");

    // Use this for initialization
    void Start()
    {

        circleAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Debug.Log("trigger pyramid open");
            circleAnimator.SetTrigger("alienStop");


        }

       // AnimatorStateInfo stateInfo = circleAnimator.GetCurrentAnimatorStateInfo(0);
       // Debug.Log(stateInfo.fullPathHash);
        if (Input.GetKeyDown(KeyCode.Period) )
        {
            Debug.Log("trigger pyramid close");
            
            circleAnimator.SetTrigger("alienStart");

        }


    }
}
