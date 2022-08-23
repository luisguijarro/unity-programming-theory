using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class weapon : ProjectileEmitter 
{
    [SerializeField] protected float aimSpeed = 0f; // not all Weapons can Aim.
    [SerializeField] protected float cadence = 1f; // One per second by default
    [SerializeField] protected bool automaticFire = true;
    [SerializeField] protected KeyCode KeyConfig = KeyCode.None; // ninguna por defecto.
    private float cadenceTime;

    // POLYMORPHISM
    protected override void OnStart()
    {
        base.OnStart();
    }

    // POLYMORPHISM
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (this.gameManager.OnGame && !this.gameManager.OnPause)
        {
            if (this.automaticFire ? Input.GetKey(KeyConfig) : Input.GetKeyDown(KeyConfig))
            {
                this.Shoot(this.gameObject.tag);       
            }
            this.cadenceTime += Time.deltaTime;
        }
    }

    // POLYMORPHISM
    public override void Shoot(string goEmmiterTag)
    {
        if (this.cadenceTime >= 1f/this.cadence)
        {
            base.Shoot(goEmmiterTag);
            this.cadenceTime = 0f;
        }
    }

    // ENCAPSULATION
    public float Damage
    {
        get { return this.projectileType.GetComponent<projectile>().Damage; }
    }

    // ENCAPSULATION
    public float Cadence 
    { 
        get { return this.cadence; } 
    }
}