using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    public WheelCollider wc;
    public float degree = 0;//转动速度
    

    // Update is called once per frame
    void Update()
    {
        this.degree = (this.wc.rpm * 360 / 60) * Time.deltaTime;
        this.transform.rotation = this.wc.transform.rotation * Quaternion.Euler(this.degree, this.wc.steerAngle,0);
    }
}
