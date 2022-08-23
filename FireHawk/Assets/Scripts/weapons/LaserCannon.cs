using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class LaserCannon : weapon
{
    public LaserCannon() : base()
    {
        this.cadence = 1f; // One shoot for second.
    }

}
