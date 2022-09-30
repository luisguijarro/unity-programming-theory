using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public abstract class projectile : SpaceObject
{
    [SerializeField] protected int damage = 1; // By Default
    private ParticleSystem explosion;
    [SerializeField] internal string emmiterTag;
    [SerializeField] private bool isEnergy;

    // POLYMORPHISM
    protected override void OnStart()
    {
        base.OnStart();
        this.explosion = this.GetComponent<ParticleSystem>();
    }

    // POLYMORPHISM
    protected override void Move()
    {
        if (!this.isDestroyed)
        {
            base.Move();
            if ((this.transform.position.z > 940) || (this.transform.position.z < -10))
            {
                Destroy(this.gameObject);
            }
        }        
    }

    // ENCAPSULATION
    public int Damage 
    { 
        get 
        { 
            return this.damage;
        } 
    }

    // ENCAPSULATION
    public bool IsEnergy
    {
        get { return this.isEnergy; }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.tag.Contains(this.emmiterTag))
        {
            if (other.gameObject.tag.Contains("Shield"))
            {
                other.gameObject.GetComponent<EnergyShield>().SetDamage(this.damage);
            }
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<FireHawk>().SetDamageOnHull(this.damage);
            }
            this.destroyed = true;
            this.GetComponent<BoxCollider>().enabled = false;
            this.explosion.Play();
            Destroy(this.gameObject, 0.2f);
        }   
            
    }
}
