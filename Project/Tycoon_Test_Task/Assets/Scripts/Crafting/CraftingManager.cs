using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{

    [SerializeField]
    GameObject recipeUIElementPrefab;

    [SerializeField]
    Transform recipeContentField;

    [HideInInspector]
    private static List<CraftingRecipe> recipesAtProcess;

    [SerializeField]
    GameObject CraftingMenuButton;



    public static CraftingManager instance;
    void Start()
    {
        instance = this;

        recipesAtProcess = new List<CraftingRecipe>();
    }


    void Update()
    {
        foreach(var recipe in recipesAtProcess)
        {
            recipe.DoCraftingTick();
        }
    }


    public CraftingRecipe LearnRecipe(Dictionary<ResourseType, int> cost, Dictionary<ResourseType, int> gain, float cooldown)
    {         
        CraftingMenuButton.SetActive(true);      

        GameObject recipeObj = Instantiate(recipeUIElementPrefab, recipeContentField);

        CraftingRecipe recipe = recipeObj.GetComponent<CraftingRecipe>();

        recipe.ChangeRecipe(cost, gain, cooldown);

        return recipe;
    }

    public void ForgetRecipe(CraftingRecipe recipe)
    {
        recipesAtProcess.Remove(recipe);
        Destroy(recipe.gameObject);
    }

    static public void StartProcessingRecipe(CraftingRecipe recipe)
    {
        recipesAtProcess.Add(recipe);
    }

    static public void StopProcessingRecipe(CraftingRecipe recipe)
    {
        recipesAtProcess.Remove(recipe);
    }


}
