using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornerback_Controller_FSM : MonoBehaviour
{
    #region Player vars

    public Transform PlayerToCover;

    [HideInInspector]
    public Rigidbody2D myRb;

    public float speed;
    public float rotationSpeed;

    #endregion

    #region FSM

    public Cornerback_Base_State currentState;
    public readonly Cornerback_Cover_State cover_State = new Cornerback_Cover_State();
    public readonly Cornerback_Push_State push_State = new Cornerback_Push_State();

    #endregion

    void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        TransitionToState(push_State);
    }

    public void TransitionToState(Cornerback_Base_State state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void FixedUpdate() 
    {
        if(currentState != null)
        {
            currentState.Update(this);
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        currentState.OnCollisionEnter(this,other);    
    }
}
