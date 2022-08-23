using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class ProjectileEmitter : SpaceObject
{    
    [SerializeField] protected GameObject projectileType; // Projectile that can shoot.
    [SerializeField] protected int projectilesPerShoot = 1;
    [SerializeField] protected Vector3 projectileOffSet;

    protected override void OnStart()
    {
        //We avoid non-existent AudioSource references
        //base.OnStart();
    }
    
    // ABSTRACTION
    public virtual void Shoot()
    {
        this.Shoot("");
    }
    
    // ABSTRACTION
    public virtual void Shoot(string goEmmiterTag)
    {
        for (int i=0;i<projectilesPerShoot;i++)
        {
            // Shooting in same direction by Default.
            Vector3 v3Pos = this.transform.position + (Vector3.Distance(Vector3.zero, projectileOffSet) * this.transform.forward);
            GameObject prjtl = Instantiate(this.projectileType, /*this.transform.position + projectileOffSet*/ v3Pos, Quaternion.LookRotation(this.transform.forward, this.transform.up));
            prjtl.GetComponent<projectile>().emmiterTag = goEmmiterTag;
        }
    }
}
