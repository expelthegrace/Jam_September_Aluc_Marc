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

    private GameState mCurrentState;
    public GameState CurrentState
    {
        get { return mCurrentState; }
    }

    void Awake()
    {
        if (GmRegerence != null)
            GameObject.Destroy(GmRegerence);
        else
            mGmReference = this;

    }

    private void Start()
    {
        mCurrentState = GameState.Playing;
        score = 0;
    }

    public void GoToGameState(GameState aGameState)
    {
        mCurrentState = aGameState;
        mOnGameStateChanged.Invoke();
    }

   
}
