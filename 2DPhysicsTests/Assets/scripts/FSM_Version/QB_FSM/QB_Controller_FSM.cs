using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QB_Controller_FSM : MonoBehaviour
{
    #region Player vars

    public Transform ThrowTarget;

    public GameObject Receiver_current;
    public GameObject[] Receivers;

    public Transform prefab_TargetIndicator;

    public float targetOffset;

    bool isSacked = false;

    #endregion

    #region FSM

    public QB_Base_State currentState;

    public readonly QB_ChooseTarget_State chooserTarget_State = new QB_ChooseTarget_State();
    public readonly QB_Throw_State throw_State = new QB_Throw_State();
    public readonly QB_Sacked_State sacked_State = new QB_Sacked_State();

    #endregion
    
    void Awake()
    {
        GameObject receiversObj = GameObject.Find("Receivers");
        Receivers = new GameObject[receiversObj.transform.childCount];

        int childIndex = 0;
        foreach(Transform receiver in receiversObj.transform)
        {
            Receivers[childIndex++] = receiver.gameObject;
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {   
        //Change this later
        TransitionToState(chooserTarget_State);    
    }

    public void TransitionToState(QB_Base_State state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void Update() 
    {
        if(currentState != null)
        {
            currentState.Update(this);
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.gameObject.tag == "PassRusher")
        {   
            if(isSacked == false)
            {   
                isSacked = true;
                TransitionToState(sacked_State);
            }
        }
        
    }

    public Transform InstantiateTargetIndicator(Vector3 position)
    {
        return Instantiate(prefab_TargetIndicator, position, Quaternion.identity);
    }

    
}
