using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private BallLauncher _ballLauncher;

    [SerializeField]
    private TouchDetection _touchDetection;

    [SerializeField]
    private float _touchSpeed = 0.003f;

    [SerializeField]
    private List<TMP_Text> _deltaInputInfo = new(2);

    [SerializeField]
    private PlayGameSounds _playGameSounds;

    private bool _isActive = true;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    private void Update()
    {
        if (_isActive && _touchDetection.TouchDetected)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                DetectTouch();
            }

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Throw!");
                _playGameSounds.PlayBallSound();
                _ballLauncher.ThrowBall();
                _isActive = false;
            }
        }
    }

    private void DetectTouch()
    {
        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

        float offsetValue = -touchDeltaPosition.x * _touchSpeed;
        float heightValue = -touchDeltaPosition.y * _touchSpeed;

        if (Mathf.Abs(offsetValue) > 5)
        {
            offsetValue = Mathf.Sign(offsetValue) * 5;
        }

        if (Mathf.Abs(heightValue) > 5)
        {
            heightValue = Mathf.Sign(heightValue) * 5;
        }

        Debug.Log("offsetValue " + offsetValue);
        Debug.Log("heightValue " + heightValue);

        _deltaInputInfo[0].text = offsetValue.ToString();
        _deltaInputInfo[1].text = heightValue.ToString();

        _ballLauncher.Offset += offsetValue;
        _ballLauncher.Height += heightValue;
    }
}
