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
        public ZoneController ZoneController;
        public GloveController GloveController;
        public FountainRSControllerVR FountainRSController;
        public DroneController DroneController;

        OscMessage msg;


        //Syncphony osc addresses

        private const string oscSideTap = "gesture/ring-right-hand/sidetap";
        private const string oscTap = "gesture/ring-right-hand/tap";
        private const string oscOrientation = "motion/ring-right-hand/orientation";
        private const string oscOmni = "gesture/ring-right-hand/omni";
        private const string oscRightHandFingers = "gesture/right-hand/fingers";
        private const string oscLeftHandFingers = "gesture/left-hand/fingers";
        private const string oscRightHandWrist = "gesture/right-hand/wrist";
        private const string oscLeftHandWrist = "gesture/left-hand/wrist";
        private const string oscLeftNote = "midi/1";
        private const string oscRighttNote = "midi/2";




        //Touch OSC addresses
        private const string gloveLeft1 = "Kevin/gloveLeft1";
        private const string gloveLeft2 = "Kevin/gloveLeft2";
        private const string gloveLeft3 = "Kevin/gloveLeft3";
        private const string gloveLeft4 = "Kevin/gloveLeft4";
        private const string gloveLeft5 = "Kevin/gloveLeft5";
        private const string gloveRight1 = "Kevin/gloveRight1";
        private const string gloveRight2 = "Kevin/gloveRight2";
        private const string gloveRight3 = "Kevin/gloveRight3";
        private const string gloveRight4 = "Kevin/gloveRight4";
        private const string gloveRight5 = "Kevin/gloveRight5";
        private const string rsZoneLeftXZ = "Kevin/zoneLeftXZ";
        private const string rsZoneLeftY = "Kevin/zoneLeftY";
        private const string rsZoneRightXZ = "Kevin/zoneRightXZ";
        private const string rsZoneRightY = "Kevin/zoneRightY";

        private string[] fingerBendLeft = { gloveLeft1, gloveLeft2, gloveLeft3, gloveLeft4, gloveLeft5 };
        private string[] fingerBendRight = { gloveRight1, gloveRight2, gloveRight3, gloveRight4, gloveRight5 };


        private const string noteLeft1 = "Kevin/noteLeft1";
        private const string noteLeft2 = "Kevin/noteLeft2";
        private const string noteLeft3 = "Kevin/noteLeft3";
        private const string noteLeft4 = "Kevin/noteLeft4";
        private const string noteLeft5 = "Kevin/noteLeft5";
        private const string noteRight1 = "Kevin/noteRight1";
        private const string noteRight2 = "Kevin/noteRight2";
        private const string noteRight3 = "Kevin/noteRight3";
        private const string noteRight4 = "Kevin/noteRight4";
        private const string noteRight5 = "Kevin/noteRight5";


        private string[] LeftNoteAddresses = { noteLeft1, noteLeft2, noteLeft3, noteLeft4, noteLeft5 };
        private string[] RightNoteAddresses = { noteRight1, noteRight2, noteRight3, noteRight4, noteRight5 };



        // Max OSC addresses
        private const string rightZone = "right/zone";
        private const string leftZone = "left/zone";

        //REALSENSE DRAGONFLY OSC address
        private const string cursorLeft = "/cursor/s124/left";
        private const string cursorRight = "/cursor/s124/right";
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


            // handles OSC data simulating glove
            for (int i = 0; i < 5; i++)
            {
                if (msg.Address.Contains(fingerBendLeft[i]))
                {
                    float fingerBendDataLeft = (float)msg.Data[0];
                    GloveController.fingerBendLeft(i, fingerBendDataLeft);
                    FountainRSController.fountainHeightLeft(i, fingerBendDataLeft);
                    DroneController.FingerBend(4 - i, fingerBendDataLeft);

                }

                if (msg.Address.Contains(fingerBendRight[i]))
                {
                    //Debug.Log("right data");
                    float fingerBendDataRight = (float)msg.Data[0];
                    GloveController.fingerBendRight(i, fingerBendDataRight);
                    FountainRSController.fountainHeightRight(i, fingerBendDataRight);
                    DroneController.FingerBend(i + 5, fingerBendDataRight);

                }
            }


            // handles Touch Osc data simulating notes
            for (int i = 0; i < 5; i++)
            {
                if (msg.Address.Contains(LeftNoteAddresses[i]))
                {
                    Debug.Log("note:" + i);
                    DroneController.LeftNoteHit(i);
                }
                if (msg.Address.Contains(RightNoteAddresses[i]))
                {
                    Debug.Log("note:" + i);
                    DroneController.RightNoteHit(i);
                }
            }


            //
            //  SYNCPHONY
            //

            // syncphony glove finger bends
            if (msg.Address.Contains(oscLeftHandFingers))
            {
                //   Debug.Log("left:" + msg.Data[1] + "," + msg.Data[2] + "," + msg.Data[3] + "," + msg.Data[4] + "," + msg.Data[5]);
                for (int i = 0; i < 5; i++)
                {
                    float fingerBendData = GloveController.GetFingerBend(i, (int)msg.Data[i + 1]);
                    FountainRSController.fountainHeightLeft(i, fingerBendData);

                }
            }
            if (msg.Address.Contains(oscRightHandFingers))
            {
                //   Debug.Log("right:" + msg.Data[1] + "," + msg.Data[2] + "," + msg.Data[3] + "," + msg.Data[4] + "," + msg.Data[5]);
                for (int i = 0; i < 5; i++)
                {
                    float fingerBendData = GloveController.GetFingerBend(i + 5, (int)msg.Data[i + 1]);
                    FountainRSController.fountainHeightRight(i, fingerBendData);
                }
            }

            // syncphony glove wrist rotation
            if (msg.Address.Contains(oscLeftHandWrist))
            {
                int degrees = (int)msg.Data[1];
                //  Debug.Log("left-wrist:" + degrees);
                GloveController.SetLeftWristAngle(degrees);
            }
            if (msg.Address.Contains(oscRightHandWrist))
            {
                int degrees = (int)msg.Data[1];
                // Debug.Log("right-wrist:" + degrees);
                GloveController.SetRightWristAngle(degrees);
            }

            // syncphony glove MIDI note hits
            if (msg.Address.Contains(oscLeftNote))
            {
                int note = (int)msg.Data[1];
                Debug.Log("left-note:" + note);
                DroneController.LeftMidiNoteHit(note); // note is a midi note
            }

            if (msg.Address.Contains(oscRighttNote))
            {
                int note = (int)msg.Data[1];
                Debug.Log("right-note:" + note);
                DroneController.RightMidiNoteHit(note);
            }

            // handles osc data simulating hand position over realsense
            if (msg.Address.Contains(rsZoneLeftXZ))
            {
                float rsZoneDataLeftX = (float)msg.Data[0];
                float rsZoneDataLeftZ = (float)msg.Data[1];
                GloveController.rsZoneLeftXZ(rsZoneDataLeftX, rsZoneDataLeftZ);
            }

            if (msg.Address.Contains(rsZoneLeftY))
            {
                float rsZoneDataLeftY = (float)msg.Data[0];
                GloveController.rsZoneLeftY(rsZoneDataLeftY);
            }

            if (msg.Address.Contains(rsZoneRightXZ))
            {
                float rsZoneDataRightX = (float)msg.Data[0];
                float rsZoneDataRightZ = (float)msg.Data[1];
                GloveController.rsZoneRightXZ(rsZoneDataRightX, rsZoneDataRightZ);
            }

            if (msg.Address.Contains(rsZoneRightY))
            {
                float rsZoneDataRightY = (float)msg.Data[0];
                GloveController.rsZoneRightY(rsZoneDataRightY);
            }

            // handles rs data from single camera
            if (msg.Address.Contains(cursorLeft))
            {

                float xPos = (float)msg.Data[0];
                float zPos = (float)msg.Data[1];
                float yPos = (float)msg.Data[2];


                GloveController.rsZoneLeftY(yPos);
                GloveController.rsZoneLeftXZ(-zPos, -xPos);
                ZoneController.UpdateLeftZone(xPos, zPos, yPos);

            }
            if (msg.Address.Contains(cursorRight))
            {

                float xPos = (float)msg.Data[0];
                float zPos = (float)msg.Data[1];
                float yPos = (float)msg.Data[2];


                GloveController.rsZoneRightY(yPos);
                GloveController.rsZoneRightXZ(-zPos, -xPos);
                ZoneController.UpdateRightZone(xPos, zPos, yPos);



                Debug.Log(xPos);

            }

            // handles osc rs data from Max
            if (msg.Address.Contains(rightZone))
            {
                rightZoneData = (int)msg.Data[0];
                //Debug.Log(rightZoneData);
                ZoneController.rightZoneColor(rightZoneData);

            }
            else if (msg.Address.Contains(leftZone))
            {
                leftZoneData = (int)msg.Data[0];
                ZoneController.leftZoneColor(leftZoneData);
            }


        }

    }
}
