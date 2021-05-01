using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Player Behaviors")]
	[SerializeField] PlayerMovement movement;

	[Header("UI")]
	[SerializeField] GameObject buildPanel;

	[Header("Camera")]
	[SerializeField] GameObject playerCamera;


	bool fullScreen = false;
	bool buildMode = false;

	Controls controls;
	private void Awake()
	{
		controls = new Controls();
		controls.Player.ToggleBuild.started += ctx => ToggleBuild();
		ToggleFullscreen();
	}

	private void OnEnable()
	{
		controls.Enable();
	}
	private void OnDisable()
	{
		controls.Disable();
	}

	private void ToggleBuild()
	{
		buildMode = !buildMode;
		buildPanel.SetActive(buildMode);
		movement.enabled = !buildMode;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) ToggleFullscreen();
	}

	private void ToggleFullscreen()
	{
		fullScreen = !fullScreen;
		Screen.fullScreen = fullScreen;

		if (fullScreen) Cursor.lockState = CursorLockMode.Locked;
		else Cursor.lockState = CursorLockMode.None;
	}

}
