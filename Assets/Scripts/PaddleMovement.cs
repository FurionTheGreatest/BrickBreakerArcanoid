using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private float _zDistance, _leftCorner, _rightCorner;
    [SerializeField] private float _paddleSpeed = 10f;

    private void Start()
    {
        _zDistance = transform.position.z - Camera.main.transform.position.z;
        var sprite = GetComponent<SpriteRenderer>().sprite;
        _leftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, _zDistance)).x + sprite.bounds.size.x / 2;
        _rightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, _zDistance)).x - sprite.bounds.size.x / 2;
    }

    public void Move(float direction)
    {
        transform.Translate(new Vector3(direction * _paddleSpeed * Time.deltaTime, 0, 0));
    }

    private void ClampXPosition()
    {
        var paddlePosition = gameObject.transform.position;
        paddlePosition.x = Mathf.Clamp(paddlePosition.x, _leftCorner, _rightCorner);
        transform.position = paddlePosition;
    }

    private void Update()
    {
        ClampXPosition();
    }
}