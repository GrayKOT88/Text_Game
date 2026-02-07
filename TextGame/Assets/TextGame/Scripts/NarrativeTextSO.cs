using UnityEngine;

[CreateAssetMenu(fileName = "Text", menuName = "NarrativeText")]
public class NarrativeTextSO : ScriptableObject
{    
    public NarrativeTextLine[] lines;
    public NarrativeTextOption[] options;
}

[System.Serializable]
public class NarrativeTextLine
{    
    [TextArea(5, 10)] public string text;
}

[System.Serializable]
public class NarrativeTextOption
{
    public string optionText;
    public NarrativeTextSO nextNarrativeText;
}