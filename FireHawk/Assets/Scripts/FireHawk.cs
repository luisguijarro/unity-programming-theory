using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHawk : MonoBehaviour
{
    [SerializeField] private float angleRotZ;
    [SerializeField] private readonly float rotSpeed = 10f;
    [SerializeField] private readonly float maxAngle = 45;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horMov = Input.GetAxis("Horizontal");

        // Translate
        this.transform.Translate(Vector3.right * horMov, Space.World);
        
        if (this.transform.position.x > 44)
        {
            this.transform.position = new Vector3(44, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < -44)
        {
            this.transform.position = new Vector3(-44, this.transform.position.y, this.transform.position.z);
        }

        // Rotate Efect...

        this.angleRotZ += horMov * (this.rotSpeed / 30f);

        if (angleRotZ > 1)
        {
            angleRotZ -= (rotSpeed ) * Time.deltaTime;
        }
        else if (angleRotZ < -1)
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
}
