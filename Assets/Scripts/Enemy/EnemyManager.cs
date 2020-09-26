using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Bullets speed settings")]
    [SerializeField] private float mSpeedIncrementSeconds = 5f;
    [SerializeField] private float mSpeedIncrement = 0.1f;
    [SerializeField] public int mMaxSpeedIncrements { get; set; } = 10;

    private float mGameTime;

    // Start is called before the first frame update
    void Start()
    {
        GameManagerSC.mOnGameStateChanged.AddListener(StartGame);
    }

    void StartGame()
    {
        mGameTime = Time.time;
    }

    public float GetSpeedIncrement()
    {
        int numIncrements = GetNumIncrements();
        return numIncrements * mSpeedIncrement;
    }

    public int GetNumIncrements()
    {
        return Mathf.Min(Mathf.FloorToInt((Time.time - mGameTime) / mSpeedIncrementSeconds), mMaxSpeedIncrements);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
