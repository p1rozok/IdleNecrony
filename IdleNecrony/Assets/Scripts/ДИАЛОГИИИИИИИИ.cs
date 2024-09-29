using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public string[] lines; 
    public float speedText = 0.05f; 
    public Text dialogText; 
    public Button skipButton; 

    private int index; 

    private void Start()
    {
        index = 0;
        gameObject.SetActive(false); 

        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipTextClick); 
        }
        else
        {
            Debug.LogError("Кнопка пропуска не привязана в DialogSystem.");
        }
    }

    public void StartDialog()
    {
        gameObject.SetActive(true); 
        index = 0;
        dialogText.text = string.Empty;
        Time.timeScale = 0f; 
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSecondsRealtime(speedText); 
        }
    }

    public void SkipTextClick()
    {
        if (dialogText.text == lines[index])
        {
            NextLines();
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = lines[index];
        }
    }

    public void NextLines()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialog(); 
        }
    }

    public void EndDialog()
    {
        gameObject.SetActive(false); 
        Time.timeScale = 1f; 

        if (EnemySpawner.Instance != null)
        {
            Debug.Log("Диалог завершен. Запускаем следующий спавн врагов.");
            EnemySpawner.Instance.SpawnNextEntity(); 
        }
        else
        {
            Debug.LogError("EnemySpawner.Instance не найден при завершении катсцены!");
        }
    }
    }
