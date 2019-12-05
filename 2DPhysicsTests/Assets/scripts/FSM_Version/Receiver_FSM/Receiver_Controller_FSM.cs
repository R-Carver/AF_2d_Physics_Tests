using System.Collections;
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

    #endregion

    #region FSM

    public Receiver_Base_State currentState;
    public readonly Receiver_RunRoute_State runRoute_State = new Receiver_RunRoute_State();

    #endregion

    void Awake()
    {   
        Receiver_Routes routes_script = new Receiver_Routes();
        myRb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //set the current Route depending on the chosen route Name
        currentRoute = Receiver_Routes.Instance.routes[currentRouteName];

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
        if(TargetReached())
        {
            currentTarget = (Vector2)this.transform.position + currentRoute.GetNextRoutePoint();
        }
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
