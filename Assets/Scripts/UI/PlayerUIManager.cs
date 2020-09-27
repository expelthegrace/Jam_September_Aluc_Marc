using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{

    public Image coolDownImageBar;

    public GameObject mPlayer;
    private ShieldManager mPlayerShieldManager;

    private Coroutine mCooldownImageCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        mPlayerShieldManager = mPlayer.GetComponent<ShieldManager>();
        ShieldManager.EventShieldCooldownStarted.AddListener(StartShieldCooldown);
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);
    }

    private void OnEventStartGame()
    {
        coolDownImageBar.fillAmount = 1.0f;
    }

    private void StartShieldCooldown()
    {
        if (mCooldownImageCoroutine != null)
        {
            StopCoroutine(mCooldownImageCoroutine);
        }
        mCooldownImageCoroutine = StartCoroutine(UpdateCooldownImage());
    }

    private IEnumerator UpdateCooldownImage()
    {
        float startTime = Time.time;
        float coolDownDuration = mPlayerShieldManager.mCooldownSeconds;
        coolDownImageBar.fillAmount = 0.0f;

        while (coolDownImageBar.fillAmount < 1.0f)
        {
            coolDownImageBar.fillAmount = (Time.time - startTime) / coolDownDuration;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
