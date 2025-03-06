using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour, Buildable
{
    
    [SerializeField]
    GameObject regionTerrain;

    public string BuildingProductionDescription()
    {
        return "Unlocks new land";
    }

    public void OnBuildingDestroyed()
    {
        
    }

    private void Start()
    {
        Camera.main.GetComponent<CameraMovement>().UnlockNewChunk(regionTerrain.transform.position);
    }
}
