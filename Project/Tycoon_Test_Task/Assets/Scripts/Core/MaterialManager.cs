using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ResourseType
{
    Food,
    Wood,
    Planks,
    Rock,
    Cotton,
    Cloth,
    Coal,
    RawIron,
    Iron
}

public class MaterialManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI materialsHeaderText;

    public static ulong[] materialAmount { private set; get; }


    private void Start()
    {
        int resourses = Enum.GetNames(typeof(ResourseType)).Length;

        materialAmount = new ulong[resourses];


        //Load resourse amount from save file
        List<ulong> materialsFromSave = SaveManager.loadedGameState.materialsCount;
        for(int i = 0; i < materialsFromSave.Count; i++)
        {
            materialAmount[i] = materialsFromSave[i];
        }    
    }

    void Update()
    {
        materialsHeaderText.text = ToSingleLineString();
    }

    private string ToSingleLineString()
    {
        string result = "";
        for(int i = 0; i < materialAmount.Length; i++)
        {
            if (materialAmount[i] == 0) continue;

            result += "<sprite=" + i + ">: " + materialAmount[i].KiloFormat() + " ";
        }

        return result;
    }

    public static void AddResourse(ResourseType ID, uint amount)
    {
        materialAmount[(int)ID] += amount;
    }

    public static void AddResourses(Dictionary<ResourseType, int> resoursesAmount)
    {
        foreach(var resourse in resoursesAmount)
        {
            AddResourse(resourse.Key, (uint)resourse.Value);
        }
    }

    private static void RemoveResourse(ResourseType resourse, uint amount)
    {
        materialAmount[(int)resourse] -= amount;

    }


    public static bool TryToRemoveResourses(Dictionary<ResourseType, int> resoursesAmount)
    {

        foreach (var item in resoursesAmount)
        {
            if (MaterialManager.materialAmount[(int)item.Key] < (uint)item.Value)
            {
                //Not enough resourses
                return false;
            }
        }


        foreach (var item in resoursesAmount)
        {
            MaterialManager.RemoveResourse(item.Key, (uint)item.Value);
        }

        return true;
    }

}



public static class Extensions
{
    public static string KiloFormat(this ulong num)
    {
        if (num >= 1000000)
            return (num / 1000000).ToString("0.#") + "." + (num % 1000000 / 100000).ToString() + "M";

        if (num >= 1000)
            return (num / 1000).ToString() + "." + (num % 1000 / 100).ToString() + "K";

        return num.ToString();
    }
}