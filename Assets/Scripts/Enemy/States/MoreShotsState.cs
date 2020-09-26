using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreShotsState : BasicState
{

    public override void HandleStart()
    {
        mDurationTime = 12f;
        nameState = "More Shots!!";
        mStateID = 2;
        mBulletType = BulletSpawner.eBulletType.Normal;


        mTimeBetweenShooting = 2.5f;
        mBulletsPerShooting = 5;
        mNumberOfExtraCanons = 8;

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
