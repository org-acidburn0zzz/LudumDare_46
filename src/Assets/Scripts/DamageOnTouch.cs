using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] float damage = 0.1f;
            public float GetDamage()
        {
            return damage;
        }
}
