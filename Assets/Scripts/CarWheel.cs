using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarWheel : MonoBehaviour{
    public WheelCollider targetWheel;
    public Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();


    private void Update(){
        targetWheel.GetWorldPose(out wheelPosition, out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
    }
}
