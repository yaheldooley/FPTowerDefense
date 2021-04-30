using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
	[SerializeField] Transform camera;
	[SerializeField] Transform playerTransform;
	[SerializeField] float mouseSensitivity = 50;
	[SerializeField] float minimumY = -20;
	[SerializeField] float maximumY = 30;
	float rotationY;
	Quaternion originalRotation;
	Controls controls;
	
	private void Awake()
	{
		controls = new Controls();
	}
	private void Update()
	{
		Look(controls.Player.Look.ReadValue<Vector2>());
	}
	private void Look(Vector2 lookAxis)
	{
		playerTransform.Rotate(0f, lookAxis.x * mouseSensitivity * Time.deltaTime, 0f);
		rotationY += lookAxis.y * mouseSensitivity * Time.deltaTime;


		rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

		Quaternion localRotation = Quaternion.Euler(0, rotationY, 0.0f);
		transform.rotation = localRotation;

	}
}
