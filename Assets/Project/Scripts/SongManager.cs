using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SongManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<Button> wordButtons = new List<Button>();
    [SerializeField] private List<TextMeshProUGUI> wordTexts = new List<TextMeshProUGUI>();
    [SerializeField] private Image dimoniBar;
    private int wordOrder;
    float dimoniTimer;
    float initialTimer;
    void Start()    
    {
        initialTimer = 0;
        dimoniTimer = 10;
        for(int i = 0; i<WordManager.instance.WordManagerCount();++i)
        {
            wordButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = WordManager.instance.GetWord(i).GetCorrectWord();
            int buttonPosition = i;
            wordButtons[i].onClick.AddListener(()=>AddWordToSong(buttonPosition));
        }
        WordManager.instance.newOrderForSong();
        
    }

    // Update is called once per frame
    void Update()
    {
        DimoniTimerUpdate();
        if (dimoniBar.fillAmount == 1 &&WordManager.instance.WordManagerCount()>wordOrder)
        {
            wordTexts[wordOrder].text= WordManager.instance.GetWord(wordOrder).GetDesorderedWord();
            ++wordOrder;
            initialTimer = Time.time;
        }
        else
        {

        }
        
    }
    private void DimoniTimerUpdate()
    {
        float timeInCycle = Time.time-initialTimer;

        float fill = timeInCycle / dimoniTimer;

        dimoniBar.fillAmount = fill;
    }
    public void AddWordToSong(int buttonPosition)
    {
        if (wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text==WordManager.instance.GetWord(wordOrder).GetCorrectWord())
        {
            wordTexts[wordOrder].text = wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text;
            ++wordOrder;
            initialTimer = Time.time;
        }
    }
    
}
