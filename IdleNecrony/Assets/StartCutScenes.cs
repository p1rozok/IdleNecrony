using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem1 : MonoBehaviour
{
    public string[] lines;
    public float speedText;
    public Text dialogText;

    private int index;
    private bool dialogPlayed = false;

    void Start()
    {
        if (!dialogPlayed)
        {
            index = 0;
            dialogText.text = string.Empty;
            gameObject.SetActive(true);
            StartDialog();
        }
    }

    public void StartDialog()
    {
        if (!dialogPlayed)
        {
            Time.timeScale = 0f;
            StartCoroutine(TypeLine());
        }
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
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        index = 0;
        dialogPlayed = true;
        Time.timeScale = 1f;
        dialogText.text = string.Empty;
        gameObject.SetActive(false);
    }
}
