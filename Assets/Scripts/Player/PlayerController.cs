using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D mRigidBody;
    [SerializeField] private float mSpeed = 2.0f;
    private const string BulletTag = "Bullet";
    private const string EnemyTag = "Enemy";

    [SerializeField] private bool mCanDie = false;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
        GameManagerSC.EventGameEnded.AddListener(OnEventEndGame);
    }

    private void OnEventStartGame()
    {
        mRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnEventEndGame()
    {
        mRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = Input.GetKey(KeyCode.LeftShift) ? mSpeed * 0.5f : mSpeed;
        mRigidBody.velocity = new Vector3(horizontal * speed, vertical * speed, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (aCollision.gameObject.tag == BulletTag || aCollision.gameObject.tag == EnemyTag)
        {
            CameraAnimation.Shake();
            if (mCanDie)
            {
                GameManagerSC.GoToGameState(GameManagerSC.GameState.NotPlaying);
            }
        }
    }
}
