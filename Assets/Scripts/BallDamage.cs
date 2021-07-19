using UnityEngine;

public class BallDamage : BaseStatsChanger
{
    private bool _isInstantDeath;

    public bool IsInstantDeath => _isInstantDeath;
    
    protected override void ActivateChanges()
    {
        _isInstantDeath = true;
    }

    protected override void ResetStats()
    {
        _isInstantDeath = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var breakable = other.gameObject.GetComponent<BreakableBrick>();
        if (breakable!= null)
        {
            AudioManager.instance.PlayBrickSound(breakable.brickType);
        }
        else
        {
            AudioManager.instance.PlayUnbreakable();
        }
    }
}
