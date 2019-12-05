using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver_RunRoute_State : Receiver_Base_State
{
    public override void CleanUp(Receiver_Controller_FSM receiver)
    {
        
    }

    public override void EnterState(Receiver_Controller_FSM receiver)
    {
        
    }

    public override void OnCollisionEnter(Receiver_Controller_FSM receiver)
    {
        
    }

    public override void Update(Receiver_Controller_FSM receiver)
    {
        Vector2 localForward = receiver.transform.rotation * Vector2.right;
        receiver.myRb.AddForce(localForward * receiver.speed * Time.deltaTime);

        float angle = Vector2.SignedAngle(receiver.currentTarget - (Vector2)receiver.transform.position, new Vector2(1.0f, 0.0f));
        receiver.myRb.MoveRotation(Mathf.LerpAngle(receiver.myRb.rotation, -angle, receiver.rotationSpeed * Time.deltaTime));
    }
}
