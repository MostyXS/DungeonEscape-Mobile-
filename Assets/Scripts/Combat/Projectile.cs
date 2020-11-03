using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 5f);
    }


    private void Update()
    {
        transform.Translate(Vector2.right*3f*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        other.GetComponent<IDamageable>().TakeDamage();
        Destroy(gameObject);

    }


}
