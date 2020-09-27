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
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
    }

    void OnEventStartGame()
    {
        mGameTime = Time.time;
        gameObject.transform.position = new Vector3(-6, 0, 0);

    }

    public float GetSpeedIncrement()
    {
        int numIncrements = GetNumSpeedIncrements();
        return numIncrements * mSpeedIncrement;
    }

    public int GetNumSpeedIncrements()
    {
        return Mathf.Min(Mathf.FloorToInt((Time.time - mGameTime) / mSpeedIncrementSeconds), mMaxSpeedIncrements);
    }

    /*
     * Returns a float betwwen 0 and 1 indicating the bullets speed increment factor.
     */
    public float GetSpeedIncrementScale()
    {
        return (float)GetNumSpeedIncrements() / (float)mMaxSpeedIncrements;
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
