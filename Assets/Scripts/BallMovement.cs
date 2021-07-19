using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float _ballSpeed = 10f;
    [SerializeField] private Transform _ballSpawnPosition;
    private Rigidbody2D _ballRigidbody2D;
    public bool IsBallThrowed { get; private set;}
    private void Start()
    {
        _ballRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void ThrowBall()
    {
        AddForce(SetRandomStartDirection());
        IsBallThrowed = true;
    }
    
    private void RemoveVelocity() => _ballRigidbody2D.velocity = Vector2.zero;
    private void AddForce(Vector2 force) => _ballRigidbody2D.AddForce(force * _ballSpeed, ForceMode2D.Impulse);

    private Vector2 SetRandomStartDirection() => new Vector2(Random.Range(1,2), Random.Range(1,2));

    public void SetupBallBeforeThrow()
    {
        gameObject.transform.position = _ballSpawnPosition.position;
        RemoveVelocity();
        IsBallThrowed = false;
        gameObject.SetActive(true);
    }

    public void SetSpeed(SpeedModes newMode)
    {
        switch (newMode)
        {
            case SpeedModes.Default:
                _ballRigidbody2D.velocity *= 2;
                break;
            case SpeedModes.Half:
                var velocity = _ballRigidbody2D.velocity;
                velocity /= 2;
                _ballRigidbody2D.velocity = velocity;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newMode), newMode, "There is no such speed mode");
        }
    }
}

public enum SpeedModes
{
    Default,
    Half
}
