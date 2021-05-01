using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWavesList : MonoBehaviour
{
    WaveOrder[] waveOrders;
    WaveManager waveManager;

	private void Awake()
	{
        waveManager = FindObjectOfType<WaveManager>();
	}

	public void InitializeAllWavesOrdersLists()
    {
        waveOrders = GetComponentsInChildren<WaveOrder>();
    }
    public void SpawnFromThisWavesWaveOrders(int second, Transform target, int waveNumber)
    {
        if(waveNumber < waveOrders.Length) waveOrders[waveNumber].SpawnWave(second, target);

    }
}
