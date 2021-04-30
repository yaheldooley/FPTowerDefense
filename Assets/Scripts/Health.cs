using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int HP = 5;
    [SerializeField] int maxHP = 5;
    public bool isAlive = true;
    public bool isEnemy = false;

    public void Damage(int damage)
    {
        if (!isAlive) return;
        HP = HP - damage;
        if (HP <= 0) Dead();
	}

	private void Dead()
	{
        isAlive = false;
        
        Destroy(gameObject);
	}

	private void OnParticleCollision(GameObject other)
	{
        Damage(1);
	}
	
}
