using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Player Behaviors")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] Build build;

	[Header("UI")]
	[SerializeField] GameObject buildPanel;

	Controls controls;
	bool fullScreen = false;
	bool buildMode = false;

	private void Awake()
	{
		controls = new Controls();
		controls.Player.ToggleBuild.started += ctx => ToggleBuild();
	}

	private void ToggleBuild()
	{
		buildMode = !buildMode;
		buildPanel.SetActive(buildMode);
		build.enabled = buildMode;
		movement.enabled = !buildMode;
	}

	private void OnEnable()
	{
		controls.Enable();
	}
	private void OnDisable()
	{
		controls.Disable();
	}

	void Start()
	{
		ToggleFullscreen();
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
