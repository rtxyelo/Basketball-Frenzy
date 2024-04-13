using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private MoneyCounter _moneyCounter;

    [SerializeField]
    private GameObject _coin;

    [SerializeField]
    private ScoreCounter _scoreCounter;

    [SerializeField]
    private PlayGameSounds _playGameSounds;

    private float _gravity = -18;

    private float _h;

    private float _offset;

    private float _heightThreshold = 5;

    private float _offsetThreshold = 10;

    public float Height { get => _h; set => _h = value; }
    public float Offset { get => _offset; set => _offset = value; }

    private bool _isCanThrow = true;

    public bool IsCanThrow { get => _isCanThrow; set => _isCanThrow = value; }

    [SerializeField]
    private List<Sprite> _ballSprites = new(5);

    [SerializeField]
    private SpriteRenderer _ballImage;

    private readonly string _ballKey = "Ball";

    private Vector3 _ballInitPosition;

    private void Awake()
    {
        _ballInitPosition = transform.position;

        if (!PlayerPrefs.HasKey(_ballKey))
            PlayerPrefs.SetInt(_ballKey, 1);

        SetBallImage();

        RandomHeightAndOffset();
    }

    private void Start()
    {
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        if (_h > _heightThreshold)
            _h = _heightThreshold;
        else if (_h < 2.5f)
            _h = 2.5f;

        if (_offset > _offsetThreshold)
            _offset = _offsetThreshold;
        else if (_offset < -3)
            _offset = -3;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBall();
        }

        if (_isCanThrow)
            DrawPath();
        else
            RemovePath();
    }

    private void RandomHeightAndOffset()
    {
        _h = Random.Range(2.6f, 6.5f);
        _offset = Random.Range(0.1f, 3.5f);
    }

    public void ThrowBall()
    {
        Physics2D.gravity = Vector2.up * _gravity;
        _rb.gravityScale = 1;
        _rb.velocity = CalculateLaunchData(_offset).initialVelocity;
        _isCanThrow = false;
    }

    LaunchData CalculateLaunchData(float offset)
    {
        float displacementY = _target.position.y - transform.position.y;
        
        Vector2 displacementX = new Vector2(_target.position.x - transform.position.x + offset, 0);

        float time = Mathf.Sqrt(-2 * _h / _gravity) + Mathf.Sqrt(2 * (displacementY - _h) / _gravity);

        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * _gravity * _h);

        Vector2 velocityX = displacementX / time;

        return new LaunchData(velocityX + velocityY * -Mathf.Sign(_gravity), time);
    }

    public readonly struct LaunchData
    {
        public readonly Vector2 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector2 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    private void DrawPath()
    {
        if (!_lineRenderer.enabled)
            _lineRenderer.enabled = true;

        LaunchData launchData = CalculateLaunchData(_offset);
        //Vector3 previousDrawPoint = transform.position;

        int resolution = 30;

        _lineRenderer.positionCount = resolution;

        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            
            Vector2 displacement = launchData.initialVelocity * simulationTime + Vector2.up * _gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = transform.position + (Vector3)displacement;

            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.red);

            _lineRenderer.SetPosition(i - 1, drawPoint);

            //previousDrawPoint = drawPoint;
        }
    }

    private void SetBallImage()
    {
        int ballIndex = PlayerPrefs.GetInt(_ballKey) - 1;
        _ballImage.sprite = _ballSprites[ballIndex];
    }

    private void RemovePath()
    {
        _lineRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Coin")
            {
                _coin.SetActive(false);
                _playGameSounds.PlayCoinSound();
                _moneyCounter.IncreaseMoney();

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Respawn")
            {
                RestartGame();
            }
            else if (collision.gameObject.tag == "Ring")
            {
                _scoreCounter.IncreaseScore();
                RestartGame();
            }
        }
    }

    private void RestartGame()
    {
        RandomHeightAndOffset();

        _coin.SetActive(true);

        _rb.gravityScale = 0;

        _rb.velocity = Vector2.zero;

        _rb.angularVelocity = 0;

        transform.rotation = Quaternion.identity;

        _isCanThrow = true;

        transform.position = _ballInitPosition;
    }


}
