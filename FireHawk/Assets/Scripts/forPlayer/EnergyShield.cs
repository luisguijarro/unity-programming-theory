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
        this.totalLife /= (int)MainGameManager.Instance.Dificult;
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

    public int TotalLife
    {
        get { return this.totalLife; }
    }

    public int Damage
    {
        get { return this.damageOnShield; }
    }
}
