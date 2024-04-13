using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private BallLauncher _ballLauncher;

    [SerializeField]
    private InputController _inputController;

    public int GameTime { get => (int)_gameSessionTime; }

    private float _gameSessionTime = 60f;

    private void Update()
    {
        _gameSessionTime -= Time.deltaTime;

        if (_gameSessionTime < 0)
        {
            //Debug.Log("Game over!");

            _ballLauncher.IsCanThrow = false;

            _inputController.IsActive = false;

            _gameOverPanel.SetActive(true);
        }
    }
}