using UnityEngine;

public class BreakableBrick : BaseBrick
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var ballDamage = other.gameObject.GetComponent<BallDamage>();
        if(ballDamage == null) return;
        ReduceHealth(ballDamage.IsInstantDeath);
    }
}
