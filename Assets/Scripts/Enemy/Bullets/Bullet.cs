using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const string ObstacleTag = "Obstacle";
    [SerializeField] private float mSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * mSpeed;
    }

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (aCollision.gameObject.tag == ObstacleTag)
        {
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            if (aCollision.GetContacts(contacts) > 0)
            {
                Vector3 Normal = contacts[0].normal;
                Vector3 Reflection = transform.right - 2 * (Vector3.Dot(transform.right, Normal)) * Normal;
                transform.rotation = StaticFunctions.Get2DQuaternionFromDirection(Reflection);
            }
        }
    }
}
