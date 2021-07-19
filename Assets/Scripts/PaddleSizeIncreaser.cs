using UnityEngine;

public class PaddleSizeIncreaser : BaseStatsChanger
{
    private Vector3 _defaultScale;
    private Transform _paddleTransform;

    private void Awake()
    {
        _paddleTransform = FindObjectOfType<PaddleMovement>().transform;
    }

    protected override void ActivateChanges()
    {
        var localScale = _defaultScale = _paddleTransform.localScale ;
        localScale = new Vector3(localScale.x * 2,
            localScale.y);
        _paddleTransform.localScale = localScale;
    }

    protected override void ResetStats()
    {
        _paddleTransform.localScale = _defaultScale;
    }
}
