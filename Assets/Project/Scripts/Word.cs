using UnityEngine;

public class Word : MonoBehaviour
{
    private string CorrectWord;
    public string GetCorrectWord() { return CorrectWord; }
    public bool CheckCorrectWord(string playerWord) { return playerWord == CorrectWord; }

    private string DesorderedWord;
    public string GetDesorderedWord() { return DesorderedWord; }
    void Update()
    {
        
    }

    public Word(string correctWord, string desorderedWord)
    {
        CorrectWord = correctWord;
        DesorderedWord = desorderedWord;
    }
    
    
}
