using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    protected float mDurationTime;
    protected int mStateID;
    public virtual void HandleStart()
    {
        mStateID = -1;
        mDurationTime = -1;
    }
    public virtual void HandleExit() { }

    //Update for the states. Use this instead of Update()
    public virtual EnemyState StateUpdate() { return null; }

    //Fixed Update for the states. Use this instead of FixedUpdate()
    public virtual void StateFixedUpdate() {}
   
}
