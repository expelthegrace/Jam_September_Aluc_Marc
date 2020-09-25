using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerSC : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        NotPlaying
    }

    public float score;

    public static UnityEvent mOnGameStateChanged = new UnityEvent();

    private GameManagerSC mGmReference;
    public  GameManagerSC GmRegerence { get { return mGmReference; } }

    private GameState mCurrentGameState;
    public GameState CurrentGameState
    {
        get { return mCurrentGameState; }
    }

    private bool mEarlyInvoked = false;
    private void EarlyEventInvoke()
    {
        mOnGameStateChanged.Invoke();
        mEarlyInvoked = true;
    }

    void Awake()
    {
        if (GmRegerence != null)
            GameObject.Destroy(GmRegerence);
        else
            mGmReference = this;

    }

    public void Start()
    {       
        GoToGameState(GameState.NotPlaying);
        score = 0;
    }

    public void GoToGameState(GameState aGameState)
    {
        if (aGameState == mCurrentGameState) return;
        mCurrentGameState = aGameState;
    }

    //Only for test
    public void Update()
    {
        if (!mEarlyInvoked) EarlyEventInvoke();

        if (Input.GetKeyDown(KeyCode.G))
        {
            GoToGameState(GameState.Playing);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GoToGameState(GameState.NotPlaying);
        }
    }


}
