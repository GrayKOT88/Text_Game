using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Ссылки")]
    public StorySO[] stories; // Массив всех историй
    public GameObject storyButtonPrefab; // Префаб кнопки
    public Transform buttonContainer; // Родительский объект для кнопок (GridLayoutGroup/VerticalLayoutGroup)

    [Header("Ссылка на NarrativeTextManager")]
    public NarrativeTextManager narrativeTextManager;

    private void Start()
    {
        CreatStoryButtons();
    }

    private void CreatStoryButtons()
    {
        // Очищаем контейнер от старых кнопок (если есть)
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // Создаем кнопки для каждой истории
        foreach (StorySO story in stories)
        {
            GameObject buttonObj = Instantiate(storyButtonPrefab, buttonContainer);
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            Button button = buttonObj.AddComponent<Button>();

            // Устанавливаем текст кнопки
            buttonText.text = story.storyName;

            // Назначаем действие при нажатии
            button.onClick.AddListener(() => StartStory(story.firstChapter));
        }
    }

    private void StartStory(NarrativeTextSO firstChapter)
    {
        // Запускаем первую главу через NarrativeTextManager
        narrativeTextManager.StartNarrativeText(firstChapter);

        // Скрываем меню
        gameObject.SetActive(false);
    }
}