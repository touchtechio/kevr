using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using OSCsharp.Data;


namespace UniOSC
{
    [AddComponentMenu("ECS/OSCMessageReceiver")]

    public class OSCMessageReceiver : BaseOSC
    {
        private const string oscSideTap = "gesture/ring-right-hand/sidetap";
        private const string oscTap = "gesture/ring-right-hand/tap";
        private const string oscOrientation = "motion/ring-right-hand/orientation";
        private const string oscOmni = "gesture/ring-right-hand/omni";

        // Use this for initialization

        private int noteVal;

        private int sequenceNo;
        private int wristRot;
        private int armVert;
        private int armHor;
        //private int sideTap;
        //private int 

        // private ButtonTest test;

        //public OSCDataMapper dataScript; // no longer using this
        public GameControllerVR GameVR;

        OscMessage msg;

        private const string rightZone = "right/zone";
        private const string leftZone = "left/zone";
        private const string cursor = "cursor";
        private const string heartbeat = "heartbeat";

        private int rightZoneData;
        private int leftZoneData;

        // upon message received from OSC, call LastMessageUpdate()
        public override void OnOSCMessageReceived(UniOSCEventArgs args)
        {

            msg = (OscMessage)args.Packet;


            //Debug.Log ((int)msg.Data[0]);


            //Debug.Log (msg.Data[1]);
            LastMessageUpdate();
            if (msg.Data.Count < 1 || isLive != true) return;

            //transform.rotation = new Quaternion (float.Parse((string)msg.Data [1]), float.Parse((string)msg.Data [2]), float.Parse((string)msg.Data [3]), float.Parse((string)msg.Data [0]))
        }

        // handles messages
        public void LastMessageUpdate()
        {
            // if no message, escape
            if (msg == null) return;


            if (msg.Address.Contains(cursor))
            {
                if (!msg.Address.Contains(heartbeat))
                {


                    float xPos = (float)msg.Data[0];

                    if (Math.Abs(xPos) < 0.05)
                    {
                        // zone center
                        GameVR.rightZoneColor(1);
                        GameVR.leftZoneColor(1);

                    }
                    else if (Math.Abs(xPos) < 0.15)
                    {

                        // zone middle

                        GameVR.rightZoneColor(2);
                        GameVR.leftZoneColor(2);
                    }
                    else
                    {
                        // zone outside
                        GameVR.rightZoneColor(3);
                        GameVR.leftZoneColor(3);
                    }
                    
                    Debug.Log(xPos);
                }

                //GameVR.rightZoneColor(rightZoneData);


            }

            if (msg.Address.Contains(rightZone))
            {
                rightZoneData = (int)msg.Data[0];
                //Debug.Log(rightZoneData);
                GameVR.rightZoneColor(rightZoneData);

            }
            else if (msg.Address.Contains(leftZone))
            {
                leftZoneData = (int)msg.Data[0];
                GameVR.leftZoneColor(leftZoneData);
            }
        }
    }
}



            /*

            // handle gestures if sequence is new
            else if (sequenceNo != (int)msg.Data[0])
            {

                sequenceNo = (int)msg.Data[0];


                if (msg.Address.Contains(oscSideTap))
                {
                    if ((int)msg.Data[1] > 40)
                    {
                      //  if ((wristRot > 160) || (wristRot <-150)) // if left hand device
                            if ((wristRot > 120) && (wristRot < 180)) // if right hand device, it's opposite direction to a side chop
                            {
                            Debug.Log("gesture-BACK (" + wristRot + "," + armVert + "):" + msg.Address + " " + msg.Data[1]);
                            GCSL.gestureBack();
                            // Debug.Log(msg.Data[0]);
                        }
                    }
                    if ((int)msg.Data[1] > 500)
                    {
                        GCSL.gestureHome();
                    }

                }

                if (msg.Address.Contains(oscTap))

                {

                    if ((int)msg.Data[1] > 10)
                    {

                        //  Debug.Log(msg.Data[0]);
                        //if (armVert > 0 && armVert < 45)
                        if (wristRot > 120 || (wristRot <-160)) // clamp velocity to prevent accidental hits by moving hand
                        {
                            Debug.Log("gesture-SCROLLDOWN (" + wristRot + "," + armVert + "):" + msg.Address + " vel: " + msg.Data[1] + " seq" + msg.Data[0]); // 0 is sequnce number, 1 is velocity
                            GCSL.gestureScrollDown();
                            GCSL.gestureToggle();
                        }

                        //  if (armVert > 120 && armVert < 150)
                        else if ((wristRot > -20) && (wristRot < 40))
                        {
                            Debug.Log("gesture-SCROLLUP (" + wristRot + "," + armVert + "):" + msg.Address + " vel: " + msg.Data[1] + " seq" + msg.Data[0]); // 0 is sequnce number, 1 is velocity
                            GCSL.gestureScrollUp();
                            GCSL.gestureToggle();
                        }

                        //if (armVert > 75 && armVert < 120)
                        else  if ((wristRot > 50) && (wristRot < 110))
                        {
                            Debug.Log("gesture-SELECT (" + wristRot + "," + armVert + "):" + msg.Address + " vel: " + msg.Data[1] + " seq" + msg.Data[0]); // 0 is sequnce number, 1 is velocity
                            GCSL.gestureSelect();
                        }
                        else if (wristRot > 140)
                        {
                            //Debug.Log("toggle (" + wristRot + "," + armVert + "):" + msg.Address + " vel: " + msg.Data[1] + " seq" + msg.Data[0]); // 0 is sequnce number, 1 is velocity
                            //GCSL.gestureToggle();
                        }
                        else
                        {
                            Debug.Log("tapRegistered (" + wristRot + "," + armVert + "):" + msg.Address + " vel: " + msg.Data[1] + " seq" + msg.Data[0]);
                        }
                    }
                }

                if (msg.Address.Contains(oscOmni))
                {
                    Debug.Log("gesture-omni (" + wristRot + "," + armVert + "):" + msg.Address + " vel: " + msg.Data[1] + " seq" + msg.Data[0]); // 0 is sequnce number, 1 is velocity
                    // GCSL.gestureHome();
                }

            }


            //this.noteVal = (int)msg.Data[0];
            //Debug.Log (noteVal);

            //Debug.Log(msg.Data);
            //SetStartingSideWithDevice ();

            //OSCDataMapper gestureMapper = GameObject.FindObjectOfType<OSCDataMapper>();

            //GestureInterface ttt = GameObject.FindObjectOfType<OSC_TTT>();
            //Debug.Log("interface: " + ttt);
            //gestureMapper.buttonScript = ttt;

            //GestureInterface button = GameObject.FindObjectOfType<ButtonTest>();
            //Debug.Log("interface: " + button);
            //dataScript.gestureScript = button;

            ////   dataScript.MapOSCData(noteVal); // send oscdata to gamecontroller


            /// Does same thing as above line
            // ButtonTest test =  GameObject.FindObjectOfType<ButtonTest>();



            //test.CallInOscData(this.noteVal);
            
           
        }
		void SetStartingSideWithDevice() {


			if (this.noteVal == 55) {
				
				GameObject.FindObjectOfType<GameControllerTTT> ().SetStartingSide ("X");
			} else if (this.noteVal == 53) {
				GameObject.FindObjectOfType<GameControllerTTT> ().SetStartingSide ("O");
			}
		}
        */

