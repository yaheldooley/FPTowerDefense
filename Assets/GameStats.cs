using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI goldText;
    [SerializeField] int startingGold = 600;
    private int currentGold = 0;

	private void Start()
	{
		currentGold = startingGold;
		UpdateGoldText();
	}

	public void AddGold(int amount)
	{
		currentGold += amount;
	}

	public bool SpendGold(int amount)
	{
		if (currentGold <= amount)
		{
			currentGold -= amount;
			UpdateGoldText();
			return true;
		}
		else return false;
	}

	private void UpdateGoldText()
	{
		goldText.text = $"Purse: {currentGold}g";
	}
}
