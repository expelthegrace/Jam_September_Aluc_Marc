using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIManager : MonoBehaviour
{
    public TMP_Text stateTitleText;
    public Image stateContdownImageBar;

    public GameObject mEnemy;
    private StateManager mEnemyStateManager;
    private EnemyState mCurrentState;

    // Start is called before the first frame update
    void Start()
    {
        mEnemyStateManager = mEnemy.GetComponent<StateManager>();       
       
        EnemyState.mOnEnemyStateChanged.AddListener(StartCountdownnImage);
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

