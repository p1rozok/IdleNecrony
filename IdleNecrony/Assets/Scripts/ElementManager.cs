
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public GameObject[] uiElements; 

    private void OnEnable()
    {
        
        Castle.OnCastleDestroyed += ActivateElement;
    }

    private void OnDisable()
    {
        
        Castle.OnCastleDestroyed -= ActivateElement;
    }

    private void ActivateElement(int cutsceneIndex)
    {
        if (cutsceneIndex >= 0 && cutsceneIndex < uiElements.Length)
        {
            uiElements[cutsceneIndex].SetActive(true); 
            Debug.Log($"Активирован элемент с индексом {cutsceneIndex}");
        }
        else
        {
            Debug.LogError($"UI элемент с индексом {cutsceneIndex} не найден!");
        }
    }
}
