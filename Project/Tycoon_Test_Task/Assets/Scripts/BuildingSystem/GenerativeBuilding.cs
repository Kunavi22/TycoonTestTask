using System.Collections;
using TMPro;
using UnityEngine;

public interface Buildable
{
    string BuildingProductionDescription();

    void OnBuildingDestroyed();
}

public class GenerativeBuilding : MonoBehaviour, Buildable
{
    [SerializeField]
    SerializableDict<ResourseType, int> generationPerCooldown;

    private TextMeshPro resourseTickText;

    [SerializeField]
    private float cooldown = 1;

    private float timer;

    void Start()
    {
        resourseTickText = transform.Find("PopupText").GetComponent<TextMeshPro>();
        SetPopUpText();
        timer = cooldown;
        
        
    }

    public virtual void Update()
    {
        timer -= Time.deltaTime;

        if( timer < 0 )
        {
            timer+= cooldown;
            
            StartCoroutine(FadebleText(1f));

            ProduceResourses();
        }
    }

    private void SetPopUpText()
    {
        string text = "+";
        foreach (var resourseGain in generationPerCooldown)
        {
            text += resourseGain.value + "<sprite=" + (int)resourseGain.key + "> ";
        }
        resourseTickText.text = text;

        Color tmpColor = resourseTickText.color;
        tmpColor.a = 0f;
        resourseTickText.color = tmpColor;

    }
    IEnumerator FadebleText(float fadeTime)
    {
        Color tmpColor = resourseTickText.color;
        tmpColor.a = 1;
        resourseTickText.color = tmpColor;
        yield return new WaitForEndOfFrame();

        while (tmpColor.a > 0)
        {
            tmpColor.a -= (Time.deltaTime / fadeTime);
            resourseTickText.color = tmpColor;
            yield return new WaitForEndOfFrame();
        }      
    }

    private void ProduceResourses()
    {
        foreach(var resourse in generationPerCooldown)
        {
            MaterialManager.AddResourse(resourse.key, (uint)resourse.value);
        }  
    }

    public string BuildingProductionDescription()
    {      
        string result = "Production: ";
        foreach(var resourse in generationPerCooldown)
        {
            result += resourse.value + "<sprite=" + (int)resourse.key + "> ";
        }

        result += "/" + cooldown + "s";

        return result;
    }

    public void OnBuildingDestroyed()
    {

    }
}