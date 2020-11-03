using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int health = 5;
    [SerializeField] Collectible collectiblePrefab = null;
    public int Health { get; set; }
    [SerializeField] int gems;

    [SerializeField] protected float speed, distanceToTrigger;
    [SerializeField] protected Transform pointA, pointB;

    protected bool isDead;

    protected Transform currentDestination, player;
    protected Animator anim;
    protected SpriteRenderer sprite;

    

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        Health = health;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentDestination = pointA;
    }
    public virtual void Update()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
        ProcessControl();
    }

    private void ProcessControl()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        if (AttackBehaviour()) return;
        SuspisionBehaviour();
    }

    private void SuspisionBehaviour()
    {
        FlipCheck();
        Movement();
    }

    private bool AttackBehaviour()
    {
        if (Vector2.Distance(player.position, transform.position) > distanceToTrigger)
        {
            anim.SetBool("InCombat", false);
            return false;
        }
        else if (anim.GetBool("InCombat"))
        {
            float direction = player.transform.position.x - transform.position.x;
            sprite.flipX = direction < 0;
            return true;
        }
        return false;
    }

    public virtual void Movement()
    {
        

        if (transform.position == pointA.position)
        {
            currentDestination = pointB;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentDestination = pointA;
            anim.SetTrigger("Idle");
        }
        transform.position = Vector2.MoveTowards(transform.position, currentDestination.position, speed * Time.deltaTime);
    }

    protected virtual void FlipCheck()
    {
        if (currentDestination == pointA)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    public virtual void TakeDamage()
    {
        if (isDead) return;
        anim.SetTrigger("Hit");
        Health -= 1;
        anim.SetBool("InCombat", true);
        if (Health <= 0)
        {
            isDead = true;
            var gem = Instantiate(collectiblePrefab, transform.position, Quaternion.identity);
            gem.Amount = gems;
            anim.SetTrigger("Death");
            
            enabled = false;
        }
    }

    
}
