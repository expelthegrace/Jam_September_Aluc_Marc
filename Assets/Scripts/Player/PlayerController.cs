using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D mRigidBody;
    [SerializeField] private float mSpeed = 2.0f;
    private const string BulletTag = "Bullet";
    private const string EnemyTag = "Enemy";

    [SerializeField] private bool mCanDie = false;
    
    private bool mSlowMovement;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
        GameManagerSC.EventGameEnded.AddListener(OnEventEndGame);
        mSlowMovement = false;
    }

    private void OnEventStartGame()
    {
        gameObject.transform.position = new Vector3(6, 0, 0);
        mRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnEventEndGame()
    {
        mRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void OnInputEventMove(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        float horizontal = inputValue.x;
        float vertical = inputValue.y;

        float speed = mSlowMovement ? mSpeed * 0.5f : mSpeed;
        mRigidBody.velocity = new Vector3(horizontal * speed, vertical * speed, 0.0f);
    }
    
    public void OnInputEventSlowMovement(InputAction.CallbackContext context)
    {
        mSlowMovement = context.performed;
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
