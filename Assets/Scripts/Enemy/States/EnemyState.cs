using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float mDurationTime;
    protected int mStateID;

    public string nameState;
    public virtual void HandleStart()
    {
        mStateID = -1;
        mDurationTime = -1;
        gameObject.transform.position = new Vector3(0, 0, 0);
    }
    public virtual void HandleExit() { }

    //Update for the states. Use this instead of Update()
    public virtual EnemyState StateUpdate() { return null; }

    //Fixed Update for the states. Use this instead of FixedUpdate()
    public virtual void StateFixedUpdate() {}

    public static UnityEvent mOnEnemyStateChanged = new UnityEvent();
   

}
