using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GestureInterface : MonoBehaviour {

    public static readonly string RIGHT_CHOP = "rightChop";
    public static readonly string LEFT_TAP = "leftTap";
    public static readonly string DOWN_TAP = "downTap";
    public static readonly string TOGGLE = "toggle";
    public static readonly string SELECT = "select";
    public static readonly string BACK = "back";
    public static readonly string HOME = "home";
    public static readonly string SCROLLUP = "scrollUp";
    public static readonly string SCROLLDOWN = "scrollDown";

    abstract public void gesture(string instruction);
}
