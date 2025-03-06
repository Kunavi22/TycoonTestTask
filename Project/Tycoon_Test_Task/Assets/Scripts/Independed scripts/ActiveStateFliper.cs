using UnityEngine;

public class ActiveStateFliper : MonoBehaviour
{
    public void FlipActiveState()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
