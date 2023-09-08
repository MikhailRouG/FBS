using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float _mouthSensetiveX;
    [SerializeField] private float _mouthSensetiveY;

    private void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void Rotate(Vector2 sensitivity)
    {
        if (Time.time < 1) return;
        transform.eulerAngles += new Vector3(-sensitivity.y * _mouthSensetiveY, sensitivity.x * _mouthSensetiveX, 0f);
    }
}
