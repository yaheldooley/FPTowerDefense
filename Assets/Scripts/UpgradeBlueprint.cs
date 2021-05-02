using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FPTowerDefense.Core;

namespace FPTowerDefense.Build
{
    public class UpgradeBlueprint : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] BuildData[] buildData;
        [SerializeField] Transform playersCam;
        [SerializeField] Transform blueprintRoot;
        Vector3 buildAngle = new Vector3(0, -.4f, 0);
        private GameObject currentBlueprintChosen;
        private Vector3 lastGroundPos = Vector3.zero;
        bool justBuilt = false;
        Treasury treasury;

		private void Awake()
		{
            if (treasury == null) treasury = FindObjectOfType<Treasury>();
		}


		void OnDisable()
        {
            if (currentBlueprintChosen != null)
            {
                Destroy(currentBlueprintChosen.gameObject);
                currentBlueprintChosen = null;
            }
        }
        
        public void BuildBaseBlueprint()
        {
            bool purchased = treasury.SpendGold(buildData[0].GetCost());
            if (purchased)
            {
                justBuilt = true;
                currentBlueprintChosen.gameObject.GetComponent<Blueprint>().IsPlaced = true;
                currentBlueprintChosen = null;
            }
        }

        public void UpgradeExisting(int currentLevel)
        {
            if (currentLevel < buildData.Length - 1)
            {
                if (buildData[currentLevel].GetCost() <= LevelSettings.currentGold)
                {
                    treasury.SpendGold(buildData[0].GetCost());
                }
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (currentBlueprintChosen != null) Destroy(currentBlueprintChosen.gameObject);
            GameObject newObject = Instantiate(buildData[0].GetPrefab(), blueprintRoot);
            currentBlueprintChosen = newObject;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (currentBlueprintChosen != null && !justBuilt) Destroy(currentBlueprintChosen.gameObject);
            justBuilt = false;
        }


        private Vector3 GetBlueprintPos()
        {
            RaycastHit hit;

            Ray ray = new Ray(playersCam.position, playersCam.forward + buildAngle);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Ground"))
            {
                lastGroundPos = hit.point;
                return hit.point;
            }

            return lastGroundPos;
        }
        private Quaternion GetBluePrintRot()
        {
            return Quaternion.identity;

        }
        private void Update()
        {
            if (currentBlueprintChosen != null)
            {
                currentBlueprintChosen.transform.position = GetBlueprintPos();
                currentBlueprintChosen.transform.rotation = GetBluePrintRot();
            }
        }
    }

    [System.Serializable]
    public class BuildData
    {
        [SerializeField] GameObject prefab;
        [SerializeField] string prefabName;
        [SerializeField] int cost;

        public GameObject GetPrefab()
        {
            return prefab;
        }
        public string GetName()
        {
            return prefabName;
        }
        public int GetCost()
        {
            return cost;
        }
    }
}