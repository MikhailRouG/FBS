using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthPoint;

    public void TakeDamega(float damage)
    {
        healthPoint -= damage;
        Debug.Log(healthPoint);
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
