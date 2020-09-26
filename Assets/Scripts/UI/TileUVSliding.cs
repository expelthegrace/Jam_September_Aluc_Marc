using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileUVSliding : MonoBehaviour
{
    private Material mMaterial;
    private Vector2 mOffsetTexture = new Vector2(0, 0);
    [SerializeField] Vector2 mTextureVelocity = new Vector2(0.1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        mMaterial = gameObject.GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
        mMaterial.SetTextureOffset("_MainTex", mOffsetTexture);
        mOffsetTexture += mTextureVelocity;
        if (mOffsetTexture.x == 1) mOffsetTexture = new Vector2(0, mOffsetTexture.y);
        if (mOffsetTexture.y == 1) mOffsetTexture = new Vector2(mOffsetTexture.x, 0);

    }
}
