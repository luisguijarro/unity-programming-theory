using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHawk : MonoBehaviour
{
    [SerializeField] private float angleRotZ;
    [SerializeField] private float rotSpeed = 360;
    [SerializeField] private float maxAngle = 90;
    [SerializeField] private float rangeX;
    //[SerializeField] private int energyShield; // Life of Energy Field of the spaceship.
    [SerializeField] private int spaceshipHull; // Life of Hull of the spaceship.
    [SerializeField] private ProgressBar hullProgressBar;
    [SerializeField] private ProgressBar shieldProgressBar;
    [SerializeField] private GameObject goEnergyShield;
    private EnergyShield energyShield;
    private int damageOnHull = 0;
    private ParticleSystem explosion;

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = GameManager.Instance;
        this.explosion = this.GetComponentInChildren<ParticleSystem>();  

        this.SetHull(this.spaceshipHull/(int)MainGameManager.Instance.Dificult);
        this.SetDamageOnHull(0); // update LifeBar on UI.

        this.SetEnergyShield(Instantiate<GameObject>(goEnergyShield, this.transform));
        
    }

    // Update is called once per frame
    void Update()
    {
        float horMov = Input.GetAxis("Horizontal");

        // Translate
        float trasnlateMult = horMov * (this.rotSpeed/2f) * Time.deltaTime;
        this.transform.Translate(Vector3.right * trasnlateMult, Space.World);
        
        if (this.transform.position.x > rangeX)
        {
            this.transform.position = new Vector3(rangeX, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < -rangeX)
        {
            this.transform.position = new Vector3(-rangeX, this.transform.position.y, this.transform.position.z);
        }

        // Rotate Efect...

        this.angleRotZ += horMov * (this.rotSpeed / 30f);


        if (angleRotZ > 5)
        {
            angleRotZ -= (rotSpeed ) * Time.deltaTime;
        }
        else if (angleRotZ < -5)
        {
            angleRotZ += (rotSpeed ) * Time.deltaTime;
        }
        else
        {
            angleRotZ = 0;
        }


        if (angleRotZ > maxAngle)
        {
            angleRotZ = maxAngle;
        }

        if (angleRotZ < -maxAngle)
        {
            angleRotZ = -maxAngle;
        }

        this.transform.rotation = Quaternion.AngleAxis(((int)-angleRotZ), Vector3.forward);
    }

    #region Damage

    private void SetDamageOnEnergyShield()
    {
        //this.damageOnShield += damage;
        this.shieldProgressBar.Value = this.energyShield.TotalLife-this.energyShield.Damage;
    }

    private void DamOnShield( object sender, IntValueChangeEventArgs e)
    {
        SetDamageOnEnergyShield();
    }

    public void SetDamageOnHull(int damage)
    {
        this.damageOnHull += damage;
        this.hullProgressBar.Value = this.spaceshipHull-this.damageOnHull;
        if (this.damageOnHull >= this.spaceshipHull)
        {
            this.explosion.Play(); // Play Explosion
            Destroy(this.gameObject, 0.3f); // Destroy with Delay to not destroy Explosion
            this.gameManager.GameOver();
        }  
    }

    #endregion

    #region Set Equipment

    public void SetHull(int totalLife)
    {
        /* In the future will exit a diferents Hull Qualities, but 
           by the moment only we will define the points of life. */
        this.spaceshipHull = totalLife;
        this.hullProgressBar.SetMaxValue(this.spaceshipHull);
    }

    private void SetEnergyShield(EnergyShield energy_shield)
    {
        this.energyShield = energy_shield;
        this.shieldProgressBar.SetMaxValue(this.energyShield.TotalLife);
        this.SetDamageOnEnergyShield();
        this.energyShield.ValueChange += DamOnShield;
    }

    public void SetEnergyShield(GameObject go_energy_shield)
    {
        this.goEnergyShield = go_energy_shield;
        this.goEnergyShield.tag = "PlayerShield";
        SetEnergyShield(this.goEnergyShield.GetComponent<EnergyShield>());
    }

    #endregion

    void OnCollisionEnter(Collision other)
    {/*
        if (other.gameObject.CompareTag("Projectile"))
        {
            projectile p0 = other.gameObject.GetComponent<projectile>();
            if (!p0.isDestroyed)
            {
                if (p0.emmiterTag != this.gameObject.tag)
                {
                    if (this.damageOnHull >= this.spaceshipHull)
                    {
                        this.explosion.Play(); // Play Explosion
                        Destroy(this.gameObject, 0.3f); // Destroy with Delay to not destroy Explosion
                        this.gameManager.GameOver();
                    }  
                    else
                    {
                        this.SetDamageOnHull(p0.Damage);
                        Debug.Log("Damage on Field: " + this.damageOnHull + "/" + this.spaceshipHull);
                    }
                } 
            }
            //Debug.Log("Collision with projectile emmited by " + p0.emmiterTag);             
        }
        else */if (other.gameObject.CompareTag("Enemy"))
        {
            this.damageOnHull += (int)(other.gameObject.GetComponent<SpaceShip>().Speed/10f);
        }
    }
}
