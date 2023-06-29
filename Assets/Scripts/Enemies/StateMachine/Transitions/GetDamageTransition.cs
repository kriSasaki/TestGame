using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamageTransition : Transition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out SimpleBullet bullet))
        {
            NeedTransit = true;
        }
    }
}
