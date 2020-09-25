using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField] private GameObject mPrefab;
    [SerializeField] private float mRadiusFromGameObject = 0.2f;
    [SerializeField] private int mPartsNum = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanGenerateShield())
        {
            float shielRadius = Mathf.Max(gameObject.GetComponent<SpriteRenderer>().bounds.size.x, gameObject.GetComponent<SpriteRenderer>().bounds.size.y) + mRadiusFromGameObject;
            float angleIncrement = (2.0f * Mathf.PI)/ mPartsNum;
            for (int part = 0; part < mPartsNum; ++part)
            {
                Vector3 offset = shielRadius * new Vector3(Mathf.Cos(angleIncrement * part), Mathf.Sin(angleIncrement * part), 0.0f);
                GameObject shieldPart = Instantiate(mPrefab, gameObject.transform.position + offset, Quaternion.identity);
                shieldPart.transform.parent = gameObject.transform;
            }
        }
    }

    private bool CanGenerateShield()
    {
        return Input.GetKeyDown(KeyCode.Y);
    }
}
