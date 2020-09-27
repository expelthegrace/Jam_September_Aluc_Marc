using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTController : MonoBehaviour
{
    [SerializeField] private GameObject mEnemy;
    private EnemyManager mEnemyManager;

    private AudioSource mMainAudio;
    [SerializeField] private float mInitialVolume = 0.7f;
    [SerializeField] private float mPitchIncrementStep = 0.5f;
    [SerializeField] private float mVolumeIncrementStep = 1.0f;

    private bool mCanModfifyAudio;

    private Coroutine mResetSettingsCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        mCanModfifyAudio = false;
        LoadAudio();
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
        GameManagerSC.EventGameEnded.AddListener(OnEventEndGame);
        mEnemyManager = mEnemy.GetComponent<EnemyManager>();
    }

    private void LoadAudio()
    {
        mMainAudio = gameObject.GetComponent<AudioSource>();
        if (mMainAudio == null) Debug.Log("No main audio found");
        mMainAudio.volume = mInitialVolume;
    }

    private void OnEventStartGame()
    {
        if (mResetSettingsCoroutine != null)
        {
            StopCoroutine(mResetSettingsCoroutine);
        }
        mCanModfifyAudio = true;
        mMainAudio.volume = mInitialVolume;
        mMainAudio.pitch = 1;
    }

    private void OnEventEndGame()
    {
        mCanModfifyAudio = false;
        mResetSettingsCoroutine = StartCoroutine(ResetSettings());
    }

    private IEnumerator ResetSettings()
    {
        float startTime = Time.time;

        while (mMainAudio.volume > mInitialVolume || mMainAudio.pitch > 1)
        {
            mMainAudio.volume = Mathf.Max(mInitialVolume, mMainAudio.volume - 0.01f);
            mMainAudio.pitch = Mathf.Max(1, mMainAudio.pitch - 0.001f);
            yield return null;
        }
    }

    void Update()
    {
        if (mCanModfifyAudio)
        {
            float incrementScale = mEnemyManager.GetSpeedIncrementScale() * 10;
            mMainAudio.pitch = 1 + incrementScale * mPitchIncrementStep;
            mMainAudio.volume = mInitialVolume + incrementScale * mVolumeIncrementStep;
        }        
    }
}
