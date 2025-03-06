using System;
using UnityEngine;




public class CraftingBuilding : MonoBehaviour, Buildable
{
    [Serializable]
    private class SerializableRecipes
    {
        //[SerializeField]
        public SerializableDict<ResourseType, int> craftingCost;
        
        //[SerializeField]
        public SerializableDict<ResourseType, int> craftingGain;

        public float craftingTime = 3;
    }


    [SerializeField]
    string description;

    [SerializeField]
    SerializableRecipes[] unlocksRecipes;

    private CraftingRecipe[] recipes;



    void Start()
    {
        recipes = new CraftingRecipe[unlocksRecipes.Length];

        for(int i = 0; i < recipes.Length; i++)
        {
            recipes[i] = CraftingManager.instance.LearnRecipe(
                unlocksRecipes[i].craftingCost.ToDictionary(), 
                unlocksRecipes[i].craftingGain.ToDictionary(),
                unlocksRecipes[i].craftingTime);
        }
    }

    public string BuildingProductionDescription()
    {
        return description;
    }

    public void OnBuildingDestroyed()
    {
        foreach(var recipe in recipes)
        {
            CraftingManager.instance.ForgetRecipe(recipe);
        }
    }
}
