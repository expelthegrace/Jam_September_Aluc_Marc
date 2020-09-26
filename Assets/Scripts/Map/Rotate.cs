using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float mRotationSpeed = 10;
    [SerializeField] private float mRotationDirection = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        mRotationDirection = Mathf.Max(0, Mathf.Min(1, mRotationDirection));
        transform.Rotate(0, 0, mRotationSpeed * mRotationSpeed, Space.Self);
    }
}
