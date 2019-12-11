using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornerback_Push_State : Cornerback_Base_State
{
    public override void CleanUp(Cornerback_Controller_FSM cornerback)
    {
        
    }

    public override void EnterState(Cornerback_Controller_FSM cornerback)
    {
        
    }

    public override void OnCollisionEnter(Cornerback_Controller_FSM cornerback, Collision2D other)
    {
       if(other.gameObject.tag == "Receiver")
       {
           cornerback.StartCoroutine(Release(cornerback));
       }
    }

    public override void Update(Cornerback_Controller_FSM cornerback)
    {
        Vector2 localRight = cornerback.transform.rotation * Vector2.right;
        cornerback.myRb.AddForce(localRight * cornerback.speed * Time.deltaTime);

        float angle = Vector2.SignedAngle(cornerback.PlayerToCover.position - cornerback.transform.position, new Vector2(1.0f, 0.0f));
        cornerback.myRb.MoveRotation(Mathf.LerpAngle(cornerback.myRb.rotation, -angle, cornerback.rotationSpeed * Time.deltaTime));
    }

    IEnumerator Release(Cornerback_Controller_FSM cornerback)
    {
        yield return new WaitForSeconds(1);
        cornerback.transform.GetComponent<BoxCollider2D>().enabled = false;
    }
}
