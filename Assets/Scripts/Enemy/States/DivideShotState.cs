using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideShotState : BasicState
{
    public override void HandleStart()
    {
        Debug.Log("DivideState");
        mDurationTime = 5f;
        mStateID = 1;
        base.BasicStart();
    }

    public override EnemyState StateUpdate()
    {
        mTimeStateActive = Time.time - mTimeStateActivated;

        if (mTimeStateActive > mDurationTime)
        {
            Debug.Log("Change State");
            return GetRandomState();
        }

        return null;
    }

    public override void StateFixedUpdate()
    {
        base.Move();
        base.ShootingManager();      
    }

}
