﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideShotState : BasicState
{
    public override void HandleStart()
    {
        Debug.Log("DivideState");
        base.BasicStart();
    }

    public override EnemyState StateUpdate()
    {
        mTimeStateActive = Time.time - mTimeStateActivated;

        //if (mTimeStateActive > 3) return gameObject.GetComponent<BasicState>();

        return null;
    }

    public override void StateFixedUpdate()
    {
        base.Move();
        //New Shot();      
    }

}
