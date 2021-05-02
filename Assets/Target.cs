using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	[SerializeField] WaveManager waveManager;
	bool gameOver = false;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy") && !gameOver)
		{
			LevelSettings.lives--;
			other.gameObject.GetComponent<EnemyHealth>().Dead(false);
			if (LevelSettings.lives <= 0)
			{
				gameOver = true;
				waveManager.LevelFailed();
			}
		}
	}
}
