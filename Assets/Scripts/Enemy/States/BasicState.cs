using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BasicState : EnemyState
{
    public AudioSource mShootAudio;

    private Rigidbody2D mRigidBody;
    private Transform mPlayer;

    //Movement
    private float mLastTurn;
    [Header("Movement settings")]
    [SerializeField] private float mTimeBetweenTurns = 2;
    [SerializeField] private float mTimeToTurn = 1;
    [SerializeField] protected float mSpeed = 0.55f;
    [SerializeField] protected float mTurnAngle = 140;
    protected float mDistanceTriggerToTurn = 1.5f;

    //Shooting
    [Header("Shooting settings")]
    [SerializeField] private float mAngleBetweenCanons = 20f;
    [SerializeField] protected int mNumberOfExtraCanons = 2;
    [SerializeField] private float mCanonDistance = 0.2f;        

    private float mLastTimeShooted;
    [SerializeField] protected float mTimeBetweenShooting = 2f;
    [SerializeField] protected float mTimeBetweenBullet = 0.2f;
    [SerializeField] protected int mBulletsPerShooting = 4;

    protected BulletSpawner mSpawner;
    protected BulletSpawner.eBulletType mBulletType;

    protected float mTimeStateActive;
    protected float mTimeStateActivated;

    //Depending on the order of the starts of the scripts, certains events are declared after the eventListeners and dont get triggered at the start
    //So this function is called in the first Update, when all the starts are been called

    public override void HandleStart()
    {      
        mDurationTime = 5f;
        nameState = "'Normal' Mode";
        mStateID = 0;
        mBulletType = BulletSpawner.eBulletType.Normal;
        BasicStart();      
    }

    protected void BasicStart()
    {
        //Audio
        mShootAudio = GameObject.Find("ShootAudio").GetComponent<AudioSource>();

        mPlayer = GameObject.Find("Player").transform;
        if (mPlayer == null) Debug.Log("no player found");

        mOwner = gameObject.GetComponent<EnemyManager>();
        mSpawner = (BulletSpawner)FindObjectOfType(typeof(BulletSpawner));
        mSpawner.Emitter = gameObject;
        mOnEnemyStateChanged.Invoke();
        mLastTurn = Time.time;
        mTimeStateActivated = Time.time;
        mLastTimeShooted = Time.time;
        Assert.AreNotEqual(gameObject, null);
        mRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    //Use this instead of Update() Nota: no se com fer que els fills no puguin veure Update(), deu ser impossible si hereden de MonoBehaviour
    public override EnemyState StateUpdate()
    {
        mTimeStateActive = Time.time - mTimeStateActivated;

        ShootingManager();

        if (mTimeStateActive > mDurationTime)
            return GetRandomState();

        return null;
    }

    public override void StateFixedUpdate()
    {
        Move();     
    }

    protected void Move()
    {
        if (mRigidBody == null) return;
        mRigidBody.velocity = new Vector2(gameObject.transform.right.x, gameObject.transform.right.y) * mSpeed;

        Debug.DrawLine(transform.position, transform.position + transform.right * mDistanceTriggerToTurn);

        float newAngle = 0;
        if (Time.time - mLastTurn > mTimeBetweenTurns)
        {
            int cont = 0;
            bool obstacleFound;
            do
            {
                obstacleFound = false;

                newAngle = Random.Range(-mTurnAngle, mTurnAngle);

                Vector2 newDirectionRotated = StaticFunctions.RotateVector2(new Vector2(transform.right.x, transform.right.y), newAngle);
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, newDirectionRotated, mDistanceTriggerToTurn);         

                Debug.DrawRay(transform.position, newDirectionRotated, Color.yellow, 2f);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.gameObject.tag == "Obstacle")
                    {
                        obstacleFound = true;
                        break;
                    }
                }

                ++cont;
            } while (obstacleFound && cont < 10);

            mLastTurn = Time.time;
            StartCoroutine(Turn(newAngle, mTimeToTurn));
        }
       
    }


    private IEnumerator Turn(float aAngle, float aTime)
    {
        float startTime = Time.time;
        float startAngle = mRigidBody.rotation;

        while (Time.time < startTime + aTime)
        {
            float newRotation = Mathf.LerpAngle(startAngle, startAngle + aAngle, (Time.time - startTime) / aTime);
            mRigidBody.SetRotation(newRotation);
     
            yield return null;
        }             
    }

    protected void ShootingManager()
    {
        if (Time.time - mLastTimeShooted > mTimeBetweenShooting)
        {
            mLastTimeShooted = Time.time;
            StartCoroutine(ShootBurst());
            CameraAnimation.Shake(0.01f, mBulletsPerShooting * mTimeBetweenBullet);
        }
    }

    private void FanShoot()
    {     
        List<Vector2> canonDirections = new List<Vector2>();

        Vector2 centralVector = new Vector2(mPlayer.position.x - transform.position.x, mPlayer.position.y - transform.position.y).normalized;
        canonDirections.Add(centralVector);

        Vector2 canonTemp;

        for (int i = 0; i < mNumberOfExtraCanons / 2; ++i)
        {
            canonTemp = StaticFunctions.RotateVector2(centralVector, mAngleBetweenCanons * (i + 1));
            canonDirections.Add(canonTemp.normalized);
            canonTemp = StaticFunctions.RotateVector2(centralVector, -mAngleBetweenCanons * (i + 1));
            canonDirections.Add(canonTemp.normalized);
        }

        foreach(Vector2 direction in canonDirections)
        {
            //Debug.DrawRay(transform.position, direction, Color.yellow,2f);
            Vector3 direction3 = new Vector3(direction.x, direction.y, 0f);
            mSpawner.SpawnBullet(mBulletType, mOwner.GetSpeedIncrement(), transform.position + direction3 * mCanonDistance, direction3);

            mShootAudio.Play();
        }          
    }
    
    private IEnumerator ShootBurst()
    {
        float shootedTime = Time.time;
        int bulletsShooted = 0;
        while (bulletsShooted < mBulletsPerShooting)
        {
            if (Time.time - shootedTime > mTimeBetweenBullet)
            {
                shootedTime = Time.time;
                FanShoot();
                ++bulletsShooted;
            }           
            yield return null;
        }
    }

    protected virtual EnemyState GetRandomState()
    {
        EnemyState returnState = this;
        int state = mStateID;
        while (state == mStateID && StateManager.mNUmberOfEnemyStates > 1)
            state = Random.Range(0, StateManager.mNUmberOfEnemyStates);

        switch(state)
        {
            case 0:
                return gameObject.GetComponent<BasicState>();
            case 1:
                return gameObject.GetComponent<DivideShotState>();
            case 2:
                return gameObject.GetComponent<MoreShotsState>();                
        }

        return null;
    }
}
