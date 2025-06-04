using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Settings")]
    public Transform player;
    public float speed = 2f;
    public float chaseRange = 15f;

    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            anim.SetInteger("walk", 0); // idle
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Tentukan arah untuk animasi
        if (direction.x > 0.01f)
        {
            sprite.flipX = false;
            anim.SetInteger("walk", 1); // ke kanan
        }
        else if (direction.x < -0.01f)
        {
            sprite.flipX = true;
            anim.SetInteger("walk", -1); // ke kiri
        }
        else
        {
            anim.SetInteger("walk", 0); // idle
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Enemy menyentuh player! Mengurangi nyawa.");
                playerHealth.TakeDamage(10); // kurangi nyawa
            }
        }
    }
}
