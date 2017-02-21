using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using OSCsharp.Data;

namespace UniOSC{
	[AddComponentMenu("ECS/BaseOSC")]
	public class BaseOSC : UniOSCEventTarget {
		#region public
		public string address;
		#endregion
		// Use this for initialization
		void Awake(){
			
		}
		
		public override void OnEnable(){
			_Init();
			base.OnEnable();
		}
		
		protected void _Init(){
			
			//receiveAllAddresses = false;
			_oscAddresses.Clear();
			if(!_receiveAllAddresses){
				_oscAddresses.Add(address);
			}
		}
		protected bool isLive = true;
		public void AmILive(bool iLive)
		{
			isLive = iLive;

		}
		public bool POS;
		public override void OnOSCMessageReceived(UniOSCEventArgs args){

			//do nothing

		}
	}
}
