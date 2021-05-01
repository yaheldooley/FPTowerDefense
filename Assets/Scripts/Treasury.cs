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
		[SerializeField] TextMeshProUGUI goldText;
		[SerializeField] int startingGold = 600;
		public int currentGold = 0;

		[SerializeField] Transform blueprintsRoot;

		private void Start()
		{
			currentGold = startingGold;
			UpdateGoldText();
			GiveRefToBlueprints();
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

		public void UpdateGoldText()
		{
			goldText.text = $"Purse: {currentGold}g";
		}

		private void GiveRefToBlueprints()
		{
			UpgradeBlueprint[] blueprints = blueprintsRoot.GetComponentsInChildren<UpgradeBlueprint>();
			foreach (UpgradeBlueprint blue in blueprints)
			{
				blue.GiveGameStatsReference(this);
			}
		}
	}
}
