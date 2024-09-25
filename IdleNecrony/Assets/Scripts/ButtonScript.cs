using UnityEngine;

public class ToggleElement : MonoBehaviour
{
    
    public GameObject targetElement;

    
    public void Toggle()
    {
        if (targetElement != null)
        {
            
            targetElement.SetActive(!targetElement.activeSelf);
        }
        else
        {
            Debug.LogWarning("TargetElement не назначен в скрипте ToggleElement.");
        }
    }
}
