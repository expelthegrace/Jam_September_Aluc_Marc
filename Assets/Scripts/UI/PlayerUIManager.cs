using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{

    public Image coolDownImageBar;

    private float mInitialAlphaBar;
    private float mInitialAlphaShieldImage;

    public GameObject mPlayer;
    private ShieldManager mPlayerShieldManager;

    private Coroutine mCooldownImageCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        mPlayerShieldManager = mPlayer.GetComponent<ShieldManager>();
        ShieldManager.EventShieldCooldownStarted.AddListener(StartShieldCooldown);
        GameManagerSC.EventGameStarted.AddListener(OnEventStartGame);

        mInitialAlphaBar = coolDownImageBar.color.a;
    }

    private void OnEventStartGame()
    {
        coolDownImageBar.fillAmount = 1.0f;

        coolDownImageBar.color = new Color(coolDownImageBar.color.r, coolDownImageBar.color.g, coolDownImageBar.color.b, mInitialAlphaBar);

    }

    private void StartShieldCooldown()
    {

        coolDownImageBar.color = new Color(coolDownImageBar.color.r, coolDownImageBar.color.g, coolDownImageBar.color.b, mInitialAlphaBar / 2f);

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

        coolDownImageBar.color = new Color(coolDownImageBar.color.r, coolDownImageBar.color.g, coolDownImageBar.color.b, mInitialAlphaBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
