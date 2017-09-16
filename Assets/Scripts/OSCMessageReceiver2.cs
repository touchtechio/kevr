using OSCsharp.Data;
using System.Collections.Generic;
using UnityEngine;


namespace UniOSC
{
    [AddComponentMenu("ECS/OSCMessageReceiver2")]

    public class OSCMessageReceiver2 : BaseOSC
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
        public ZoneControllerSingleAny ZoneController;
        public ZoneControllerSingleAny ZoneController2;
        public ZoneControllerStageRadial ZoneControllerStageRadial;
        public ZoneControllerStageMess ZoneControllerStageSquare;
        public GloveController GloveController;
        public GloveController StageGloveController;
        public StickController StageStickController;
     //   public StickController StageStickControllerRight;
        public FountainRSControllerVR FountainRSController;
		public DroneController DroneController;
		public DronePianoController DronePianoController;
        public FingerControl LeftFingerController;
        public FingerControl RightFingerController;
        public FingerControl StageLeftFingerController;
        public FingerControl StageRightFingerController;
        public ParticleLauncher ParticleLauncherLeft;
		public ParticleLauncher ParticleLauncherRight;
        public FingerControl IMURightFingerController;


        OscMessage msg;

        //imu glove osc addresse
        private const string imuGlove = "fimu/joints"; 

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
        private const string wristLeft = "Kevin/wristLeft";
        private const string wristRight = "Kevin/wristRight";

        /// <summary>
        /// glove refers to finger bend data in touch osc
        /// </summary>
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


        // position
        private const string stagePositionLeft = "position/left-hand";
        private const string stagePositionRight = "position/right-hand";
        private const string stagePositionDrumLeft = "position/drum-left";
        private const string stagePositionDrumRight = "position/drum-right";
        private const string stageHitLeft = "gesture/drum-left/tap/";
        private const string stageHitRight = "gesture/drum-right/tap/";

        private string[] LeftNoteAddresses = { noteLeft1, noteLeft2, noteLeft3, noteLeft4, noteLeft5 };
        private string[] RightNoteAddresses = { noteRight1, noteRight2, noteRight3, noteRight4, noteRight5 };

        // glove midi config
        public int rightGloveMidiStart = 55;
        public int leftGloveMidiStart = 24;


        // Max OSC addresses
        private const string rightZone = "right/zone";
        private const string leftZone = "left/zone";

        //REALSENSE DRAGONFLY OSC address
        //       int[] realsense = { 90, 91, 124 };
        int[] activeRealsense = { 90, 91, 92, 93, 124, 125 };
        bool[] isRealsenseLeft = { true, false, false, true, true, true};
        private const string heartbeat = "heartbeat";


        private int rightZoneData;
        private int leftZoneData;

        private int LastHit = 0; // for deduping notes


        // upon message received from OSC, call LastMessageUpdate()
        public override void OnOSCMessageReceived(UniOSCEventArgs args)
        {
        
            msg = (OscMessage)args.Packet;

            string o = "";

            for (int i = 0; i < msg.Data.Count; i++)
            {
                o = msg.Address.ToString();
                o += " , ";
                o += msg.Data[i].ToString();
            }
            Debug.LogWarning(o);

            LastMessageUpdate();
            if (msg.Data.Count < 1 || isLive != true) return;

            // Debug.Log (msg.Data[1]);
            LastMessageUpdate();
            if (msg.Data.Count < 1 || isLive != true) return;

            //transform.rotation = new Quaternion (float.Parse((string)msg.Data [1]), float.Parse((string)msg.Data [2]), float.Parse((string)msg.Data [3]), float.Parse((string)msg.Data [0]))
        }

        // handles messages
        public void LastMessageUpdate()
        {
            // if no message, escape
            if (msg == null) return;

            // handles finger bends and wrist rotation for the real sense
            rsGloveData();

            // handles rs data from single camera
            rsZones();

            ImuGloveData();

            // handles osc data simulating hand position over realsense
            touchOSCZones();

            // handles Touch Osc data simulating notes
            touchOSCNotes();

            // handles Touch OSC data simulating glove
            touchOSCBEnds();

            touchOSCWrist();

            // handles finger bends, wrist rotation as well as stage positioning
            StageGloveData();

            // handles stick hits as well as stage positioning
            StageStickData();

        }

        private void ImuGloveData()
        {
            // syncphony glove MIDI note hits
            if (msg.Address.Contains(imuGlove))
            {
                string debugRadian = "fimu radian: ";
                string debugDegree = "fimu degree: ";
                float[] fingerBendDataIMUDeg;
                fingerBendDataIMUDeg = new float[15];

                List<float> fingerBendIMUList = new List<float>();

                for (int i = 0; i < 15; i++)
                {
                    float radians = (float)msg.Data[i];
                    debugRadian += " " + radians;
                    debugDegree += " " + Mathf.Rad2Deg * radians;
                    fingerBendIMUList.Add(Mathf.Rad2Deg * radians);
                }

                Debug.Log(debugDegree);
                Debug.Log(debugRadian);
                fingerBendDataIMUDeg = fingerBendIMUList.ToArray();
                Debug.Log(fingerBendDataIMUDeg[0]);
                IMURightFingerController.bendFingerIMU(fingerBendDataIMUDeg);
            }

        }


        private void touchOSCWrist()
        {
            // syncphony glove MIDI note hits
            if (msg.Address.Contains(wristLeft))
            {
                float percent = (float)msg.Data[0];
                //Debug.Log("left-wrist:" + degrees);
                GloveController.SetLeftWristAngle((int)(180 * percent));
            }

            if (msg.Address.Contains(wristRight))
            {
                float percent = (float)msg.Data[0];
                //Debug.Log("right-wrist:" + degrees);
                GloveController.SetRightWristAngle((int)(180 * percent));
            }
        }

        private void StageGloveData()
        {
            if (msg.Address.Contains(stagePositionLeft))
            {
                float xPos = -(float)msg.Data[0];
                float zPos = -(float)msg.Data[1];
                float yPos = (float)msg.Data[2];
   
                Debug.Log("stage-pos-left: " + xPos + " " + yPos + " " + zPos);
               // StageGloveController.SetLeftPosition(xPos, yPos, zPos);

               // ZoneControllerStage.UpdateZone(xPos, yPos, zPos);
                Vector3 currentZoneObjectPosition = ZoneControllerStageSquare.GetZoneObjectPosition();
                StageGloveController.SetLeftGlovePositionWithZone(new Vector3(xPos, yPos, zPos), currentZoneObjectPosition);
            }

            if (msg.Address.Contains(stagePositionRight))
            {
                float xPos = -(float)msg.Data[0];
                float zPos = -(float)msg.Data[1];
                float yPos = (float)msg.Data[2];

                //Debug.Log("stage-pos-right: " + xPos + " " + yPos + " " + zPos);
               // StageGloveController.SetRightPosition(xPos, yPos, zPos);
                //ZoneControllerStageRadial.UpdateZone(xPos, yPos, zPos);
                Vector3 currentZoneObjectPosition = ZoneControllerStageSquare.GetZoneObjectPosition();
                StageGloveController.SetRightGlovePositionWithZone(new Vector3(xPos, yPos, zPos), currentZoneObjectPosition);
                   
            }

            // syncphony glove wrist rotation
            if (msg.Address.Contains(oscLeftHandWrist))
            {
                int degrees = (int)msg.Data[1];
                //  Debug.Log("left-wrist:" + degrees);
                StageGloveController.SetLeftWristAngle(degrees);
            }
            if (msg.Address.Contains(oscRightHandWrist))
            {
                int degrees = (int)msg.Data[1];
                // Debug.Log("right-wrist:" + degrees);
                StageGloveController.SetRightWristAngle(degrees);
            }

            // syncphony glove wrist rotation
            if (msg.Address.Contains(oscLeftWristCc))
            {
                int degrees = (int)msg.Data[0];
               // Debug.Log("left-wrist:" + degrees);
                StageGloveController.SetLeftWristAngle(degrees - 30);
            }

            if (msg.Address.Contains(oscRightWristCc))
            {
                int degrees = (int)msg.Data[0];
                //Debug.Log("right-wrist:" + degrees);
                StageGloveController.SetRightWristAngle(-degrees);
            }

            // syncphony glove MIDI note hits
            if (msg.Address.Contains(oscLeftNote))
            {
                int note = (int)msg.Data[1];
                int droneGroup = 4 - (note - leftGloveMidiStart);
                // Debug.Log("left-note:" + note + " droneGroup:" + droneGroup);
                //DroneController.LeftNoteHit(droneGroup); // note is a midi note

                //int fountainNumber = leftGloveMidiStart - note + 4;
                //Debug.Log("left-note:" + note + " fountainNumber:" + fountainNumber);

                //FountainRSController.fountainMidiLeft(fountainNumber);
                //ParticleLauncherLeft.launchParticle(fountainNumber);
            }

            if (msg.Address.Contains(oscRighttNote))
            {
                int note = (int)msg.Data[1];
                int droneGroup = note - rightGloveMidiStart - 5;
                //   Debug.Log("right-note:" + droneGroup);
                /*DroneController.RightNoteHit(droneGroup);

                int fountainNumber = note - rightGloveMidiStart;
                Debug.Log("right-note:" + fountainNumber);
                FountainRSController.fountainMidiRight(fountainNumber - 5);
                ParticleLauncherRight.launchParticle(fountainNumber - 5);*/

            }
        }

        private void StageStickData()
        {
            if (msg.Address.Contains(stagePositionDrumLeft))
            {
                float xPos = -(float)msg.Data[0];
                float zPos = -(float)msg.Data[1];
                float yPos = (float)msg.Data[2];
               // Debug.Log("stick-pos: " + xPos + " " + yPos + " " + zPos);
                // hard coding y pos
                yPos = 0f;
                //yPos = 0.5f;

                ZoneControllerStageRadial.UpdateLeftZone(xPos, yPos, zPos); // sends in new stick positions
                Vector3 currentZoneObjectPosition = ZoneControllerStageRadial.GetLeftStickPosition(); // figures out where the stick should be on stage
                StageStickController.SetLeftStickPositionWithZone(new Vector3(xPos, yPos, zPos), currentZoneObjectPosition);
                GameObject drumstick = ZoneControllerStageRadial.drumstickLeft;
                int selectedZone = ZoneControllerStageRadial.selectedLeftZone;
                int stageZoneSlices = ZoneControllerStageRadial.stageZoneSlices;
                StageStickController.SetLeftStickAngle(drumstick, selectedZone * 360 / stageZoneSlices);
                // for radial zone calc

            }

            if (msg.Address.Contains(stagePositionDrumRight))
            {
                float xPos = -(float)msg.Data[0];
                float zPos = -(float)msg.Data[1];
                float yPos = (float)msg.Data[2];
                // Debug.Log("stick-pos: " + xPos + " " + yPos + " " + zPos);
                // hard coding y pos
                yPos = 0f;
                //yPos = 0.5f;

                ZoneControllerStageRadial.UpdateRightZone(xPos, yPos, zPos); // sends in new stick positions
                Vector3 currentZoneObjectPosition = ZoneControllerStageRadial.GetRightStickPosition(); // figures out where the stick should be on stage
                StageStickController.SetRightStickPositionWithZone(new Vector3(xPos, yPos, zPos), currentZoneObjectPosition);
                GameObject drumstick = ZoneControllerStageRadial.drumstickRight;
                int selectedZone = ZoneControllerStageRadial.selectedRightZone;
                int stageZoneSlices = ZoneControllerStageRadial.stageZoneSlices;
                StageStickController.SetRightStickAngle(drumstick, selectedZone * 360 / stageZoneSlices);
                // for radial zone calc

            }

            if (msg.Address.Contains(stageHitLeft))
            {
                int channel = (int)msg.Data[0];
                int hitVel = (int)msg.Data[1];

                Debug.Log("detected hit " + channel);
                // hard coding y pos
                if (channel != LastHit)
                {
                    Debug.Log("detected NEW hit w seq " + channel);

                    StageStickController.drumHitLeft(true, hitVel, channel);
                }

                LastHit = channel;
            }

            if (msg.Address.Contains(stageHitRight))
            {
                int channel = (int)msg.Data[0];
                int hitVel = (int)msg.Data[1];

                Debug.Log("detected hit " + channel);
                // hard coding y pos
                if (channel != LastHit)
                {
                    Debug.Log("detected NEW hit w seq " + channel);

                    StageStickController.drumHitRight(true, hitVel, channel);
                }

                LastHit = channel;
            }

        }


        // handles positioning from rs camera
        private void rsZones()
        {
            for (int i = 0; i < activeRealsense.Length; i++)
            {
 
                    
                //create zone objects
           

                string leftCursor = "/cursor/s" + activeRealsense + "/left";
                string rightCursor = "/cursor/s" + activeRealsense + "/right";

                if (msg.Address.Contains(leftCursor) || msg.Address.Contains(rightCursor))
                {

                    float xPos = -(float)msg.Data[0];
                    float zPos = (float)msg.Data[1];
                    float yPos = (float)msg.Data[2];

                  //  rotated the cameras for kevin
                        xPos = -xPos;
                        zPos = -zPos;
                 


                    // choose whether to send data to left or right rs camera

                    if (msg.Address.Contains(leftCursor))
                    {
                        if (isRealsenseLeft[i])
                        {
                            GloveController.rsZoneLeftY(yPos);
                            GloveController.rsZoneLeftXZ(xPos, zPos);
                            ZoneController2.UpdateZone(xPos, yPos, zPos, isRealsenseLeft[i]);
                        }
                    }
                    else if (msg.Address.Contains(rightCursor))
                    {
                        if (!isRealsenseLeft[i])
                        {
                            GloveController.rsZoneRightY(yPos);
                            GloveController.rsZoneRightXZ(xPos, zPos);
                            ZoneController.UpdateZone(xPos, yPos, zPos, isRealsenseLeft[i]);

                        }
                    }


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
                  
                    LeftFingerController.bendFinger(i, fingerBendDataLeft); // send finger bend data to UI
                    StageLeftFingerController.bendFinger(i, fingerBendDataLeft);
                    //FountainRSController.fountainHeightLeft(i, fingerBendDataLeft);
                    //DronePianoController.FingerBend(4 - i, fingerBendDataLeft);



                }

                if (msg.Address.Contains(fingerBendRight[i]))
                {
                 //   Debug.Log("right data");
                    float fingerBendDataRight = (float)msg.Data[0];
                   
                    RightFingerController.bendFinger(i, fingerBendDataRight); // send finger bend data to UI
                    StageRightFingerController.bendFinger(i, fingerBendDataRight);
                    //FountainRSController.fountainHeightRight(4-i, fingerBendDataRight);
                    //DronePianoController.FingerBend(i + 5, fingerBendDataRight);

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
                    FountainRSController.fountainMidiLeft(i);
					ParticleLauncherLeft.launchParticle (i);
                   

                }
                if (msg.Address.Contains(RightNoteAddresses[i]))
                {
                    Debug.Log("note:" + i);
                    DroneController.RightNoteHit(i);
                    FountainRSController.fountainMidiRight(i);
                    ParticleLauncherRight.launchParticle(i);
                }
            }
        }

        private void touchOSCZones()
        {
           
          
            if (msg.Address.Contains(rsZoneLeftXZ))
            {
              

                float incomingTouchOscZ = (float)msg.Data[0];
                float incomingTouchOscX = (float)msg.Data[1];

                GloveController.rsZoneLeftXZ(0.35f - 0.7f * incomingTouchOscX, 0.2f * incomingTouchOscZ - 0.1f);

                Vector3 position = GloveController.GetLeftPosition();
                ZoneController2.UpdateZone(position.x, position.y, position.z, true);

            }

            if (msg.Address.Contains(rsZoneLeftY))
            {
             
                float incomingTouchOscY = (float)msg.Data[0];

                GloveController.rsZoneLeftY(incomingTouchOscY * 0.5f + 0.3f); // movement of the glove asset

                Vector3 position = GloveController.GetLeftPosition();
                ZoneController2.UpdateZone(position.x, position.y, position.z, true);
          

            }

            if (msg.Address.Contains(rsZoneRightXZ))
            {
                float incomingTouchOscZ = (float)msg.Data[0];
                float incomingTouchOscX = (float)msg.Data[1];

                // convert from touch osc to rs coordinates
                GloveController.rsZoneRightXZ(0.7f * incomingTouchOscX - 0.35f, 0.2f * incomingTouchOscZ - 0.1f);

                Vector3 position = GloveController.GetRightPosition();
                ZoneController.UpdateZone(position.x, position.y, position.z, false);
 
            }

            if (msg.Address.Contains(rsZoneRightY))
            {
                
                float incomingTouchOscY = (float)msg.Data[0];
                Debug.Log("touchOSCY "+incomingTouchOscY);
                GloveController.rsZoneRightY(incomingTouchOscY * 0.5f + 0.3f);

                Vector3 position = GloveController.GetRightPosition();
                ZoneController.UpdateZone(position.x, position.y, position.z, false); // separate zone controller for left and right
      

            }
        }

        private void rsGloveData()
        {
            // syncphony glove finger bends
            if (msg.Address.Contains(oscLeftHandFingers))
            {
               // Debug.Log("left:" + msg.Data[1] + "," + msg.Data[2] + "," + msg.Data[3] + "," + msg.Data[4] + "," + msg.Data[5]);
                for (int i = 0; i < 5; i++)
                {
                    float fingerBendData = (float)msg.Data[i + 1];
 //                   float fingerBendData = GloveController.GetFingerBend(4 - i, (int)msg.Data[i + 1]);
                    FountainRSController.fountainHeightLeft(i, fingerBendData);
                    LeftFingerController.bendFinger(i, fingerBendData);
                    StageLeftFingerController.bendFinger(i, fingerBendData);

                    // DronePianoController.FingerBend(4 - i, fingerBendData);


                }
            }
            if (msg.Address.Contains(oscRightHandFingers))
            {
                //   Debug.Log("right:" + msg.Data[1] + "," + msg.Data[2] + "," + msg.Data[3] + "," + msg.Data[4] + "," + msg.Data[5]);
                for (int i = 0; i < 5; i++)
                {
                    float fingerBendData = (float)msg.Data[i + 1];
                    //float fingerBendData = GloveController.GetFingerBend(i + 5, (int)msg.Data[i + 1]);
                    FountainRSController.fountainHeightRight(i, fingerBendData);
                    RightFingerController.bendFinger(i, fingerBendData);
                    StageRightFingerController.bendFinger(i, fingerBendData);

                    // DronePianoController.FingerBend(i + 5, fingerBendData);

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
                GloveController.SetLeftWristAngle(degrees-30);
            }

            if (msg.Address.Contains(oscRightWristCc))
            {
                int degrees = (int)msg.Data[0];
                //Debug.Log("right-wrist:" + degrees);
                GloveController.SetRightWristAngle(-degrees);
            }

            // syncphony glove MIDI note hits
            if (msg.Address.Contains(oscLeftNote))
            {
                int note = (int)msg.Data[1];
                int droneGroup = 4 - (note - leftGloveMidiStart);
               // Debug.Log("left-note:" + note + " droneGroup:" + droneGroup);
                DroneController.LeftNoteHit(droneGroup); // note is a midi note

                int fountainNumber = leftGloveMidiStart - note + 4;
                //Debug.Log("left-note:" + note + " fountainNumber:" + fountainNumber);

                FountainRSController.fountainMidiLeft(fountainNumber);
                ParticleLauncherLeft.launchParticle(fountainNumber);
            }

            if (msg.Address.Contains(oscRighttNote))
            {
                int note = (int)msg.Data[1];
                int droneGroup = note - rightGloveMidiStart - 5;
             //   Debug.Log("right-note:" + droneGroup);
                DroneController.RightNoteHit(droneGroup);

                int fountainNumber = note - rightGloveMidiStart;
                //Debug.Log("right-note:" + fountainNumber);
                FountainRSController.fountainMidiRight(fountainNumber-5);
                ParticleLauncherRight.launchParticle(fountainNumber-5);

            }
        }
    }
}
