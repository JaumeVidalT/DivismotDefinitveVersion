using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    private List<Word> wordManager = new List<Word>();
    public static WordManager instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        wordManager.Add(new Word("grocs", "grosc"));
        wordManager.Add(new Word("tardor", "radtro"));
        wordManager.Add(new Word("menjo", "jomne"));
        wordManager.Add(new Word("marrons", "srramon"));
        wordManager.Add(new Word("arribat", "batrira"));
        wordManager.Add(new Word("carrer", "rerarc"));
        wordManager.Add(new Word("torrem", "retorm"));
        wordManager.Add(new Word("bolets", "tlesob"));
        wordManager.Add(new Word("fulles", "sllfeu"));
        wordManager.Add(new Word("àvia", "viaà"));
        wordManager.Add(new Word("pressa", "sapres"));
        wordManager.Add(new Word("bosc", "csob"));
        wordManager.Add(new Word("tornem", "nemotr"));
        wordManager.Add(new Word("castanyes", "stacnesay"));
        wordManager.Add(new Word("cauen", "neuca"));
        wordManager.Add(new Word("fred", "rdef"));
        wordManager.Add(new Word("cacem", "mcaec"));
    }

    public Word GetWord(int wordPosition)
    {
        return wordManager[wordPosition];
    }
    public int WordManagerCount() { return wordManager.Count; }

    public void newOrderForSong()
    {
        wordManager.Clear();

        wordManager.Add(new Word("grocs", "grosc"));
        wordManager.Add(new Word("tardor", "radtro"));
        wordManager.Add(new Word("menjo", "jomne"));
        wordManager.Add(new Word("marrons", "srramon"));
        wordManager.Add(new Word("arribat", "batrira"));
        wordManager.Add(new Word("carrer", "rerarc"));
        wordManager.Add(new Word("bolets", "tlesob"));
        wordManager.Add(new Word("torrem", "retorm"));
        wordManager.Add(new Word("fulles", "sllfeu"));
        wordManager.Add(new Word("pressa", "sapres"));
        wordManager.Add(new Word("bolets", "tlesob"));      // Repetida
        wordManager.Add(new Word("àvia", "viaà"));
        wordManager.Add(new Word("bosc", "csob"));
        wordManager.Add(new Word("tornem", "nemotr"));
        wordManager.Add(new Word("bolets", "tlesob"));      // Repetida
        wordManager.Add(new Word("cauen", "neuca"));
        wordManager.Add(new Word("castanyes", "stacnesay"));
        wordManager.Add(new Word("cacem", "mcaec"));
        wordManager.Add(new Word("fred", "rdef"));
        wordManager.Add(new Word("fulles", "sllfeu"));


    }





}
