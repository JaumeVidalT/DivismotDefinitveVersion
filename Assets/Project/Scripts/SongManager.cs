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
    void Start()    
    {
        dimoniTimer = 10;
        for(int i = 0; i<WordManager.instance.WordManagerCount();++i)
        {
            wordButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = WordManager.instance.GetWord(i).GetCorrectWord();
            int buttonPosition = i;
            wordButtons[i].onClick.AddListener(()=>AddWordToSong(buttonPosition));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        DimoniTimerUpdate();
        if (Time.time% dimoniTimer <= 0)
        {
            wordTexts[wordOrder].text= WordManager.instance.GetWord(wordOrder).GetDesorderedWord();
            ++wordOrder;
        }
        
    }
    private void DimoniTimerUpdate()
    {
        dimoniBar.fillAmount = Time.time % dimoniTimer;
    }
    public void AddWordToSong(int buttonPosition)
    {        
        /*if()
        {
            wordTexts[wordOrder].text = wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text;
            ++wordOrder;
        }*/
    }
    
}
