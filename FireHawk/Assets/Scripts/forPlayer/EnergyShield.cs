using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    [SerializeField] private int totalLife;
    private int damageOnShield;

    public event EventHandler<IntValueChangeEventArgs> ValueChange;
    // Start is called before the first frame update
    void Start()
    {
        this.ValueChange += delegate{};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(int damage)
    {
        this.damageOnShield += damage;
        this.ValueChange(this, new IntValueChangeEventArgs(0, this.totalLife, this.damageOnShield));
        if (this.damageOnShield >= this.totalLife)
        {
            //this.explosion.Play(); // Play Explosion
            Destroy(this.gameObject);
        }
    }
/*
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            projectile p0 = other.gameObject.GetComponent<projectile>();
            if (!p0.isDestroyed && p0.IsEnergy)
            {
                if (p0.emmiterTag != this.gameObject.tag)
                {
                    if (this.damageOnShield >= this.totalLife)
                    {
                        //this.explosion.Play(); // Play Explosion
                        Destroy(this.gameObject);
                    }  
                    else
                    {
                        this.SetDamage(p0.Damage);
                        Debug.Log("Damage on EnergyShield: " + this.damageOnShield + "/" + this.totalLife);
                    }
                    //Debug.Log("Collision with projectile emmited by " + p0.emmiterTag + " on Energy Shield.");
                } 
                Debug.Log("Collision with projectile emmited by " + p0.emmiterTag + " on Energy Shield.");
            }
            //Debug.Log("Collision with projectile emmited by " + p0.emmiterTag);             
        }
    }*/

    public int TotalLife
    {
        get { return this.totalLife; }
    }

    public int Damage
    {
        get { return this.damageOnShield; }
    }
}
