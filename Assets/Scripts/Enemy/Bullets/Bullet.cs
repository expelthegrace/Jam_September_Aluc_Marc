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
        Vector3 Normal = GetObstacleNormal(aCollision);
        if (Normal != Vector3.zero)
        {
            Vector3 Reflection = StaticFunctions.GetReflectionVector(transform.right, Normal);
            transform.rotation = StaticFunctions.Get2DQuaternionFromDirection(Reflection);
        }
    }

    /**
     *  Gets the normal of the obstacle type game object from a collision. 
     *  Zero if no collision with an obstacle has occured.
     */
    protected Vector3 GetObstacleNormal(Collision2D aCollision)
    {
        Vector3 normal = Vector3.zero;
        if (aCollision.gameObject.tag == ObstacleTag)
        {
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            if (aCollision.GetContacts(contacts) > 0)
            {
                normal = contacts[0].normal;
              
            }
        }
        return normal;
    }
}
