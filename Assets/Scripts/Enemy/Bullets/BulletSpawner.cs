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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
        }
    }

    public void SpawnBullet(Vector3 aPosition, Vector3 aDirection)
    {
        Instantiate(mBulletPrefab, aPosition, StaticFunctions.Get2DQuaternionFromDirection(aDirection));
    }
}
