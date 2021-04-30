using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] NavMeshAgent navMesh;
	[SerializeField] Vector3 position;
	public Transform bullseye;

	public void EnableNavMesh(Vector3 _position)
	{
		navMesh.enabled = true;
		position = _position;
		navMesh.SetDestination(position);
	}

	private void Update()
	{
		if (!navMesh.hasPath) navMesh.SetDestination(position);
	}
}
