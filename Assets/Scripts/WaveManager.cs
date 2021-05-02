using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    bool gameInProgress = true;

	[Header("Wave Data")]
	[SerializeField] int[] lengthOfEachWave;
    [SerializeField] Transform target;
	[SerializeField] int breakBetweenWaves = 120;

	[Header("UI")]
	[SerializeField] TextMeshProUGUI secondsText;
	[SerializeField] TextMeshProUGUI waveNumberText;

	private AllWavesList[] allWavesLists;

	private int secondsIntoBreak = 0;

	private int totalWaves;
	private int currentWave = -1;
    private int secondsIntoWave = 0;


	private int enemiesSpawned = 0;
	private bool spawnsComplete = false;

	private GameState gameState = GameState.Start;

	private void Awake()
	{
		allWavesLists = GetComponentsInChildren<AllWavesList>();
	}

	private void Start()
	{
		foreach(AllWavesList list in allWavesLists) list.InitializeAllWavesOrdersLists();
		totalWaves = lengthOfEachWave.Length;
        StartCoroutine(ProgressThroughWaves());
	}

	private IEnumerator ProgressThroughWaves()
    {
        while(gameInProgress)
		{
			if (gameState == GameState.Start)	IncrementStart();
			if (gameState == GameState.Wave)	IncrementWave();
			if (gameState == GameState.Break)	IncrementBreak();
			yield return new WaitForSeconds(1);
		}
		LevelComplete();
		yield return null;
	}

	private void IncrementStart()
	{
		if (secondsIntoBreak < breakBetweenWaves)
		{
			secondsIntoBreak++;
			secondsText.text = $"Seconds until battle: {breakBetweenWaves - secondsIntoBreak}";
			waveNumberText.text = "Prepare!";
		}
		else
		{
			secondsIntoBreak = 0;
			BeginNextWave();
		}
	}

	private void BeginNextWave()
	{
		spawnsComplete = false;
		secondsIntoWave = 0;
		currentWave++;
		gameState = GameState.Wave;
	}
	private void IncrementWave()
	{
		if (!spawnsComplete)
		{
			SpawnFromAllWavesList();
			secondsText.text = $"Seconds: {secondsIntoWave}";
			waveNumberText.text = $"WAVE: {currentWave +1}/{totalWaves}";
			secondsIntoWave++;
			if (secondsIntoWave == lengthOfEachWave[currentWave]) spawnsComplete = true;
		}
		else if (EnemyTracker.currentEnemies == 0)
		{
			if (currentWave == totalWaves -1) gameInProgress = false; //Level Complete
			else
			{
				secondsIntoBreak = 0;
				gameState = GameState.Break;
			}
		}	
	}

	private void IncrementBreak()
	{
		if (secondsIntoBreak < breakBetweenWaves)
		{
			secondsIntoBreak++;
			secondsText.text = $"Seconds until next wave: {breakBetweenWaves - secondsIntoBreak}";
			waveNumberText.text = "Repair/Fortify!";
		}
		else
		{
			BeginNextWave();
		}
	}

	private void SpawnFromAllWavesList()
	{
		foreach (AllWavesList list in allWavesLists)
		{
			list.SpawnFromThisWavesWaveOrders(secondsIntoWave, target, currentWave);
		}
	}

	
	public void AddEnemy()
	{
		enemiesSpawned++;
		print(enemiesSpawned + " enemies in play");
	}
	public void RemoveEnemy()
	{
		enemiesSpawned--;
		print(enemiesSpawned + " enemies in play");
	}

	private void LevelComplete()
	{
		secondsText.text = "Level Complete!";
	}
}
