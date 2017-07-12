using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pyramidAnim : MonoBehaviour {

    Animator pyramidAnimator;
   // int openHash = Animator.StringToHash("PyramidProjectionOpen");

    // Use this for initialization
    void Start () {

        pyramidAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(GameControllerVR.HOTKEY_PYRAMID_OPEN))
        {
            Debug.Log("trigger pyramid open");
            pyramidAnimator.SetTrigger("pyramidOpen");
        

        }

       // AnimatorStateInfo stateInfo = pyramidAnimator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(stateInfo.fullPathHash);
        if (Input.GetKeyDown(GameControllerVR.HOTKEY_PYRAMID_CLOSE))// && stateInfo.fullPathHash == -1741390083)
        {
            Debug.Log("trigger pyramid close");
            pyramidAnimator.SetTrigger("pyramidWait");
            pyramidAnimator.SetTrigger("pyramidClose");

        }


    }
}
