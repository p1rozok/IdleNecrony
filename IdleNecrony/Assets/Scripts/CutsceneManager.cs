using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    public GameObject cutsceneUI; 
    public Text dialogueText; 
    public Button nextButton; 

    private string[] currentDialogue; 
    private int currentDialogueIndex = 0; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CutsceneManager установлен как Instance.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cutsceneUI.SetActive(false); 
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    public void PlayCutscene(string[] dialogue)
    {
        currentDialogue = dialogue;
        currentDialogueIndex = 0;
        cutsceneUI.SetActive(true);
        ShowCurrentDialogue();
    }

    private void ShowCurrentDialogue()
    {
        if (currentDialogueIndex < currentDialogue.Length)
        {
            dialogueText.text = currentDialogue[currentDialogueIndex];
        }
        else
        {
            EndCutscene();
        }
    }

    private void OnNextButtonClicked()
    {
        currentDialogueIndex++;
        ShowCurrentDialogue();
    }

    private void EndCutscene()
    {
        cutsceneUI.SetActive(false);
        currentDialogue = null;
        currentDialogueIndex = 0;

        
        EnemySpawner.Instance.SpawnNextEntity();
    }
}
