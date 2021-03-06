﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideShotState : BasicState
{
    public override void HandleStart()
    {
        mDurationTime = 8f;
        nameState = "Dividing Shots";
        mStateID = 1;
        mBulletType = BulletSpawner.eBulletType.Dividable;
        base.BasicStart();
    }

    public override EnemyState StateUpdate()
    {
        mTimeStateActive = Time.time - mTimeStateActivated;

        if (mTimeStateActive > mDurationTime)
        {
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
