using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DividableBullet : Bullet
{

    [SerializeField] private float mSameVectorAngle = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        mBouncesToDestroy = 1;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D aCollision)
    {
        Vector3 Normal = GetObstacleNormal(aCollision);
        if (Normal != Vector3.zero)
        {
            Assert.AreNotEqual(Spawner, null);

            BulletSpawner.eBulletType newBulletType = BulletSpawner.eBulletType.Normal;
            if (StaticFunctions.AreAlmostSameVector(-transform.right, Normal, mSameVectorAngle))
            {
                Spawner.SpawnBullet(newBulletType, transform.position, StaticFunctions.Rotate2DVector3(Normal, -45.0f));
                Spawner.SpawnBullet(newBulletType, transform.position, StaticFunctions.Rotate2DVector3(Normal, 45.0f));
            }
            else
            {
                Vector3 Reflection = StaticFunctions.GetReflectionVector(transform.right, Normal);
                Spawner.SpawnBullet(newBulletType, transform.position, Reflection);
                Spawner.SpawnBullet(newBulletType, transform.position, -transform.right);
            }
            UpdateBounces();
        }
    }
}
