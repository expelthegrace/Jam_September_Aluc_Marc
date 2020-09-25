using System.Collections;
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

    public void SpawnBullet(GameObject aEmitter, Vector3 aPosition, Vector3 aDirection)
    {
        GameObject bullet = Instantiate(mBulletPrefab, aPosition, StaticFunctions.Get2DQuaternionFromDirection(aDirection));
        IgnoreCollision(aEmitter, bullet);
    }
    public void SpawnBullet(GameObject aEmitter, Vector3 aPosition, Quaternion aRotation)
    {
        GameObject bullet = Instantiate(mBulletPrefab, aPosition, aRotation);
        IgnoreCollision(aEmitter, bullet);
    }

    private void IgnoreCollision(GameObject aEmitter, GameObject aBulletObject)
    {
        Physics2D.IgnoreCollision(aEmitter.GetComponent<Collider2D>(), aBulletObject.GetComponent<Collider2D>());
    }
}
