using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipe : MonoBehaviour
{

    private Dictionary<ResourseType, int> cost;
    private Dictionary<ResourseType, int> production;

    private float cooldown;

    private float timer;

    [SerializeField]
    Image loadBar;

    [SerializeField]
    TextMeshProUGUI recipeDescTextbox;

    void Update()
    {
        loadBar.fillAmount = 1 - (timer / cooldown);       
    }

    public void DoCraftingTick()
    {      
        if(timer <= 0)
        {
            if (!MaterialManager.TryToRemoveResourses(cost)) return;

            MaterialManager.AddResourses(production);

            timer += cooldown;
        }

        timer -= Time.deltaTime;
    }

    public void StartCrafting()
    {
        CraftingManager.StartProcessingRecipe(this);
    }

    public void PauseCrafting()
    {
        CraftingManager.StopProcessingRecipe(this);
    }

    public void ChangeRecipe(Dictionary<ResourseType, int> cost, Dictionary<ResourseType, int> production, float cooldown)
    {
        this.cost = cost;
        this.production = production;
        this.cooldown = cooldown;

        timer = cooldown;

        UpdateRecipeDescription();
    }

    private void UpdateRecipeDescription()
    {
        string description = "<nobr>Consume: ";

        foreach(var material in cost)
        {
            description += material.Value + "<sprite="+(int)material.Key+ "> ";
        }

        description += "</nobr> <nobr>Produce: ";

        foreach (var material in production)
        {
            description += material.Value + "<sprite=" + (int)material.Key + "> ";
        }
        description += "</nobr> ";

        description += "/ " + cooldown + "s";

        recipeDescTextbox.text = description;

    }

}
