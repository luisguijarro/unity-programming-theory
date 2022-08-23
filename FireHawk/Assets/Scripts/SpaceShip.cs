using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class SpaceShip : SpaceObject
{    
    //protected List<weapon> weapons = new List<weapon>();
    [SerializeField] protected int baseScorePoints = 1;
    [SerializeField] protected int fieldLife = 2;
    private int damageOnField = 0;
    private ParticleSystem explosion;

    
    #region Protected Methods:

    // POLYMORPHISM
    protected override void OnStart()
    {
        base.OnStart();
        this.explosion = this.GetComponentInChildren<ParticleSystem>();
        this.goTarget = GameObject.Find("FireHawk");
        StartCoroutine(cShoot());
    }

    private IEnumerator cShoot()
    {
        while(!this.destroyed)
        {
            yield return new WaitForSeconds(1f/this.aimsPerSecond);
            this.ShootCoroutine();
        }        
    }

    // ABSTRACTION
    protected virtual void ShootCoroutine()
    {
        if ((int)this.aimDiffAngle == 0)
        {
            this.ShootProjectileEmmiters();
            Debug.Log("Apuntado correcto.");
        }
        Debug.Log("Eshoot");
    }

    // ABSTRACTION
    protected override void DestroyThis(bool withPoints)
    {
        //this.GetComponent<MeshRenderer>().enabled = false;
        this.explosion.Play(); // Play Explosion
        this.audioSource.Play();
        if (!this.destroyed && withPoints)  // Prevenning multiscore.
        { 
            this.gameManager.IncrementScore(this.baseScorePoints); 
        }
        
        base.DestroyThis(withPoints); //this.destroyed = true;
        Destroy(this.gameObject, 0.3f); // Destroy with Delay to not destroy Explosion
    }

    #endregion

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            projectile p0 = other.gameObject.GetComponent<projectile>();
            if (p0.emmiterTag != this.gameObject.tag)
            {
                if (!p0.isDestroyed) { this.damageOnField += p0.Damage; } // Preven multi impact when projectile is destroyed
                if (this.damageOnField >= this.fieldLife)
                {
                    this.DestroyThis(true);
                }  
            }                      
        }
        else if (other.gameObject.CompareTag("Player")) // Dead by crash not return scorepoints.
        {
            this.DestroyThis(false);
        }
    }
}
