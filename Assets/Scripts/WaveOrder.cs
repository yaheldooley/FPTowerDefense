using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AllWavesList))]
public class WaveOrder : MonoBehaviour
{
    [SerializeField] List<SpawnGroup> spawnGroups = new List<SpawnGroup>();

	private void Awake()
	{
		spawnGroups.Sort((x, y) => x.secondsIntoWave.CompareTo(y.secondsIntoWave));
	}

	public void SpawnWave(int second, Transform target)
	{
		if (spawnGroups.Count < 1) return;

		if(spawnGroups[0].secondsIntoWave == second)
		{
			for (int i = 0; i < spawnGroups[0].spawnCount; i++)
			{
				GameObject newEnemy = Instantiate(spawnGroups[0].enemyPrefab);
				newEnemy.transform.position = transform.position;
				newEnemy.GetComponent<Health>().isEnemy = true;
				newEnemy.GetComponent<EnemyMovement>().EnableNavMesh(target.position);
			}
			spawnGroups.RemoveAt(0);
		}
	}
}

[System.Serializable]
public class SpawnGroup
{
    public GameObject enemyPrefab = null;
    public int spawnCount = 1;
	public int secondsIntoWave = 0;
}
