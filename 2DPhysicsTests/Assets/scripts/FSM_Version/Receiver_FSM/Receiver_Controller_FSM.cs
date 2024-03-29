﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver_Controller_FSM : MonoBehaviour
{
    #region Player vars

    public RouteName currentRouteName;
    public Route currentRoute;
    //public Transform currentTarget;
    public Vector2 currentTarget;

    [HideInInspector]
    public Rigidbody2D myRb;
    public float speed;
    public float rotationSpeed;

    //for the editor
    public Vector3 startPos;

    #endregion

    #region FSM

    public Receiver_Base_State currentState;
    public readonly Receiver_RunRoute_State runRoute_State = new Receiver_RunRoute_State();
    public readonly Receiver_RunFinished_State runFinished_State = new Receiver_RunFinished_State();

    #endregion

    void Awake()
    {   
        Receiver_Routes routes_script = new Receiver_Routes();
        myRb = GetComponent<Rigidbody2D>();

        startPos = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //set the current Route depending on the chosen route Name
        //we use the one from the routes dict as template and create a new one so its not shared
        currentRoute = new Route(currentRouteName, Receiver_Routes.Instance.routes[currentRouteName].routePoints);

        //set the first Target
        //currentTarget = this.transform.position;
        currentTarget = (Vector2)this.transform.position + currentRoute.GetFirstRoutePoint();

        TransitionToState(runRoute_State);
    }

    public void TransitionToState(Receiver_Base_State state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(TargetReached())
        {
            //currentTarget = (Vector2)this.transform.position + currentRoute.GetNextRoutePoint();
            currentTarget = currentTarget + currentRoute.GetNextRoutePoint();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //TODO: change the state implementation later to OnTriggerEnter instead of OnCollisionEnter
        currentState.OnCollisionEnter(this, other);
    }

    void FixedUpdate() 
    {   
        if(currentState != null)
        {
            currentState.Update(this);
        }   
    }

    public bool TargetReached()
    {
        if((currentTarget - (Vector2)this.transform.position).magnitude < 0.25)
        {
            return true;
        }else
        {
            return false;
        }
    }

    
}
