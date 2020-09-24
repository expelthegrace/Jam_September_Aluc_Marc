﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateManager : MonoBehaviour
{

    private EnemyState mCurrentState = null;

    //Declare the states here
    private BasicState mBasicState = null;
    private DivideShotState mDivideShotState = null;

    void Start()
    {
        mBasicState = gameObject.AddComponent<BasicState>();
        mDivideShotState = gameObject.AddComponent<DivideShotState>();

        GoToState(mBasicState);
    }

    void Update()
    {
        EnemyState stateReturn = null;

        if (mCurrentState != null)
            stateReturn = mCurrentState.StateUpdate();

        if (stateReturn != null)
            GoToState(stateReturn);
        
    }

    private void FixedUpdate()
    {
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

 
    /*
     * Actualment no puc tenir un currentState = new State() si aquest State hereda de monobehaviour. S'hauria de fer amb gameObject.AddComponent(State);
     * */
}