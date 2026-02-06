using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NarrativeTextManager : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup canvasGroup;    
    public TMP_Text NarrativeText;
    public Button[] choiceButtons;

    public bool isNarrativeTextActive;

    private NarrativeTextSO currentNarrativeText;
    private int narrativeTextIndex;

    private float lastNarrativeTextEndTime;
    private float narrativeTextCooldown = 0.1f;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
            button.gameObject.SetActive(false);
    }

    public bool CanStartNarrativeText()
    {
        return Time.unscaledTime - lastNarrativeTextEndTime >= narrativeTextCooldown;
    }

    public void StartNarrativeText(NarrativeTextSO narrativeTextSO)
    {
        currentNarrativeText = narrativeTextSO;
        narrativeTextIndex = 0;
        isNarrativeTextActive = true;
        ShowNarrativeText();        
    }

    private void ShowNarrativeText()
    {
        NarrativeTextLine line = currentNarrativeText.lines[narrativeTextIndex];
        NarrativeText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        narrativeTextIndex++;
        AdvanceNarrativeText();
    }

    private void AdvanceNarrativeText()
    {
        if (narrativeTextIndex < currentNarrativeText.lines.Length)
        {
            CleaChoices();
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "-->";
            choiceButtons[0].onClick.AddListener(ShowNarrativeText);
            choiceButtons[0].gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
        }            
        else
            ShowChoices();
    }

    private void ShowChoices()
    {
        CleaChoices();

        if (currentNarrativeText.options.Length > 0)
        {
            for (int i = 0; i < currentNarrativeText.options.Length; i++)
            {
                var option = currentNarrativeText.options[i];

                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);

                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextNarrativeText));
            }
            EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);
        }
        else
        {                    
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "End";
            choiceButtons[0].onClick.AddListener(EndNarrativeText);
            choiceButtons[0].gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(choiceButtons[0].gameObject);            
        }
    }

    private void ChooseOption(NarrativeTextSO narrativeTextSO)
    {
        if (narrativeTextSO == null)
            EndNarrativeText();
        else
        {
            CleaChoices();
            StartNarrativeText(narrativeTextSO);
        }
    }

    private void EndNarrativeText()
    {
        narrativeTextIndex = 0;
        isNarrativeTextActive = false;
        CleaChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        lastNarrativeTextEndTime = Time.unscaledTime;
    }

    private void CleaChoices()
    {
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
