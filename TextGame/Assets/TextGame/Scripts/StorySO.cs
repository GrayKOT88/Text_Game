using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "GameText/StorySO")]
public class StorySO : ScriptableObject
{
    public string storyName;
    public NarrativeTextSO firstChapter;
}