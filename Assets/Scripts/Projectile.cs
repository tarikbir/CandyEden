using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage;
    public Vector3 Direction;

    void Update()
    {
        transform.Translate(Direction * Time.deltaTime);

        if (transform.position.z < -10)
        {
            PlayerControl.Instance.AddHealth(-Damage);
            Destroy(gameObject);
        }
    }
}
