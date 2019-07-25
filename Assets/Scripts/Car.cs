using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public WheelCollider fl;
    public WheelCollider fr;

    private float max_torque = 2000;
    private float max_degree = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0&& Input.GetAxis("Vertical")==0)
        {
            return;
        }
        this.fl.motorTorque = this.max_torque * Input.GetAxis("Vertical");
        this.fr.motorTorque = this.max_torque * Input.GetAxis("Vertical");

        this.fl.steerAngle = this.max_degree * Input.GetAxis("Horizontal");
        this.fr.steerAngle = this.max_degree * Input.GetAxis("Horizontal");
    }
}
