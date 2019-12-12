using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QB_Sacked_State : QB_Base_State
{
    public override void CleanUp(QB_Controller_FSM qb)
    {
        
    }

    public override void EnterState(QB_Controller_FSM qb)
    {
        GameObject normal_img = qb.transform.Find("Img_normal").gameObject;
        normal_img.SetActive(false);

        GameObject sacked_img = qb.transform.Find("Img_sacked").gameObject;
        sacked_img.SetActive(true);
    }

    public override void OnCollisionEnter(QB_Controller_FSM qb)
    {
        
    }

    public override void Update(QB_Controller_FSM qb)
    {
        
    }
}
