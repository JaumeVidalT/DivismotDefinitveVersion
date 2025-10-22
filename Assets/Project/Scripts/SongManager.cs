using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SongManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<Button> wordButtons = new List<Button>();
    [SerializeField] private List<TextMeshProUGUI> wordTexts = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> versos= new List<Image>();
    [SerializeField] private Image dimoniBar;
    [SerializeField] private GameObject background;
    private int wordOrder;
    private int versosOrder;
    private float dimoniTimer;
    private float initialTimer;
    private bool DimoniActive;
    private float fillAmountAdd;
    void Start()    
    {
        wordOrder= 0;
        versosOrder = 0;
        versos[versosOrder].gameObject.SetActive(true);

        initialTimer = Time.time;
        dimoniTimer = 10;
        DimoniActive = true;
        /*StarManager.instance.SetStarsOnScene(background);*///estrellas recordar ponerlo luego
        fillAmountAdd = 2.0f / WordManager.instance.WordManagerCount();
        

        WordManager.instance.newOrderForSong();
        SetButtons();
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
        dimoniBar.fillAmount = 0f;
        if (++wordOrder== WordManager.instance.WordManagerCount())
        {
            SceneManager.LoadScene(1);
            //Cambio de escena pendiente
            return;
        }
        if (wordTexts[wordOrder].gameObject.activeSelf != false)
        {
            ++versosOrder;
            versos[versosOrder].gameObject.SetActive(true);
        }
        StartCoroutine(WaitForButtonPressed());
        
    }

    public void AddWordToSong(int buttonPosition)
    {
        if (wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text==WordManager.instance.GetWord(wordOrder).GetCorrectWord())
        {
            wordTexts[wordOrder].text = wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text;
            wordTexts[wordOrder].color = Color.green;
            /*StarManager.instance.AddFillAmount(fillAmountAdd);*///Lo mismo quitar luego
            AddWordOrder();
        }
    }
    IEnumerator WaitForButtonPressed()
    {
        
        DimoniActive=false;
        SetButtons();
        yield return new WaitForSeconds(2f);
        versos[wordOrder-1].gameObject.SetActive(false);
        versos[wordOrder].gameObject.SetActive(true);
        initialTimer = Time.time;
        DimoniActive = true;
    }
    private void SetButtons()
    {
        int correctWordSavePosition = Random.Range(0, 4);
        wordButtons[correctWordSavePosition].GetComponentInChildren<TextMeshProUGUI>().text = WordManager.instance.GetWord(wordOrder).GetCorrectWord();
        int minimoI = 0;
        for (int i=0; i< wordButtons.Count-1;i=minimoI)
        {
            int newRandomWord= Random.Range(0, WordManager.instance.WordManagerCount()-1);
            int newRandomPosition= Random.Range(0, 4);
            if (newRandomWord != wordOrder && newRandomPosition != correctWordSavePosition && verifyWord(newRandomPosition, newRandomWord)) 
            {
                minimoI++;
                wordButtons[newRandomPosition].GetComponentInChildren<TextMeshProUGUI>().text=WordManager.instance.GetWord(newRandomWord).GetCorrectWord();
                Debug.Log(WordManager.instance.GetWord(newRandomWord).GetCorrectWord());
            }
           
        }
    }
    private bool verifyWord(int listPosition,int wordPosition)
    {
        if(wordButtons[listPosition].GetComponentInChildren<TextMeshProUGUI>().text==""&&verifyNotRepeat(wordPosition))
        {                   
            return true;
        }
        return false;
    }
    private bool verifyNotRepeat(int wordPosition)
    {
        foreach (Button button in wordButtons)
        {
            if (button.GetComponentInChildren<TextMeshProUGUI>().text == WordManager.instance.GetWord(wordPosition).GetCorrectWord())
            {
                return false;
            }
        }
        return true;
    }
    
}
