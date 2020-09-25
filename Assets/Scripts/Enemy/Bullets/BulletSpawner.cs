using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner: MonoBehaviour
{
    //TODO pool of bullets

    public GameObject mBulletPrefab;
    public GameObject mDividableBulletPrefab;

    public GameObject Emitter { get; set; }

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
        GameObject bullet = Instantiate(mBulletPrefab, aPosition, StaticFunctions.Get2DQuaternionFromDirection(aDirection));
        IgnoreCollision(bullet);
    }

    public void SpawnBullet(Vector3 aPosition, Quaternion aRotation)
    {
        GameObject bullet = Instantiate(mBulletPrefab, aPosition, aRotation);
        IgnoreCollision(bullet);
    }

    public void SpawnDividableBullet(Vector3 aPosition, Vector3 aDirection)
    {
        GameObject bullet = Instantiate(mDividableBulletPrefab, aPosition, StaticFunctions.Get2DQuaternionFromDirection(aDirection));
        bullet.GetComponent<DividableBullet>().Spawner = this;
        IgnoreCollision(bullet);
    }

    public void SpawnDividableBullet(Vector3 aPosition, Quaternion aRotation)
    {
        GameObject bullet = Instantiate(mDividableBulletPrefab, aPosition, aRotation);
        bullet.GetComponent<DividableBullet>().Spawner = this;
        IgnoreCollision(bullet);
    }

    private void IgnoreCollision(GameObject aBulletObject)
    {
        Physics2D.IgnoreCollision(Emitter.GetComponent<Collider2D>(), aBulletObject.GetComponent<Collider2D>());
    }
}
