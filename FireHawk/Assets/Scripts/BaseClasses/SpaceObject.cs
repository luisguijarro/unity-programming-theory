using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] protected float speed = 0f; // don't move by default.
    [SerializeField] protected float aimsPerSecond = 0; // Can't aims by default.
    [SerializeField] protected float aimErrorRange = 0; // 0 is perfect aim.
    [SerializeField] protected float maneuveringSpeed = 0f; // Can't maneuver by default.
    [SerializeField] protected float inferiorLimitAimforZ = 40f;
    [SerializeField] protected float inferiorLimitforZ = -20f;
    [SerializeField] protected GameObject goTarget;
    [SerializeField] protected Vector3 v3Target;
    [SerializeField] protected float detectionRadio;
    [SerializeField] protected List<ProjectileEmitter> ProjectileEmitters = new List<ProjectileEmitter>();
    protected GameManager gameManager;
    protected bool destroyed = false;
    protected float aimTime = 0f;
    protected float aimDiffAngle;
    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = GameManager.Instance;
        this.OnStart();
    }

    // Update is called once per frame
    void Update()
    {        
        if (this.gameManager.OnGame && !this.gameManager.OnPause && !this.destroyed)
        {
            if (this.transform.position.z < inferiorLimitforZ)
            {
                Destroy(this.gameObject);
            }
            this.AllActions();
        }
        this.OnUpdate();
    }

    #region PRIVATED METHODS:

    private void AllActions()
    {
        if (GameManager.Instance.OnGame && !GameManager.Instance.OnPause)
        {            
            this.Move();
            if (goTarget != null)
            {
                this.Aim(goTarget);
            }
            else
            {
                this.Aim(v3Target);
            }

            if (this.goTarget != null)
            {
                if (Vector3.Magnitude(this.goTarget.transform.position-this.transform.position) < this.detectionRadio)
                {
                    this.AimProjectileEmitters(this.goTarget);
                }
            }        
        }
    }

    #endregion

    #region PROTECTED METHODS: // ENCAPSULATION

    protected virtual void OnStart()
    {
        // Nothing By Default.
    }

    protected virtual void OnUpdate()
    {
        // Nothing By Default.
    }

    // ABSTRACTION
    protected virtual void Move()
    {        
        this.transform.Translate(this.transform.forward * speed * Time.deltaTime, Space.World);
    }

    // ABSTRACTION
    protected void Aim (GameObject target)
    {
        this.Aim(target.transform.position);
    }

    // ABSTRACTION
    protected void Aim (Vector3 targetPosition)
    {
        if (aimsPerSecond <= 0)
        {
            return; // Can't Aims.
        }

        if (this.aimTime >= 1f/(float)aimsPerSecond) 
        {
            if (this.transform.position.z > 100)
            {
                Vector3 v3_1 = this.transform.forward;
                Vector3 V3_direction = (targetPosition - this.transform.position);
                aimDiffAngle = Vector3.SignedAngle(v3_1, V3_direction, Vector3.up);
                aimDiffAngle += this.aimErrorRange <= 0 ? this.aimErrorRange : (Random.Range(0f, this.aimErrorRange) * aimDiffAngle);
                this.transform.Rotate(Vector3.up * aimDiffAngle *(Time.deltaTime * this.maneuveringSpeed), Space.World);
            }
            this.aimTime = 0f;
        }
        this.aimTime += Time.deltaTime;
    }

    // ABSTRACTION
    protected virtual void AimProjectileEmitters (GameObject target)
    {
        if (this.transform.position.z > 20)
        {
            foreach(ProjectileEmitter pe in this.ProjectileEmitters)
            {
                pe.Aim(target);
            }
        }
    }

    // ABSTRACTION
    protected virtual void ShootProjectileEmmiters()
    {
        foreach (ProjectileEmitter pe in this.ProjectileEmitters)
        {
            if (pe != null) { pe.Shoot(this.gameObject.tag); } // if is !null -> to avoid errors in coroutines
        }
    }

    protected virtual void DestroyThis(bool withPoints)
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.destroyed = true;
    }

    #endregion

    #region PROPERTIES:

    // ENCAPSULATION
    public bool isDestroyed
    {
        get { return this.destroyed; }
    }

    // ENCAPSULATION
    public float Speed
    {
        get { return this.speed; }
    }

    #endregion
}
