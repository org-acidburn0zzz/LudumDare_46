using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] float swordDamage;

    public float GetDamage()
    {
        return swordDamage;
    }
}
