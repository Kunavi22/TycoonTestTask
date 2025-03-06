using UnityEngine;
using UnityEngine.UI;

public class GraphicsDropdown : MonoBehaviour
{

    private void Start()
    {
        transform.GetComponent<Dropdown>().value = QualitySettings.GetQualityLevel();
    }

    public void RefreshGraphicsSetting(int qualityOption)
    {
        QualitySettings.SetQualityLevel(qualityOption);
    }
}
