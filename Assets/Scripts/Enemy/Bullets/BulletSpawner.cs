using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner: MonoBehaviour
{
    //TODO pool of bullets

    public GameObject mBulletContainer;

    public enum eBulletType
    {
        Normal,
        Dividable
    }

    [SerializeField] private GameObject mBulletPrefab = null;
    [SerializeField] private GameObject mDividableBulletPrefab = null;

    public GameObject Emitter { get; set; }

    void Start()
    {
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
    }

    private void OnEventStartGame()
    {
        Transform[] bullets = mBulletContainer.GetComponentsInChildren<Transform>();
        foreach(Transform bullet in bullets)
        {
            if (bullet.gameObject != mBulletContainer)
            {
                Destroy(bullet.gameObject);
            }            
        }
    }

    public void SpawnBullet(eBulletType aBulletType, float aSpeedIncrement, Vector3 aPosition, Vector3 aDirection)
    {
        SpawnBullet(aBulletType, aSpeedIncrement, aPosition, StaticFunctions.Get2DQuaternionFromDirection(aDirection));
    }

    public void SpawnBullet(eBulletType aBulletType, float aSpeedIncrement, Vector3 aPosition, Quaternion aRotation)
    {
        GameObject bulletObject = Instantiate(GetBulletPrefabFromType(aBulletType), aPosition, aRotation, mBulletContainer.transform);
        Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
        bulletComponent.Spawner = this;
        bulletComponent.GetComponent<Bullet>().IncrementSpeed(aSpeedIncrement);
        IgnoreCollision(bulletObject);
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
