using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    public Rigidbody ball;
    public Transform target;

    public float h;
    public float gravity;

    public bool launched = false;

    Vector3 ballStartPosition;

    void Start(){

        Physics.gravity = Vector3.forward * gravity;

        ball.useGravity = false;

        ballStartPosition = ball.gameObject.transform.position;
        
    }

    void Update(){

        // keep track if the ball fell of the plane
        if(ball.position.z < 0)
        {

            //make the ball stop when hits the ground for debug
            ball.isKinematic = true;

            //Invoke("ResetLauncher",1);
            
        }
    }

    public void Launch(){
        
        ball.useGravity = true;
        //print("curr LaunchData   " + currentLaunchData.initialVelocity);
        ball.velocity = CalculateLaunchData().initialVelocity;
        ball.angularVelocity = Vector3.zero;
    }

    public LaunchData CalculateLaunchData(){

        float displacementY = target.position.z - ball.position.z;
        //Debug.Log("displ Y " + displacementY);
        
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, target.position.y - ball.position.y, 0);
        //Debug.Log("disp XZ " + displacementXZ);
        

        float time = (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity));
        
        Vector3 velocityY = Vector3.forward * Mathf.Sqrt(-2 * gravity * h);
        //Debug.Log("vel Y " + velocityY);
        
        Vector3 velocityXZ = displacementXZ / time;

        Vector3 outVector = new Vector3(velocityXZ.x, velocityXZ.y , velocityY.z);
        //Debug.Log(outVector);

        return new LaunchData(outVector, time);
        
    }


    public int TargetSpeed = 3;
    public void MoveTarget(float zValue)
    {
        // in the current version we only move the target along the route of the receiver
        // which is along the z axis
        Vector3 movement = new Vector3(0, 0, zValue);
        if(target.position.z < 3 && zValue >= 0)
        {
            target.Translate(movement * TargetSpeed *Time.deltaTime);
        }

        if(target.position.z > -4 && zValue < 0)
        {
            target.Translate(movement * TargetSpeed *Time.deltaTime);
        }
    }

    public void ResetLauncher()
    {
        ball.useGravity = false;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        launched = false;

        ball.gameObject.transform.position = ballStartPosition;
        ball.isKinematic = false;
        //target.position = new Vector3(3, 0, 0);
    }

    public struct LaunchData{
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
