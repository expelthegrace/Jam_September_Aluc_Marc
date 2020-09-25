using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIManager : MonoBehaviour
{
    private float mTimeStartedPlaying = 0;

    public GameManagerSC mGameManager;

    public TMP_Text stateTitleText;
    public TMP_Text scoreText;
    public Image stateContdownImageBar;

    public GameObject mEnemy;
    private StateManager mEnemyStateManager;
    private EnemyState mCurrentState;

    // Start is called before the first frame update
    void Start()
    {
        mEnemyStateManager = mEnemy.GetComponent<StateManager>();       
       
        EnemyState.mOnEnemyStateChanged.AddListener(StartCountdownnImage);
        GameManagerSC.mOnGameStateChanged.AddListener(GameStateChanged);
    }

    private void GameStateChanged()
    {
        switch(mGameManager.CurrentState)
        {
            case GameManagerSC.GameState.Playing:
                mGameManager.score = 0;
                mTimeStartedPlaying = Time.time;
                break;
        }
    }

    public void Update()
    {       
        mGameManager.score = (int)(Time.time - mTimeStartedPlaying);
        if (mGameManager.CurrentState == GameManagerSC.GameState.Playing) scoreText.text = mGameManager.score.ToString();
    }

    private void StartCountdownnImage()
    {
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

}

