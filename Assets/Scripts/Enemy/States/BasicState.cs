using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BasicState : EnemyState
{
    private Rigidbody2D mRigidBody;
    [SerializeField] private Vector2 mDirection = new Vector2(1, 0);
    private float mLastTurn = 0;
    [SerializeField] private float mTimeBetweenTurns = 3;
    [SerializeField] private float mTimeToTurn = 2;

    [SerializeField] protected float mSpeed = 0.5f;

    public void Start()
    {
        Assert.AreNotEqual(gameObject, null);
        mRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public override EnemyState StateUpdate()
    {
        return null;
    }

    public override void StateFixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (mRigidBody == null) return;
        mRigidBody.velocity =  new Vector2( gameObject.transform.right.x, gameObject.transform.right.y) * mSpeed;

        if (Time.time - mLastTurn > mTimeBetweenTurns)
        {
            Debug.Log("Gira!");
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
