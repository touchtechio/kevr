#pragma strict

/*
*This is only a demo class to show how you can send data from c# to a js class.
*Keep in mind to locate all your js classes in the Plugins folder for the right compilation order.(As we call js from c# the js classes need to be compiled first)
* http://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html
*/
function Start () {

}

function Update () {

}

function OnOSCMessageReceived(){
	Debug.Log("JS.OnOSCMessageReceived");
}

function OnOSCBooleanReceived(data:boolean ){
	Debug.Log("JS.OnOSCBooleanReceived:"+data);
}


function OnOSCFloatReceived(data:float){
	Debug.Log("JS.OnOSCFloatReceived:"+data);
}

function OnOSCStringReceived(data:String){
	Debug.Log("JS.OnOSCStringReceived:"+data);
}
