using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BasicState : EnemyState
{
    private Rigidbody2D mRigidBody;

    private float mLastTurn;
    [SerializeField] private float mTimeBetweenTurns = 2;
    [SerializeField] private float mTimeToTurn = 1;
    [SerializeField] protected float mSpeed = 0.55f;

    protected BulletSpawner mSpawner;

    protected float mTimeStateActive;
    protected float mTimeStateActivated;

    public override void HandleStart()
    {
        Debug.Log("BasicState");
        BasicStart();
      
    }

    protected void BasicStart()
    {
        mSpawner = (BulletSpawner)FindObjectOfType(typeof(BulletSpawner));

        mLastTurn = Time.time;
        mTimeStateActivated = Time.time;
        Assert.AreNotEqual(gameObject, null);
        mRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mSpawner.SpawnBullet(transform.position, transform.rotation);//TODO from nose
        }
    }

    public override EnemyState StateUpdate()
    {
        mTimeStateActive = Time.time - mTimeStateActivated;

        if (mTimeStateActive > 3) return gameObject.GetComponent<DivideShotState>();

        return null;
    }

    public override void StateFixedUpdate()
    {
        Move();
        //Shoot();
    }

    protected void Move()
    {
        if (mRigidBody == null) return;
        mRigidBody.velocity =  new Vector2( gameObject.transform.right.x, gameObject.transform.right.y) * mSpeed;

        if (Time.time - mLastTurn > mTimeBetweenTurns)
        {
            mLastTurn = Time.time;
            StartCoroutine(Turn(Random.Range(-100,100), mTimeToTurn));
            
        }
    }

    private IEnumerator Turn(float aAngle, float aTime)
    {
        float startTime = Time.time;
        float startAngle = mRigidBody.rotation;

        while (Time.time < startTime + aTime)
        {
            float newRotation = Mathf.LerpAngle(startAngle, startAngle + aAngle, (Time.time - startTime) / aTime);
            mRigidBody.SetRotation(newRotation);
     
            yield return null;
        }             
    }
}
