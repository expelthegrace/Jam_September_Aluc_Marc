using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseState : BasicState
{
    public GameObject mPlayer;
    public GameObject mExtraEnemy;
    public Vector3[] mSpawnPoints;

    private float mLastTimeShootedExtraEnemy;
    [SerializeField] private float mTimeBetweenShootingExtraEnemy = 1f;
    [SerializeField] private float mTimeBetweenBulletExtraEnemy = 0.2f;
    [SerializeField] private int mBulletsPerShootingExtraEnemy = 2;
    [SerializeField] private int mNumberOfCanonsExtraEnemy = 20;

    private AudioSource mAppearAudio;

    private List<Coroutine> mShootCoroutines;
    public void Start()
    {
        mExtraEnemy = GameObject.Find("SurpriseEnemy");
        mPlayer = GameObject.Find("Player");
        
        GameObject SpawnPoints = GameObject.Find("SpawnPoints");
        Transform[] spawnPointsTransforms = SpawnPoints.GetComponentsInChildren<Transform>();
        mSpawnPoints = new Vector3[spawnPointsTransforms.Length];
        for (int i = 0; i < mSpawnPoints.Length; ++i)
        {
            mSpawnPoints[i] = spawnPointsTransforms[i].position;
        }

        mAppearAudio = GameObject.Find("AppearAudio").GetComponent<AudioSource>();
        if (mAppearAudio == null) Debug.Log("No audio for surprise appear found");

        mShootCoroutines = new List<Coroutine>();

        mExtraEnemy.SetActive(false);
    }
    public override void HandleStart()
    {
        mDurationTime = 3f;
        nameState = "Surprise!";
        mStateID = 3;
        mBulletType = BulletSpawner.eBulletType.Normal;

        CameraAnimation.Shake();

        mExtraEnemy.SetActive(true);
        float minDistance = float.PositiveInfinity;
        Vector3 spawnPosition = Vector3.zero;
        float minDistanceToPlayer = Mathf.Max(mPlayer.GetComponent<SpriteRenderer>().bounds.size.x, mPlayer.GetComponent<SpriteRenderer>().bounds.size.y) +
                                    Mathf.Max(mExtraEnemy.GetComponent<SpriteRenderer>().bounds.size.x, mExtraEnemy.GetComponent<SpriteRenderer>().bounds.size.y);
        foreach (Vector3 position in mSpawnPoints)
        {
            float currentPositionDistance = Vector3.Distance(mPlayer.transform.position, position);
            if (currentPositionDistance < minDistance && currentPositionDistance > minDistanceToPlayer)
            {
                minDistance = currentPositionDistance;
                spawnPosition = position;
            }
        }
        mExtraEnemy.transform.position = spawnPosition;

        mLastTimeShootedExtraEnemy = Time.time;

        mAppearAudio.Play();

        base.BasicStart();
    }

    public override void HandleExit() 
    {
        mExtraEnemy.SetActive(false);
        foreach (Coroutine shootCorrotuine in mShootCoroutines)
        {
            StopCoroutine(shootCorrotuine);
        }
        mShootCoroutines.Clear();
    }

    public override EnemyState StateUpdate()
    {
        mTimeStateActive = Time.time - mTimeStateActivated;

        if (mTimeStateActive > mDurationTime)
        {
            return GetRandomState();
        }

        return null;
    }

    public override void StateFixedUpdate()
    {
        base.Move();
        base.ShootingManager();
        
        if (Time.time - mLastTimeShootedExtraEnemy > mTimeBetweenShootingExtraEnemy)
        {
            mLastTimeShootedExtraEnemy = Time.time;
            mShootCoroutines.Add(StartCoroutine(ShootBurst()));
            CameraAnimation.Shake(0.03f, mBulletsPerShootingExtraEnemy * mTimeBetweenBulletExtraEnemy);
        }
    }

    private IEnumerator ShootBurst()
    {
        float shootedTime = Time.time;
        int bulletsShooted = 0;
        while (bulletsShooted < mBulletsPerShootingExtraEnemy)
        {
            if (Time.time - shootedTime > mTimeBetweenBulletExtraEnemy)
            {
                shootedTime = Time.time;
                FanShoot();
                ++bulletsShooted;
            }
            yield return null;
        }
    }

    private void FanShoot()
    {
        float radius = Mathf.Max(mExtraEnemy.gameObject.GetComponent<SpriteRenderer>().bounds.size.x, mExtraEnemy.gameObject.GetComponent<SpriteRenderer>().bounds.size.y);
        float angleIncrement = (2.0f * Mathf.PI) / mNumberOfCanonsExtraEnemy;
        for (int canon = 0; canon < mNumberOfCanonsExtraEnemy; ++canon)
        {
            Vector3 offset = radius * new Vector3(Mathf.Cos(angleIncrement * canon), Mathf.Sin(angleIncrement * canon), 0.0f);
            mSpawner.SpawnBullet(mBulletType, mOwner.GetSpeedIncrement(), mExtraEnemy.gameObject.transform.position + offset, offset);
            mShootAudio.Play();
        }
    }
}
