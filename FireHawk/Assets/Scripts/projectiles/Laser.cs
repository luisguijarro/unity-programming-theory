using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : projectile
{

    // POLYMORPHISM
    protected override void OnStart()
    {
        base.OnStart();
        //this.damage = 1;
    }
}
