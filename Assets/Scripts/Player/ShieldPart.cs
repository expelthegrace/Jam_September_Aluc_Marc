using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPart : MonoBehaviour
{
    private const string BulletTag = "Bullet";
    private const string ObstacleTag = "Obstacle";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnCollisionEnter2D(Collision2D aCollision)
    void OnTriggerEnter2D(Collider2D aCollision)
    {
        if (aCollision.gameObject.CompareTag(BulletTag))
        {
            Destroy(aCollision.gameObject);
            Destroy(this.gameObject);
        }
        
        if (aCollision.gameObject.CompareTag(ObstacleTag))
        {
            Destroy(this.gameObject);
        }
    }
}
