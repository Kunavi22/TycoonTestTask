using System;
using UnityEngine;
using UnityEngine.Events;




public class BuildingSpot : MonoBehaviour, Clickable
{

    private enum BuilderMode
    {
        CreatePrefabMode,
        UnhideObjectMode
    }

    [Serializable]
    private class UpgradeLevel
    {
        [SerializeField]
        public string buildingName;

        [SerializeField]
        public SerializableDict<ResourseType, int> cost;

        [SerializeField]
        public BuilderMode upgradeMode;

        [SerializeField]
        public GameObject prefabToBuild;

        [SerializeField]
        public UnityEvent upgradeAdditionalEvents;
    }

    Material renderMaterial;

    [SerializeField]
    UpgradeLevel[] upgrades;

    [SerializeField]
    private bool HideAfterFullUpgrade = false;
    public int buildingLevel { private set; get; } = -1;


    private GameObject currentBuild;


    [SerializeField]
    public int ID;

    public void OnValidate()
    {
        //prefab detection
        if (gameObject.scene.name == null) return;


        SaveManager saveManager = FindObjectOfType<SaveManager>();

        if (!saveManager.buildingSpots.Contains(this)) 
            saveManager.buildingSpots.Add(this);

        saveManager.OnValidate();
    }

    void Start()
    {
        //Load saves
        if(SaveManager.loadedGameState.buildingSpotsLevel.Count > ID)
        {
            LoadUpgrade(SaveManager.loadedGameState.buildingSpotsLevel[ID]);
        }
           

        renderMaterial = gameObject.GetComponent<Renderer>().material;             
    }

    public void OnClick()
    {
        if (buildingLevel + 1 == upgrades.Length) return;

        if (MaterialManager.TryToRemoveResourses(upgrades[buildingLevel + 1].cost.ToDictionary()))
        {
            //TODO: creation particle effect

            UpgradeBuilding(buildingLevel + 1);

            ToolTip.instance.Text = GenerateToolTipText();


            SaveManager.instance.SaveGameState();

        }
    }

    private void LoadUpgrade(int toUpgradeID)
    {
        if (toUpgradeID == -1) return;

        for(int i = 0; i < toUpgradeID; i++)
        {
            upgrades[i].upgradeAdditionalEvents.Invoke();

            if (upgrades[i].upgradeMode == BuilderMode.UnhideObjectMode)
            {
                upgrades[i].prefabToBuild.SetActive(true);
            }
        }

        UpgradeBuilding(toUpgradeID);

    }

    private void UpgradeBuilding(int upgradeID)
    {
        switch(upgrades[upgradeID].upgradeMode)
        {
            case BuilderMode.CreatePrefabMode:

                if (buildingLevel != -1)
                {
                    currentBuild.GetComponent<Buildable>().OnBuildingDestroyed();

                    Destroy(currentBuild);
                }

                currentBuild = Instantiate(upgrades[upgradeID].prefabToBuild, transform);
                currentBuild.transform.localScale = new Vector3(
                    currentBuild.transform.localScale.x / transform.localScale.x,
                    currentBuild.transform.localScale.y / transform.localScale.y,
                    currentBuild.transform.localScale.z / transform.localScale.z);

                break;
            case BuilderMode.UnhideObjectMode:

                upgrades[upgradeID].prefabToBuild.SetActive(true);

                break;
        }

        upgrades[upgradeID].upgradeAdditionalEvents.Invoke();

        if(upgradeID + 1 == upgrades.Length)
        {
            GetComponent<MeshRenderer>().enabled = false;

            if (HideAfterFullUpgrade)
            {
                GetComponent<BoxCollider>().enabled = false;
                
                this.enabled = false;
            }          
        }

    
        buildingLevel = upgradeID;
    }

    public void OnHover()
    {
        renderMaterial.color = Color.green;

        ToolTip.instance.Show(GenerateToolTipText());
    }

    public void OnUnHover()
    {
        renderMaterial.color = Color.yellow;
        ToolTip.instance.Hide();
    }

    private string GenerateToolTipText()
    {
        string tooltipText = "";
        if (buildingLevel == -1)
        {            
            tooltipText = "<b>" + upgrades[0].buildingName + "</b>\r\n" +
             "Cost: ";
        }
        else
        {
            tooltipText = "<b>" + upgrades[buildingLevel].buildingName + "</b>\r\n" +
                           upgrades[buildingLevel].prefabToBuild.GetComponent<Buildable>().BuildingProductionDescription() + "\n";

            if (buildingLevel + 1 < upgrades.Length) tooltipText += "----Upgrade----\n" +
                                                                    "Cost: ";
        }

        //if can be upgraded
        if(buildingLevel + 1 < upgrades.Length) 
        {
            //include upgrade cost to a tooltip
            foreach (var item in upgrades[buildingLevel + 1].cost)
            {

                if (MaterialManager.materialAmount[(int)item.key] < (uint)item.value)
                {
                    tooltipText += "<color=red>" + (int)item.value + "</color><sprite=" + (int)item.key + "> ";
                }
                else
                {
                    tooltipText += item.value + "<sprite=" + (int)item.key + "> ";
                }
            }

            tooltipText += "\n" + upgrades[buildingLevel + 1].prefabToBuild.GetComponent<Buildable>().BuildingProductionDescription();

            if(buildingLevel == -1) tooltipText += "\n\nClick to buy";
            else tooltipText += "\n\nClick to upgrade";

        }
        else
        {
            tooltipText += "\n\n<color=green><b>Fully upgraded</b></color>";
        }

        return tooltipText;
    }

}