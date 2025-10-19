using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SongManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<Button> wordButtons = new List<Button>();
    [SerializeField] private List<TextMeshProUGUI> wordTexts = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> versos= new List<Image>();
    [SerializeField] private Image dimoniBar;
    private int wordOrder;
    float dimoniTimer;
    float initialTimer;
    bool DimoniActive;
    void Start()    
    {
        initialTimer = Time.time;
        dimoniTimer = 10;
        for(int i = 0; i<WordManager.instance.WordManagerCount();++i)
        {
            wordButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = WordManager.instance.GetWord(i).GetCorrectWord();
            int buttonPosition = i;
            wordButtons[i].onClick.AddListener(()=>AddWordToSong(buttonPosition));
        }
        WordManager.instance.newOrderForSong();
        versos[wordOrder].gameObject.SetActive(true);
        DimoniActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (DimoniActive)
        {
            DimoniTimerUpdate();
        }
        
        if (dimoniBar.fillAmount == 1 &&WordManager.instance.WordManagerCount()>wordOrder)
        {
            wordTexts[wordOrder].text= WordManager.instance.GetWord(wordOrder).GetDesorderedWord();
            wordTexts[wordOrder].color = Color.red;
            AddWordOrder();

        }
        
    }
    private void DimoniTimerUpdate()
    {
        float timeInCycle = Time.time-initialTimer;

        float fill = timeInCycle / dimoniTimer;

        dimoniBar.fillAmount = fill;
    }
    private void AddWordOrder()
    {
        if(++wordOrder== WordManager.instance.WordManagerCount())
        {
            //Cambio de escena pendiente
            return;
        }
        StartCoroutine(WaitForButtonPressed());
        
    }

    public void AddWordToSong(int buttonPosition)
    {
        if (wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text==WordManager.instance.GetWord(wordOrder).GetCorrectWord())
        {
            wordTexts[wordOrder].text = wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text;
            wordTexts[wordOrder].color = Color.green;
            AddWordOrder();
        }
    }
    IEnumerator WaitForButtonPressed()
    {
        dimoniBar.fillAmount = 0f;
        DimoniActive=false;
        yield return new WaitForSeconds(2f);
        versos[wordOrder-1].gameObject.SetActive(false);
        versos[wordOrder].gameObject.SetActive(true);
        initialTimer = Time.time;
        DimoniActive = true;
    }
    
}
