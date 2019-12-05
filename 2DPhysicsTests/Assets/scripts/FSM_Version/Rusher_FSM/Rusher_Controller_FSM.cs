using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rusher_Controller_FSM : MonoBehaviour
{
    #region Player vars

    public Transform Qb;

    //this is the current Target
    public Transform Target;
    [HideInInspector]
    public Rigidbody2D myRb;

    public float speed;
    public float rotationSpeed;

    public DefenderController_FSM defender;

    #endregion

    #region FSM
    public Rush_Base_State currentState;

    public readonly Rush_OpenField_State openField_State = new Rush_OpenField_State();
    public readonly Rush_OppVisible_State oppVisible_State = new Rush_OppVisible_State();
    public readonly Rush_QbChase_State qbChase_State = new Rush_QbChase_State();

    #endregion

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(openField_State);
    }

    // Update is called once per frame
    void Update()
    {
        //this it probably not a good idea since it makes this object depend on the defender
        //but for now its ok
        if(defender.currentState is Def_KnockedOver_State)
        {
            TransitionToState(qbChase_State);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {   
        //TODO: change the state implementation later to OnTriggerEnter instead of OnCollisionEnter
        currentState.OnCollisionEnter(this, other);
    }

    public void TransitionToState(Rush_Base_State state)
    {
        state.CleanUp(this);
        currentState = state;
        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        currentState.Update(this);
    }
}
