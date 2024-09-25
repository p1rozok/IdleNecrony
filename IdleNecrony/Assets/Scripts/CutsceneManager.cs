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
            Debug.Log("CutsceneManager ���������� ��� Instance.");
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Debug.LogError("CutsceneManager ��� ���������� � �����! �������� �������������� ����������.");
            Destroy(gameObject);
            return;
        }

        foreach (var panel in cutscenePanels)
        {
            panel.SetActive(false);
        }

        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);
        Debug.Log("CutsceneManager ������� ���������������.");
    }

    public void PlayCutscene(int index)
    {
        Debug.Log($"������� ������� �������� � ��������: {index}");
        if (index >= 0 && index < cutscenePanels.Length)
        {
            currentCutsceneIndex = index;
            currentDialogueIndex = 0;
            isCutscenePlaying = true;

            if (cutscenePanels[currentCutsceneIndex] != null)
            {
                cutscenePanels[currentCutsceneIndex].SetActive(true);
                Debug.Log($"�������� {index} ������������.");
            }
            else
            {
                Debug.LogError($"�������� � �������� {index} �� ��������� � �������.");
                return;
            }

            if (dialogueText != null && dialogues.Length > currentCutsceneIndex && dialogues[currentCutsceneIndex].Length > 0)
            {
                dialogueText.text = dialogues[currentCutsceneIndex][currentDialogueIndex];
                nextButton.gameObject.SetActive(true);
                Debug.Log($"������ {currentDialogueIndex} ��� �������� {currentCutsceneIndex} ��������.");
            }
            else
            {
                Debug.LogError($"������� ��� ��������� ���� ��� �������� {index} �� ���������.");
            }
        }
        else
        {
            Debug.LogError($"������ �������� {index} ��� ���������.");
        }
    }

    private void OnNextButtonClicked()
    {
        if (isCutscenePlaying)
        {
            Debug.Log("������ 'Next' ������.");
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues[currentCutsceneIndex].Length)
            {
                dialogueText.text = dialogues[currentCutsceneIndex][currentDialogueIndex];
                Debug.Log($"������ {currentDialogueIndex} ��� �������� {currentCutsceneIndex} ��������.");
            }
            else
            {
                Debug.Log($"�������� {currentCutsceneIndex} ���������.");
                EndCurrentCutscene();
            }
        }
    }

    private void EndCurrentCutscene()
    {
        Debug.Log($"���������� �������� {currentCutsceneIndex}.");
        cutscenePanels[currentCutsceneIndex].SetActive(false);
        nextButton.gameObject.SetActive(false);
        isCutscenePlaying = false;
    }
}
