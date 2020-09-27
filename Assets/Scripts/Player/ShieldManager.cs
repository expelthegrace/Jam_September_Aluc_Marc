using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShieldManager : MonoBehaviour
{
    public AudioSource mEquipShieldAudio;

    [SerializeField] private GameObject mPrefab = null;

    [Header("Shield parts settings")]
    [SerializeField] private float mRadiusFromGameObject = 0.2f;
    [SerializeField] private int mPartsNum = 8;

    [Header("Cooldown settings")]
    [SerializeField] public float mCooldownSeconds = 4.0f;
    private bool mCooldownTimeout;

    public static UnityEvent EventShieldCooldownStarted = new UnityEvent();

    private List<GameObject> mParts;

    private Coroutine mUpdateCooldownCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        mParts = new List<GameObject>();

        mEquipShieldAudio = GameObject.Find("shieldEquipAudio").GetComponent<AudioSource>();
        if (mEquipShieldAudio == null) Debug.Log("no quip shield audio found");

        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
        GameManagerSC.EventGameEnded.AddListener(OnEventEndGame);
    }

    private void OnEventStartGame()
    {
        mCooldownTimeout = true;
        ResetShield();
    }
    
    private void OnEventEndGame()
    {
        if (mUpdateCooldownCoroutine != null)
        {
            StopCoroutine(mUpdateCooldownCoroutine);
        }
    }

    private void StartShieldCooldown()
    {
        if (mUpdateCooldownCoroutine != null)
        {
            StopCoroutine(mUpdateCooldownCoroutine);
        }
        mUpdateCooldownCoroutine = StartCoroutine(UpdateCooldown());
        mCooldownTimeout = false;
        EventShieldCooldownStarted.Invoke();
    }

    private IEnumerator UpdateCooldown()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= mCooldownSeconds)
        {
            yield return null;
        }
        mCooldownTimeout = true;
    }

    // Update is called once per frame
    public void OnInputEventShield(InputAction.CallbackContext context)
    {
        if (CanGenerateShield())
        {
            ResetShield();

            mEquipShieldAudio.Play();
            CameraAnimation.Shake(0.08f, 0.09f);

            float shielRadius = Mathf.Max(gameObject.GetComponent<SpriteRenderer>().bounds.size.x, gameObject.GetComponent<SpriteRenderer>().bounds.size.y) + mRadiusFromGameObject;
            float angleIncrement = (2.0f * Mathf.PI)/ mPartsNum;
            for (int part = 0; part < mPartsNum; ++part)
            {
                Vector3 offset = shielRadius * new Vector3(Mathf.Cos(angleIncrement * part), Mathf.Sin(angleIncrement * part), 0.0f);
                GameObject shieldPart = Instantiate(mPrefab, gameObject.transform.position + offset, Quaternion.identity);
                shieldPart.transform.parent = gameObject.transform;
                mParts.Add(shieldPart);
            }

            StartShieldCooldown();
        }
    }

    private void ResetShield()
    {
        foreach (GameObject part in mParts)
        {
            if (part != null)
            {
                Destroy(part);
            }
        }
        mParts.Clear();
    }

    private bool CanGenerateShield()
    {
        return mCooldownTimeout;
    }

}
