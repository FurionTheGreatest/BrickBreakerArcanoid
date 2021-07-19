using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PaddleMovement _paddleMovement;
    [SerializeField] private BallMovement _ballMovement;

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        _paddleMovement.Move(horizontal);
        if (Input.anyKey && !_ballMovement.IsBallThrowed)
        {
            _ballMovement.ThrowBall();
        }
    }
}