using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildPanel : MonoBehaviour
{
	[SerializeField] GameObject firstButton;
	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(firstButton);
	}
}
