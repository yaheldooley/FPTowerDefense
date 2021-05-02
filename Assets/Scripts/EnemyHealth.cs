using FPTowerDefense.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int HP = 5;
    [SerializeField] int maxHP = 5;
    public bool isAlive = true;
    public bool isEnemy = false;
	[SerializeField] int reward;

	private void OnEnable()
	{
		EnemyTracker.currentEnemies++;
	}
	public void Damage(int damage)
    {
        if (!isAlive) return;
        HP = HP - damage;
        if (HP <= 0) Dead();
	}

	private void Dead()
	{
        isAlive = false;
		EnemyTracker.currentEnemies--;
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<Treasury>().AddGold(reward);
        Destroy(gameObject);
	}

	private void OnParticleCollision(GameObject other)
	{
        Damage(1);
	}
}
