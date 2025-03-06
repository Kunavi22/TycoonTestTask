using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBuild : MonoBehaviour, Buildable
{
    [SerializeField]
    string buildingDescription;
    public string BuildingProductionDescription()
    {
        return buildingDescription;
    }

    public void OnBuildingDestroyed()
    {
    }
}
