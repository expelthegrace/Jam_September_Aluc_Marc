﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner: MonoBehaviour
{
    //TODO pool of bullets

    public enum eBulletType
    {
        Normal,
        Dividable
    }

    [SerializeField] private GameObject mBulletPrefab = null;
    [SerializeField] private GameObject mDividableBulletPrefab = null;

    public GameObject Emitter { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBullet(eBulletType aBulletType, Vector3 aPosition, Vector3 aDirection)
    {
        SpawnBullet(aBulletType, aPosition, StaticFunctions.Get2DQuaternionFromDirection(aDirection));
    }

    public void SpawnBullet(eBulletType aBulletType, Vector3 aPosition, Quaternion aRotation)
    {
        GameObject bullet = Instantiate(GetBulletPrefabFromType(aBulletType), aPosition, aRotation);
        bullet.GetComponent<Bullet>().Spawner = this;
        IgnoreCollision(bullet);
    }

    private GameObject GetBulletPrefabFromType(eBulletType aBulletType)
    {
        switch (aBulletType)
        {
            case eBulletType.Normal : return mBulletPrefab;
            case eBulletType.Dividable : return mDividableBulletPrefab;
            default: return null;
        }
    }

    private void IgnoreCollision(GameObject aBulletObject)
    {
        Physics2D.IgnoreCollision(Emitter.GetComponent<Collider2D>(), aBulletObject.GetComponent<Collider2D>());
    }
}
