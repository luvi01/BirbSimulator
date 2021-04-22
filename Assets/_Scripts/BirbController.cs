using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbController : SteerableBehaviour, IShooter, IDamageable
{
    private int lifes;
    Animator animator;
    public GameObject bullet;
    public Transform arma01;
    public float shootDelay = 1.0f;
    public AudioClip shootSFX;
    private GameManager gm;
    private float gravity;



    private float _lastShootTimestamp = 0.0f;


    public void Start()
    {
        gm = GameManager.GetInstance();
        gravity = rb.gravityScale;
        animator = GetComponent<Animator>();
        gm.vidas = 1;
    }

    public void Shoot()
    {
        if (Time.time - _lastShootTimestamp < shootDelay) return;
        AudioManager.PlaySFX(shootSFX);

        _lastShootTimestamp = Time.time;
        Instantiate(bullet, arma01.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        gm.vidas -= damage;
        if (gm.vidas <= 0) Die();
    }

    public void Die()
    {
        gm.ChangeState(GameManager.GameState.ENDGAME);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        
        if (gm.gameState != GameManager.GameState.GAME &
             gm.gameState != GameManager.GameState.RESUME)
        {
            rb.gravityScale = 0.0f;
            rb.velocity = Vector2.up * 0.0f;
            return;
        }
        animator.SetFloat("Status", 1.0f);


        rb.gravityScale = gravity;

        if (Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }

        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");
        //Thrust(xInput, yInput);

        if (yInput != 0 || xInput != 0)
        {
            Shoot();

        }

        if (Input.GetAxisRaw("Jump") != 0)
        {
            animator.SetFloat("Velocity", 1.0f);

            GoUp();
        }
        else
        {
            animator.SetFloat("Velocity", -1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Tube"))
        {
            //Destroy(collision.gameObject);
            TakeDamage(1);
        }
    }



}
