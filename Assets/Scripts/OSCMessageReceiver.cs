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
        private const string oscLeftWristCc = "midicc/1";
        private const string oscRightWristCc = "midicc/2";




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
 //       int[] realsense = { 90, 91, 124 };
        int[] realsense = { 91, 124 };
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

            //
            //  SYNCPHONY

            SynphonyGloveData();

            // handles osc data simulating hand position over realsense
            touchOSCZones();


            // handles Touch Osc data simulating notes
            touchOSCNotes();

            // handles Touch OSC data simulating glove
            touchOSCBEnds();


            // handles rs data from single camera
            rsZones();

            // handles osc rs data from Max
            rsZonesViaMax();

        }

        private void rsZonesViaMax()
        {
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

        private void rsZones()
        {

            for (int i = 0; i < realsense.Length; i++)
            {
                string cursorLeft = "/cursor/s" + realsense[i] + "/left";
                string cursorRight = "/cursor/s"+ realsense[i] + "/right";


                if (msg.Address.Contains(cursorLeft))
                {

                    float xPos = (float)msg.Data[0];
                    float zPos = (float)msg.Data[1];
                    float yPos = (float)msg.Data[2];

                    if(91 == realsense[i])
                    {
                        xPos = -xPos;
                        zPos = -zPos;
                    }

                    GloveController.rsZoneLeftY(yPos);
                    GloveController.rsZoneLeftXZ(-zPos, -xPos);
                    ZoneController.UpdateLeftZone(xPos, yPos, zPos);
                    //                ZoneController.UpdateLeftZone(GloveController.GetLeftPosition());


                }
                if (msg.Address.Contains(cursorRight))
                {

                    float xPos = (float)msg.Data[0];
                    float zPos = (float)msg.Data[1];
                    float yPos = (float)msg.Data[2];


                    GloveController.rsZoneRightY(yPos);
                    GloveController.rsZoneRightXZ(-zPos, -xPos);
                    ZoneController.UpdateRightZone(xPos, yPos, zPos);

                    Debug.Log(xPos);

                }
            }
        }

        private void touchOSCBEnds()
        {
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
        }

        private void touchOSCNotes()
        {
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
        }

        private void touchOSCZones()
        {
            if (msg.Address.Contains(rsZoneLeftXZ))
            {
                float rsZoneDataLeftX = (float)msg.Data[0];
                float rsZoneDataLeftZ = (float)msg.Data[1];

                //todo:  x and z transforms are flipped
                GloveController.rsZoneLeftXZ(-0.3f * rsZoneDataLeftX + .1f, 0.3f - 0.5f * rsZoneDataLeftZ);
                //Debug.Log("left OSC zone data: " + rsZoneDataLeftX);

                Vector3 position = GloveController.GetLeftPosition();
                ZoneController.UpdateLeftZone(-position.z, position.y, -position.x);

            }

            if (msg.Address.Contains(rsZoneLeftY))
            {
                float rsZoneDataLeftY = (float)msg.Data[0];

                GloveController.rsZoneLeftY(rsZoneDataLeftY * 0.5f + 0.4f);


                Vector3 position = GloveController.GetLeftPosition();
                Debug.Log(position.y + " y pos");
                ZoneController.UpdateLeftZone(-position.z, position.y, position.x);

            }

            if (msg.Address.Contains(rsZoneRightXZ))
            {
                float rsZoneDataRightX = (float)msg.Data[0];
                float rsZoneDataRightZ = (float)msg.Data[1];

                // convert from touch osc to rs coordinates
                GloveController.rsZoneRightXZ(-0.3f * rsZoneDataRightX + .1f, 0.1f + 0.5f * rsZoneDataRightZ);

                Vector3 position = GloveController.GetLeftPosition();
                ZoneController.UpdateRightZone(position.z, position.y, -position.x);
            }

            if (msg.Address.Contains(rsZoneRightY))
            {
                float rsZoneDataRightY = (float)msg.Data[0];
                GloveController.rsZoneRightY(rsZoneDataRightY * 0.5f + 0.4f);

                Vector3 position = GloveController.GetLeftPosition();

                ZoneController.UpdateRightZone(-position.z, position.y, position.x);
            }
        }

        private void SynphonyGloveData()
        {
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
            if (msg.Address.Contains(oscLeftWristCc))
            {
                int degrees = (int)msg.Data[0];
                //Debug.Log("left-wrist:" + degrees);
                GloveController.SetLeftWristAngle(degrees+180);
            }

            if (msg.Address.Contains(oscRightWristCc))
            {
                int degrees = (int)msg.Data[0];
                //Debug.Log("right-wrist:" + degrees);
                GloveController.SetRightWristAngle(degrees-90);
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
        }
    }
}
