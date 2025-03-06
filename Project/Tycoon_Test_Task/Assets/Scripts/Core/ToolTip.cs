using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ToolTip: MonoBehaviour
{
    public static ToolTip instance;

    private TextMeshProUGUI textField;

    public string Text { 
        set
        {
            textField.text = value;         
        }
    }

    void Start()
    {
        instance = this;
        textField = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }


    void Update()
    {        
        transform.position = Input.mousePosition;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Show(string text)
    {
        Text = text;
        gameObject.SetActive(true);
    }

}
