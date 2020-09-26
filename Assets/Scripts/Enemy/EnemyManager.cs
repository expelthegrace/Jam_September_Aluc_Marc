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
        StartGame(); //TODO call this when new game is started
    }

    void StartGame()
    {
        mGameTime = Time.time;
    }

    public float GetSpeedIncrement()
    {
        int numIncrements = Mathf.Min(Mathf.FloorToInt((Time.time - mGameTime) / mSpeedIncrementSeconds), mMaxSpeedIncrements);
        return numIncrements * mSpeedIncrement;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
