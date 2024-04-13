using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadBallsAnimation : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
