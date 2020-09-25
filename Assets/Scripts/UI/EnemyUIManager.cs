﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIManager : MonoBehaviour
{
    private float mTimeStartedPlaying = 0;

    [SerializeField] private float mBlurActivateTime = 1;
    [SerializeField] private float mBlurDeactivateTime = 1;

    public GameManagerSC mGameManager;

    public TMP_Text stateTitleText;
    public TMP_Text scoreText;
    public Image stateContdownImageBar;

    public GameObject mPlayingPanel;
    public GameObject mNotPlayingPanel;

    public Image blurPanel;

    public GameObject mEnemy;
    private StateManager mEnemyStateManager;
    private EnemyState mCurrentState;

    // Start is called before the first frame update
    void Start()
    {
        mEnemyStateManager = mEnemy.GetComponent<StateManager>();       
       
        EnemyState.mOnEnemyStateChanged.AddListener(StartCountdownnImage);
        GameManagerSC.mOnGameStateChanged.AddListener(GameStateChanged);

        var tempColor = blurPanel.color;
        tempColor.a = 0;
        blurPanel.color = tempColor;
    }

    public void GameStateChanged()
    {
        switch(mGameManager.CurrentGameState)
        {
            case GameManagerSC.GameState.Playing:
                mGameManager.score = 0;
                mTimeStartedPlaying = Time.time;
                break;
        }

        SwitchPanel();
    }

    public void Update()
    {       
        mGameManager.score = (int)(Time.time - mTimeStartedPlaying);
        if (mGameManager.CurrentGameState == GameManagerSC.GameState.Playing)
        {
            scoreText.text = BeautyScore(mGameManager.score).ToString();

        }
       
    }

    private void StartCountdownnImage()
    {
        Debug.Log("fff");
        StopCoroutine(UpdateCountdownImage());
        StartCoroutine(UpdateCountdownImage());
       
    }

    private IEnumerator UpdateCountdownImage()
    {
        mCurrentState = mEnemyStateManager.CurrentState;

        stateTitleText.text = mCurrentState.nameState;

        float StateDuration = mCurrentState.mDurationTime;
        stateContdownImageBar.fillAmount = 1f;

        float startTime = Time.time;
       
        while (stateContdownImageBar.fillAmount > 0.01f)
        {
            stateContdownImageBar.fillAmount = 1 - (Time.time - startTime) / StateDuration;
            yield return null;
        }
    }

    private string BeautyScore(float aScore)
    {
        string scoreString = ((int)aScore).ToString();

        scoreString = ("00000").Substring(scoreString.Length) + scoreString;
        return scoreString;
    }

    private void SwitchPanel()
    {
        if (mGameManager.CurrentGameState == GameManagerSC.GameState.Playing)
        {
            Debug.Log("Playing");
            StartCoroutine(deactivateBlur());
            mPlayingPanel.SetActive(true);
            mNotPlayingPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Not playing");
            StartCoroutine(activateBlur());
            mPlayingPanel.SetActive(false);
            mNotPlayingPanel.SetActive(true);
           
        }
    }

    private IEnumerator activateBlur()
    {
        Debug.Log("Activant blur");
        float startTime = Time.time;

        while (Time.time - startTime <= mBlurActivateTime)
        {
            var tempColor = blurPanel.color;
            tempColor.a = Mathf.Lerp(0,1, (Time.time - startTime) / mBlurActivateTime);
            blurPanel.color = tempColor;

            yield return null;
        }      
    }

    private IEnumerator deactivateBlur()
    {
        float startTime = Time.time;

        while (Time.time - startTime <= mBlurActivateTime)
        {
            var tempColor = blurPanel.color;
            tempColor.a = Mathf.Lerp(1,0, (Time.time - startTime) / mBlurDeactivateTime);
            blurPanel.color = tempColor;

            yield return null;
        }     
    }
}

