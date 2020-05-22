using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState
{
    public static WorldState instance { get; private set; }

    public enum weatherStates {
        sunny = 1,
        rainy = 2
    }

    public double timeOfDay = 0.0f; // Number of seconds starting at midnight(0.0f) to 11:59:59 (86399)

}
