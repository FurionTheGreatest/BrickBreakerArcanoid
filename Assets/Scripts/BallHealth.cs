using System;
using UnityEngine;

public class BallHealth : MonoBehaviour
{
    public static Action OnLivesEnded;
    private int _lives = 3;
    private LoseCollision _lose;
    private BallMovement _ballMovement;

    private void Awake()
    {
        _ballMovement = GetComponent<BallMovement>();
        _lose = FindObjectOfType<LoseCollision>();
    }

    private void OnEnable()
    {
        _lose.OnBallLost += ReduceLife;
    }

    private void OnDisable()
    {
        _lose.OnBallLost -= ReduceLife;
    }

    private void ReduceLife()
    {
        _lives--;
        gameObject.SetActive(false);
        if (_lives <= 0)
        {
            OnLivesEnded?.Invoke();
        }
        else
        {
            _ballMovement.SetupBallBeforeThrow();
        }
    }
}