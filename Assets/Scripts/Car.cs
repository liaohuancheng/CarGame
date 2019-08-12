using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public Transform wheelFR;
    public Transform wheelFL;
    public Transform wheelRR;
    public Transform wheelRL;

    public WheelCollider wheelColliderFR;
    public WheelCollider wheelColliderFL;
    public WheelCollider wheelColliderRR;
    public WheelCollider wheelColliderRL;

    public Transform centerOfMass;

    public float motorTorque;
    public float steeringAngle;

    public float maxSpeed = 200;
    public float minSpeed = 30;
    private float currentSpeed;

    public float brakeTorque ;
    private Rigidbody rigibody;

    // Start is called before the first frame update
    void Start()
    {
        initProperty();
    }

    private void initProperty()
    {
        wheelColliderFL = transform.Find("WheelsHubs/WheelHubFrontLeft").GetComponent<WheelCollider>();
        wheelColliderFR = transform.Find("WheelsHubs/WheelHubFrontRight").GetComponent<WheelCollider>();
        wheelColliderRR  = transform.Find("WheelsHubs/WheelHubRearRight").GetComponent<WheelCollider>();
        wheelColliderRL  = transform.Find("WheelsHubs/WheelHubRearLeft").GetComponent<WheelCollider>();

        wheelFL = transform.Find("model/Car02WheelFrontLeft");
        wheelFR = transform.Find("model/Car02WheelFrontRight");
        wheelRR = transform.Find("model/Car02WheelRearRight");
        wheelRL = transform.Find("model/Car02WheelRearLeft");

        rigibody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        bool isBrake = false;

        currentSpeed = wheelColliderFL.rpm * (wheelColliderFL.radius * 2 * Mathf.PI) * 60 / 1000;
        currentSpeed = rigibody.velocity.magnitude;
        //Debug.Log(wheelColliderFL.motorTorque);
        //Debug.Log(currentSpeed);
        if ((currentSpeed > maxSpeed && Input.GetAxis("Vertical") > 0) || (currentSpeed < -minSpeed && Input.GetAxis("Vertical") < 0))
        {
            //Debug.Log("置0");
            wheelColliderFL.motorTorque = 0;
            wheelColliderFR.motorTorque = 0;
            wheelColliderRL.motorTorque = 0;
            wheelColliderRR.motorTorque = 0;
        }
        else
        {

            wheelColliderFL.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliderFR.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliderRR.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliderRL.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            isBrake = true;
        }
        else
        {
            isBrake = false;
        }

        //Debug.Log(isBrake);

        if (isBrake)
        {
            

            wheelColliderRR.brakeTorque = brakeTorque;
            wheelColliderRL.brakeTorque = brakeTorque;
        }
        else
        {
            wheelColliderRR.brakeTorque = 0;
            wheelColliderRL.brakeTorque = 0;
        }
        wheelColliderFL.steerAngle = Input.GetAxis("Horizontal") * steeringAngle;
        wheelColliderFR.steerAngle = Input.GetAxis("Horizontal") * steeringAngle;
        

        RotateWheels();
        SteerWheels();
    }

    private void FixedUpdate()
    {
        Skid();
    }

    private void Skid()
    {
        
    }

    private void SteerWheels()
    {
        Vector3 localEulerAngles = wheelFL.localEulerAngles;
        localEulerAngles.y = wheelColliderFL.steerAngle;

        wheelFL.localEulerAngles = localEulerAngles;
        wheelFR.localEulerAngles = localEulerAngles;
    }

    private void RotateWheels()
    {
        wheelFL.Rotate(wheelColliderFL.rpm * 360 / 60 * Time.deltaTime * Vector3.right);
        wheelFR.Rotate(wheelColliderFR.rpm * 360 / 60 * Time.deltaTime * Vector3.right);
        wheelRR.Rotate(wheelColliderRR.rpm * 360 / 60 * Time.deltaTime * Vector3.right);
        wheelRL.Rotate(wheelColliderRL.rpm * 360 / 60 * Time.deltaTime * Vector3.right);
    }
}
