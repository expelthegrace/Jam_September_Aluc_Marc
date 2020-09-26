using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTController : MonoBehaviour
{
    private AudioSource mMenuAudio;
    private AudioSource[] mPlayGameAudios;
    private int mPlayGamePlayingAudioIndex;

    // Start is called before the first frame update
    void Start()
    {
        LoadAudios();
        mPlayGamePlayingAudioIndex = -1;
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
        GameManagerSC.EventGameEnded.AddListener(OnEventEndGame);
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
        
    }
}
