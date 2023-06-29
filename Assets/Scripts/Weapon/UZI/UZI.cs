using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UZI : Weapon
{
    public override void Shoot()
    {
        if (Delay >= FireRate)
        {
            _audio.Play();
            Animator.SetTrigger(Shot);
            ScatterAngle = _shootPoint.eulerAngles;
            ScatterAngle.z += Random.Range(-Scatter, Scatter);

            Instantiate(Bullet, _shootPoint.position, Quaternion.Euler(ScatterAngle));
            Delay = 0;
        }
    }
}
