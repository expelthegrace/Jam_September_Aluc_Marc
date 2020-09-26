using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    [SerializeField] public static GameObject Camera;
    public static GameManagerSC mCameraManager;

    // How long the object should shake for.
    private static float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    [SerializeField] private static float shakeAmount = 0.7f;
    [SerializeField] private static float decreaseFactor = 1.0f;

    private static Vector3 originalPos;

    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        mCameraManager = GameObject.Find("GameManager").GetComponent<GameManagerSC>();
        originalPos = Camera.transform.localPosition;
    }

    public static void Shake(float aShakeAmount = 0.2f, float aTimeOfShake = 0.1f)
    {
        if (mCameraManager.CurrentGameState != GameManagerSC.GameState.Playing) return;
        Camera.transform.position = originalPos;
        shakeDuration = aTimeOfShake;
        shakeAmount = aShakeAmount;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }
}

