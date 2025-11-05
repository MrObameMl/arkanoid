using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static event Action OnAttemptsEnded;
    public TMP_Text PlayerAttemptsText;
    public TMP_Text BallsLeftText;

    private float _playerAttempts = 3;
    private int _blocksCount;

    private void Awake()
    {
        PlayerAttemptsText.text = $"Attemps: {_playerAttempts}";
        UpdateBlocksCount(_blocksCount);
    }

    private void UpdateAttempts()
    {
        int registredAttempt = 1;

        if (_playerAttempts == registredAttempt)
        {
            OnAttemptsEnded?.Invoke();
            _playerAttempts--;
            PlayerAttemptsText.text = $"Attemps: {_playerAttempts}";
            return;
        }
        _playerAttempts--;
        PlayerAttemptsText.text = $"Attemps: {_playerAttempts}";
    }

    private void UpdateBlocksCount(int value)
    {
        _blocksCount = value;
        BallsLeftText.text = $"Blocks Left: {_blocksCount}";
    }

    private void OnEnable()
    {
        Ball.OnBallFalledDown += UpdateAttempts;
        GameManager.OnBlocksCountUpdated += UpdateBlocksCount;
    }

    private void OnDisable()
    {
        Ball.OnBallFalledDown -= UpdateAttempts;
        GameManager.OnBlocksCountUpdated -= UpdateBlocksCount;
    }
}
