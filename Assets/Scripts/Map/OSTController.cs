using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTController : MonoBehaviour
{
    [SerializeField] private float mInitialVolume = 0.3f;
    private AudioSource mMenuAudio;
    private AudioSource[] mPlayGameAudios;
    private int mPlayGamePlayingAudioIndex;
    [SerializeField] private GameObject mEnemy;
    private EnemyManager mEnemyManager;
    [SerializeField] private float mPitchIncrementStep = 0.5f;
    [SerializeField] private float mVolumeIncrementStep = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        LoadAudios();
        mPlayGamePlayingAudioIndex = -1;
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
        GameManagerSC.EventGameEnded.AddListener(OnEventEndGame);
        mEnemyManager = mEnemy.GetComponent<EnemyManager>();
    }

private void LoadAudios()
    {
        string[] playAudios = { "PlayAudio1", "PlayAudio2" };
        mPlayGameAudios = new AudioSource[playAudios.Length];
        //foreach (int i = 0; i <  string audioObjectName in playAudios)
        for (int i = 0; i < playAudios.Length; ++i)
        {
            string audioObjectName = playAudios[i];
            AudioSource audio = GameObject.Find(audioObjectName).GetComponent<AudioSource>();
            audio.volume = mInitialVolume;
            if (audioObjectName == null)
            {
                Debug.LogWarning("no audio for playing game" + audioObjectName + " found");
            }
            else
            {
                mPlayGameAudios[i] = audio;
            }
        }

        mMenuAudio = GameObject.Find("MenuAudio").GetComponent<AudioSource>();
        if (mMenuAudio == null) Debug.Log("no audio for menu found");
    }

    private void OnEventStartGame()
    {
        mMenuAudio.Stop();

        mPlayGamePlayingAudioIndex = Random.Range(0, mPlayGameAudios.Length);
        mPlayGameAudios[mPlayGamePlayingAudioIndex].Play();
    }

    private void OnEventEndGame()
    {
        if (mPlayGamePlayingAudioIndex != -1)
        {
            mPlayGameAudios[mPlayGamePlayingAudioIndex].Stop();
            mPlayGamePlayingAudioIndex = -1;
        }       

        //TODO game over??
        mMenuAudio.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (mPlayGamePlayingAudioIndex != -1)
        {
            float incrementScale = mEnemyManager.GetSpeedIncrementScale() * 10;
            mPlayGameAudios[mPlayGamePlayingAudioIndex].pitch = 1 + incrementScale * mPitchIncrementStep;
            mPlayGameAudios[mPlayGamePlayingAudioIndex].volume = mInitialVolume + incrementScale * mVolumeIncrementStep;
        }
    }
}
