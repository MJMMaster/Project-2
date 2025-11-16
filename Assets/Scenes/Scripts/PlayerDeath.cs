using UnityEngine;

public class PlayerDeath : Death
{
    public override void Die()
    {
        GameManager.Instance.PlayerDied();
    }
}