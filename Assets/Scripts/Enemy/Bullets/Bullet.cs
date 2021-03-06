﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletSpawner Spawner { get; set; }

    private const string ObstacleTag = "Obstacle";
    [SerializeField] private float mSpeed = 2.0f;
    protected float mSpeedIncrement = 0.0f;
    [SerializeField] private int mMinBouncesToDestroy = 3;
    [SerializeField] private int mMaxBouncesToDestroy = 10;
    protected int mBouncesToDestroy = 0;
    protected int mCurrentBounces = 0;

    // Start is called before the first frame update
    void Start()
    {
        mBouncesToDestroy = Random.Range(mMinBouncesToDestroy, mMaxBouncesToDestroy);
    }

    public void IncrementSpeed(float aIncrement)
    {
        mSpeedIncrement = aIncrement;
        mSpeed += aIncrement;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * mSpeed;
    }

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        Vector3 Normal = GetObstacleNormal(aCollision);
        if (Normal != Vector3.zero)
        {
            Vector3 Reflection = StaticFunctions.GetReflectionVector(transform.right, Normal);
            transform.rotation = StaticFunctions.Get2DQuaternionFromDirection(Reflection);
            UpdateBounces();
        }
    }

    protected void UpdateBounces()
    {
        ++mCurrentBounces;
        if (mCurrentBounces == mBouncesToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
    /**
     *  Gets the normal of the obstacle type game object from a collision. 
     *  Zero if no collision with an obstacle has occured.
     */
    protected Vector3 GetObstacleNormal(Collision2D aCollision)
    {
        Vector3 normal = Vector3.zero;
        if (aCollision.gameObject.CompareTag(ObstacleTag))
        {
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            if (aCollision.GetContacts(contacts) > 0)
            {
                normal = contacts[0].normal;
              
            }
        }
        return normal;
    }
}
