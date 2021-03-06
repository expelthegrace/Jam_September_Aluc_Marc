﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
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
    public GameObject mHowToPanel;

    public Image blurPanel;

    public GameObject mEnemy;
    private StateManager mEnemyStateManager;
    private EnemyState mCurrentState;

    public Text mYourScoreText;
    public Button mPlayButton;

    private Coroutine mCountdownImageCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        mEnemyStateManager = mEnemy.GetComponent<StateManager>();

        EnemyState.mOnEnemyStateChanged.AddListener(StartCountdownnImage);
        GameManagerSC.EventGameStarted.AddListener(OnEventGameStarted);
        GameManagerSC.EventGameEnded.AddListener(OnEventGameEnded);

        var tempColor = blurPanel.color;
        tempColor.a = 0;
        blurPanel.color = tempColor;

        BackToMenuFromHowToPlay();
    }

    public void OnEventGameStarted()
    {
        mGameManager.score = 0;
        mTimeStartedPlaying = Time.time;

        SwitchPanel();
    }

    private void OnEventGameEnded()
    {
        SwitchPanel();
    }

    public void Update()
    {       
        mGameManager.score = (int)(Time.time - mTimeStartedPlaying);
        if (mGameManager.CurrentGameState == GameManagerSC.GameState.Playing)
        {
            scoreText.text = BeautyScore(mGameManager.score).ToString();
            scoreText.color = ComputeScoreColor();
        }
       
    }

    private void StartCountdownnImage()
    {
        if (mCountdownImageCoroutine != null)
        {
            StopCoroutine(mCountdownImageCoroutine);
        }
        mCountdownImageCoroutine = StartCoroutine(UpdateCountdownImage());
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
    private Color ComputeScoreColor()
    {
        var enemyManager = mEnemy.GetComponent<EnemyManager>();

        float IncrementScale = enemyManager.GetSpeedIncrementScale();
        Color result = Color.white;
        result.g = 1.0f - IncrementScale;
        result.b = 1.0f - IncrementScale;
        //TODO play with other color range instead with just red 
        return result;
    }

    private void SwitchPanel()
    {
        if (mGameManager.CurrentGameState == GameManagerSC.GameState.Playing)
        {
            StartCoroutine(deactivateBlur());
            mPlayingPanel.SetActive(true);
            mNotPlayingPanel.SetActive(false);
        }
        else
        {
            StartCoroutine(activateBlur());
            mPlayingPanel.SetActive(false);
            mNotPlayingPanel.SetActive(true);

            mPlayButton.GetComponentInChildren<Text>().color = Color.black;

            if (mGameManager.score != 0)
            {
                mYourScoreText.gameObject.SetActive(true);
                mYourScoreText.text = "You survived  " + mGameManager.score.ToString() + "  seconds";
            }
            else
                mYourScoreText.gameObject.SetActive(false);

        }
}

    private IEnumerator activateBlur()
    {
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

    public void BackToMenuFromHowToPlay()
    {
        mHowToPanel.SetActive(false);
        mNotPlayingPanel.SetActive(true);
    }

    public void GoToHowToPlay()
    {
        mHowToPanel.SetActive(true);
        mNotPlayingPanel.SetActive(false);
    }

}

