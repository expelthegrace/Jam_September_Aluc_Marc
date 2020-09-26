using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject mGameManager;

    private EnemyState mCurrentState = null;
    public EnemyState CurrentState
    {
        get { return mCurrentState; }
    }

    //Declare the states here
    static public int mNUmberOfEnemyStates = 3;
    private EnemyState mEmptyState = null;
    private BasicState mBasicState = null;
    private DivideShotState mDivideShotState = null;
    private MoreShotsState mMoreShotsState = null;


    void Start()
    {
        mEmptyState = gameObject.AddComponent<EnemyState>();
        mBasicState = gameObject.AddComponent<BasicState>();
        mDivideShotState = gameObject.AddComponent<DivideShotState>();
        mMoreShotsState = gameObject.AddComponent<MoreShotsState>();
        GameManagerSC.mOnGameStateChanged.AddListener(GameMightStarted);

        GoToState(mEmptyState);
    }


    void Update()
    {
        //if (mGameManager.GetComponent<GameManagerSC>().CurrentGameState != GameManagerSC.GameState.Playing) return;
        EnemyState stateReturn = null;

        if (mCurrentState != null)
            stateReturn = mCurrentState.StateUpdate();

        if (stateReturn != null)
            GoToState(stateReturn);
        
    }

    private void FixedUpdate()
    {
        //if (mGameManager.GetComponent<GameManagerSC>().CurrentGameState != GameManagerSC.GameState.Playing) return;
        if (mCurrentState != null)
            mCurrentState.StateFixedUpdate();
    }

    private void GoToState(EnemyState aNewEnemyState)
    {
        if (mCurrentState != null)
            mCurrentState.HandleExit();
        mCurrentState = aNewEnemyState;
        mCurrentState.HandleStart();

    }

    private void GameMightStarted()
    {
        if (mGameManager.GetComponent<GameManagerSC>().CurrentGameState == GameManagerSC.GameState.Playing)
            GoToState(mBasicState);
        else
            GoToState(mEmptyState);
    }

 
    /*
     * Actualment no puc tenir un currentState = new State() si aquest State hereda de monobehaviour. S'hauria de fer amb gameObject.AddComponent(State);
     * */
}
