using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField] Projectile projectile = null;
    public Action onAttack;


    
    public override void Update()
    {
       
    }

    public override void Init()
    {
        base.Init();
        onAttack += Attack;
    }
    public void Attack()
    {
        Instantiate(projectile,transform.position, Quaternion.identity);
    }
   
    


}
