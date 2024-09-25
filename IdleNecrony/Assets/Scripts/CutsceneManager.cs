using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    public GameObject[] cutscenePanels;
    public Text dialogueText;
    public Button nextButton;

    private int currentCutsceneIndex = 0;
    private int currentDialogueIndex = 0;
    private bool isCutscenePlaying = false;

    [TextArea]
    public string[][] dialogues;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("CutsceneManager установлен как Instance.");
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Debug.LogError("CutsceneManager уже существует в сцене! Удаление дублирующегося экземпляра.");
            Destroy(gameObject);
            return;
        }

        foreach (var panel in cutscenePanels)
        {
            panel.SetActive(false);
        }

        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);
        Debug.Log("CutsceneManager успешно инициализирован.");
    }

    public void PlayCutscene(int index)
    {
        Debug.Log($"Попытка запуска катсцены с индексом: {index}");
        if (index >= 0 && index < cutscenePanels.Length)
        {
            currentCutsceneIndex = index;
            currentDialogueIndex = 0;
            isCutscenePlaying = true;

            if (cutscenePanels[currentCutsceneIndex] != null)
            {
                cutscenePanels[currentCutsceneIndex].SetActive(true);
                Debug.Log($"Катсцена {index} активирована.");
            }
            else
            {
                Debug.LogError($"Катсцена с индексом {index} не привязана в массиве.");
                return;
            }

            if (dialogueText != null && dialogues.Length > currentCutsceneIndex && dialogues[currentCutsceneIndex].Length > 0)
            {
                dialogueText.text = dialogues[currentCutsceneIndex][currentDialogueIndex];
                nextButton.gameObject.SetActive(true);
                Debug.Log($"Диалог {currentDialogueIndex} для катсцены {currentCutsceneIndex} отображён.");
            }
            else
            {
                Debug.LogError($"Диалоги или текстовое поле для катсцены {index} не настроены.");
            }
        }
        else
        {
            Debug.LogError($"Индекс катсцены {index} вне диапазона.");
        }
    }

    private void OnNextButtonClicked()
    {
        if (isCutscenePlaying)
        {
            Debug.Log("Кнопка 'Next' нажата.");
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues[currentCutsceneIndex].Length)
            {
                dialogueText.text = dialogues[currentCutsceneIndex][currentDialogueIndex];
                Debug.Log($"Диалог {currentDialogueIndex} для катсцены {currentCutsceneIndex} отображён.");
            }
            else
            {
                Debug.Log($"Катсцена {currentCutsceneIndex} завершена.");
                EndCurrentCutscene();
            }
        }
    }

    private void EndCurrentCutscene()
    {
        Debug.Log($"Завершение катсцены {currentCutsceneIndex}.");
        cutscenePanels[currentCutsceneIndex].SetActive(false);
        nextButton.gameObject.SetActive(false);
        isCutscenePlaying = false;
    }
}
