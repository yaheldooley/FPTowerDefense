using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBehavior : MonoBehaviour
{
	[SerializeField] LayerMask layerMask;
	Controls controls;
	public bool isBaseBuilding = true;

	private GameObject currentBlueprint;
	private GameObject lastUpgrade;

	private void Awake()
	{
		controls = new Controls();
	}

	private void OnEnable()
	{
		controls.Enable();
	}
	private void OnDisable()
	{
		controls.Disable();
	}

	public void UpdateBehavior(Transform origin)
	{
		if (isBaseBuilding)
		{
			RaycastHit hit;
			Ray ray = new Ray(origin.position, origin.forward);

			if (Physics.Raycast(ray, out hit, 200f))
			{
				//hit.collider.gameObject.CompareTag("Ground")
			}
		}
		else
		{
			RaycastHit hit;
			Ray ray = new Ray(origin.position, origin.forward);

			if (Physics.Raycast(ray, out hit, 200f, layerMask))
			{
				//highlight blueprint
			}
			else
			{
				lastUpgrade = null;
				//show message that tells player to point at blueprint that they wish to upgrade
			}
		}
	}


}