using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    

    //Animation Event
    private void Fire()
    {
        GetComponentInParent<Spider>().onAttack();
    }
    
    
    

}
