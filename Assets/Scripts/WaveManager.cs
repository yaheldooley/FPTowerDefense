using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    bool active = true;

	[Header("Wave Data")]
	[SerializeField] int[] lengthOfEachWave;
    [SerializeField] Transform target;

	[Header("UI")]
	[SerializeField] TextMeshProUGUI secondsText;
	[SerializeField] TextMeshProUGUI waveNumberText;

	private AllWavesList[] allWavesLists;

	private int totalWaves;
	private int currentWave = 0;
    private int secondsIntoWave = 0;

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
        while(active)
		{
			secondsText.text = $"Seconds: {secondsIntoWave}";
			waveNumberText.text = $"WAVE: {currentWave}/{totalWaves}";
			SpawnFromAllWavesList();
			secondsIntoWave++;
			CheckWaveStatus(); 
			yield return new WaitForSeconds(1);

		}
		secondsText.text = "All enemies spawned";
		yield return null;
	}
	private void SpawnFromAllWavesList()
	{
		foreach (AllWavesList list in allWavesLists)
		{
			list.SpawnFromThisWavesWaveOrders(secondsIntoWave, target, currentWave);
		}
	}

	private void CheckWaveStatus()
	{
		if (currentWave == lengthOfEachWave.Length) active = false;

		else if (secondsIntoWave == lengthOfEachWave[currentWave]) BeginNextWave();
		
	}

	

	public void BeginNextWave()
	{
		if (currentWave == totalWaves) LevelComplete();
		else
		{
			//play juicy sounds and graphics

			currentWave++;
			secondsIntoWave = 0;

			//give players a short build period
		}
	}

	private void LevelComplete()
	{
		Debug.Log("Level complete!");
	}
}
