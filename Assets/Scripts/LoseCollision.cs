using System;
using UnityEngine;

public class LoseCollision : MonoBehaviour
{
    public Action OnBallLost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BallMovement>() == null) return;
        OnBallLost?.Invoke();
    }
}
