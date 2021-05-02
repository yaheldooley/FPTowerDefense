using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FPTowerDefense.Core;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
	
	public void UpdateGoldText()
    {
		goldText.text = $"Purse: {LevelSettings.currentGold}g";
	}
}
