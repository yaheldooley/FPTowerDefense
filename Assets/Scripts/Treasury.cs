using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FPTowerDefense.Build;

namespace FPTowerDefense.Core
{
	public class Treasury : MonoBehaviour
	{
		[SerializeField] int startingGold = 150;

		[SerializeField] Transform blueprintsRoot;
		private UIManager uiManager;

		private void Start()
		{
			uiManager = FindObjectOfType<UIManager>();
			AddGold(startingGold);
		}

		public void AddGold(int amount)
		{
			LevelSettings.currentGold += amount;
			uiManager.UpdateGoldText();
		}
		public bool SpendGold(int amount)
		{
			if (LevelSettings.currentGold <= amount)
			{
				LevelSettings.currentGold -= amount;
				print($"Removed {amount}. Current gold is now {LevelSettings.currentGold}");
				uiManager.UpdateGoldText();
				return true;
			}
			else return false;
		}
	}
}
