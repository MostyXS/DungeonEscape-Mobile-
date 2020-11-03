using Platformer.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    int amount = 1;

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    Player player;
    private void Awake()
    {
         player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        player.AddOrRemoveGems(amount);
        Destroy(gameObject);
    }



}
