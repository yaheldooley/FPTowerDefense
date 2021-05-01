using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
	[SerializeField] Blueprint blueprint;
	[SerializeField] Transform turretPost;
	[SerializeField] Transform turretGun;
	[SerializeField] ParticleSystem particles;
	public float RotationSpeed = 20;

	//values for internal use
	private Quaternion _lookRotation = Quaternion.identity;
	private Vector3 _direction = Vector3.zero;

	Transform target;
	List<EnemyHealth> enemies = new List<EnemyHealth>();

	bool armed = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			enemies.Add(other.gameObject.GetComponent<EnemyHealth>());
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			enemies.Remove(other.gameObject.GetComponent<EnemyHealth>());	
		}
	}
	private void Update()
	{
		if (blueprint.IsPlaced)
		{
			var emission = particles.emission;
			if (emission.enabled != armed) emission.enabled = armed;

			armed = HasTarget();
			if (armed) TrackTarget();
		}
	}

	private bool HasTarget()
	{
		if (enemies.Count < 1) return false;
		else if (target == null) return GetTarget();
		else return true;
	}

	private bool GetTarget()
	{
		ConfirmTargetsValid();
		return CanAssignTarget();
	}
	private void ConfirmTargetsValid()
	{
		List<EnemyHealth> validTargets = new List<EnemyHealth>(enemies);

		foreach (EnemyHealth enemy in enemies)
		{
			if (enemy == null) validTargets.Remove(enemy);
		}
		enemies.Clear();
		enemies.AddRange(validTargets);
	}

	private bool CanAssignTarget()
	{
		if (enemies.Count > 0)
		{
			target = enemies[0].GetComponent<EnemyMovement>().bullseye;
			return true;
		}
		else return false;
	}

	private void TrackTarget()
	{
		//find the vector pointing from our position to the target
		_direction = (target.transform.position - turretGun.position).normalized;

		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		turretGun.transform.localRotation = Quaternion.Slerp(turretGun.transform.localRotation, _lookRotation, Time.deltaTime * RotationSpeed);
	}
}
