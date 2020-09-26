using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPart : MonoBehaviour
{
    public AudioSource mCristalAudio;

    private const string BulletTag = "Bullet";
    private const string EnemyTag = "Enemy";
    private const string ObstacleTag = "Obstacle";
    // Start is called before the first frame update
    void Start()
    {
        mCristalAudio = GameObject.Find("cristalAudio").GetComponent<AudioSource>();
        if (mCristalAudio == null) Debug.Log("no cristal audio found");
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
        else if (aCollision.gameObject.CompareTag(ObstacleTag) || aCollision.gameObject.CompareTag(EnemyTag))
        {
            Destroy(this.gameObject);
        }
        mCristalAudio.pitch = Random.Range(0.9f, 1.1f);
        mCristalAudio.Play();
    }
}
