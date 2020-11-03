using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float damageCooldownTime = 1f;
    bool isDamageable = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDamageable) return;

        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit==null) return;
        isDamageable = false;
        StartCoroutine(DamageCooldown());
        hit.TakeDamage();

    }
    private IEnumerator DamageCooldown()
    {
        
        yield return new WaitForSeconds(damageCooldownTime);
        isDamageable = true;
    }
}
