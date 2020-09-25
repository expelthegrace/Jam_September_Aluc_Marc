using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D mRigidBody;
    [SerializeField] private float mSpeed = 2.0f;
    private const string BulletTag = "Bullet";

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = Input.GetKey(KeyCode.LeftShift) ? mSpeed * 0.5f : mSpeed;
        mRigidBody.velocity = new Vector3(horizontal * speed, vertical * speed, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (aCollision.gameObject.tag == BulletTag)
        {
            Debug.Log("RIP");
        }
    }
}
