using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SaveManager : MonoBehaviour
{

    [SerializeField]
    public List<BuildingSpot> buildingSpots;


    [HideInInspector]
    public static GameStateSerializable loadedGameState;

    public void OnValidate()
    {
        BuildingSpot[] onSceneSpots = FindObjectsOfType<BuildingSpot>(true);

        buildingSpots.RemoveAll(spot => !onSceneSpots.Contains(spot));

        for(int i = 0; i < buildingSpots.Count; i++)
        {
            buildingSpots[i].ID = i;
        }
    }



    public static SaveManager instance;
    private void Awake()
    {
        instance = this;



#if UNITY_EDITOR

        //testing save
        //DataSaver.ClearAllData();

        //LoadGameState();
        //loadedGameState.materialsCount = new List<ulong>();

        //loadedGameState.materialsCount.Add(10000);
        //loadedGameState.materialsCount.Add(10000);
        //loadedGameState.materialsCount.Add(10000);
        //loadedGameState.materialsCount.Add(10000);
        //        loadedGameState.materialsCount.Add(10000);

        //DataSaver.SaveData(loadedGameState);
#endif


        LoadGameState();


        //autosaves
        StartCoroutine(SaveEveryTimeInterval(60));
    }


    IEnumerator SaveEveryTimeInterval(float secondsBetweenSaves)
    {
        while(true)
        {
            yield return new WaitForSeconds(secondsBetweenSaves);

            SaveGameState();
        }
    }


    public void SaveGameState()
    {
        GameStateSerializable state = new GameStateSerializable();

        //save player materials
        state.materialsCount = new List<ulong>(MaterialManager.materialAmount);


        //save player buildings
        state.buildingSpotsLevel = new List<int>();
        foreach(var buildingSpot in buildingSpots)
        {
            state.buildingSpotsLevel.Add(buildingSpot.buildingLevel);
        }

        DataSaver.SaveData(state);
    }

    private void LoadGameState()
    {
        GameStateSerializable state;

        if(!DataSaver.LoadData<GameStateSerializable>(out state))
        {
            state = new GameStateSerializable();
            state.buildingSpotsLevel = new List<int>();
            state.materialsCount = new List<ulong>();
        }

        loadedGameState = state;
    }

    public class GameStateSerializable
    {
        public List<int> buildingSpotsLevel;

        public List<ulong> materialsCount;
    }
}