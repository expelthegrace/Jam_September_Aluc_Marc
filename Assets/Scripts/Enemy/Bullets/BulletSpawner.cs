﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner: MonoBehaviour
{
    //TODO pool of bullets

    public GameObject mBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBullet(Vector3 aPosition, Vector3 aDirection)
    {
        Instantiate(mBulletPrefab, aPosition, StaticFunctions.Get2DQuaternionFromDirection(aDirection));
    }
    public void SpawnBullet(Vector3 aPosition, Quaternion aRotation)
    {
        Instantiate(mBulletPrefab, aPosition, aRotation);
    }
}
