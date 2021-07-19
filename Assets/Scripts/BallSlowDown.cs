public class BallSlowDown : BaseStatsChanger
{
    private BallMovement _ballMovement;

    private void Awake()
    {
        _ballMovement = GetComponent<BallMovement>();
    }

    protected override void ActivateChanges()
    {
        _ballMovement.SetSpeed(SpeedModes.Half);
    }

    protected override void ResetStats()
    {
        _ballMovement.SetSpeed(SpeedModes.Default);
    }
}
