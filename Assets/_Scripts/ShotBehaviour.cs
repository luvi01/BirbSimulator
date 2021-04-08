using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : SteerableBehaviour
{
    GameManager gm;

    public void Start()
    {
        gm = GameManager.GetInstance();
    }

    private void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME &
             gm.gameState != GameManager.GameState.RESUME) return;

        Thrust(1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if (!(damageable is null))
        {
            damageable.TakeDamage();
        }
        Destroy(gameObject);
    }
}
