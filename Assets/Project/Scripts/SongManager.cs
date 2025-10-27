using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] private Image CuernosPrefab;
    private int wordOrder;
    private int versosOrder;
    private float dimoniTimer;
    private float initialTimer;
    private bool DimoniActive;
    private float fillAmountAdd;

    [SerializeField] private List<AudioResource> audioSources = new List<AudioResource>();
    [SerializeField] private GameObject audioObject;
    [SerializeField] Button botonSiguienteVerso;
    void Start()    
    {
        wordOrder= 0;
        versosOrder = 0;
        versos[versosOrder].gameObject.SetActive(true);

        initialTimer = Time.time;
        dimoniTimer = 10;
        DimoniActive = true;
        StarManager.instance.SetStarsOnScene(background,1);
        

        WordManager.instance.newOrderForSong();
        fillAmountAdd = 2.0f / WordManager.instance.WordManagerCount();
        SetButtons();
        audioObject.GetComponent<AudioSource>().Play();
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
            wordTexts[wordOrder].text = WordManager.instance.GetWord(wordOrder).GetDesorderedWord();
            wordTexts[wordOrder].color = Color.red;
            Image cuernos = Instantiate(CuernosPrefab, new Vector2(0, 60), Quaternion.identity);
            cuernos.gameObject.transform.SetParent(wordTexts[wordOrder].transform, false);
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
        if (++wordOrder>= WordManager.instance.WordManagerCount())
        {
            SceneManager.LoadScene(4);
            //Cambio de escena pendiente
            return;
        }
       
        StartCoroutine(WaitForButtonPressed());
        
    }

    public void AddWordToSong(int buttonPosition)
    {
        if (DimoniActive)
        {
            wordTexts[wordOrder].text = wordButtons[buttonPosition].GetComponentInChildren<TextMeshProUGUI>().text;
            wordTexts[wordOrder].color = Color.green;
            StarManager.instance.AddFillAmount(fillAmountAdd);
            AddWordOrder();
        }
        
    }
    public void IncorrectWord()
    {
        if (DimoniActive) 
        {
            wordTexts[wordOrder].text = WordManager.instance.GetWord(wordOrder).GetDesorderedWord();
            wordTexts[wordOrder].color = Color.red;
            Image cuernos = Instantiate(CuernosPrefab, new Vector2(0, 60), Quaternion.identity);
            cuernos.gameObject.transform.SetParent(wordTexts[wordOrder].transform, false);
            AddWordOrder();
        }
       
    }
    IEnumerator WaitForButtonPressed()
    {
        
        DimoniActive=false;      
        yield return new WaitForSeconds(2f);
        if (wordTexts[wordOrder].gameObject.activeInHierarchy != true)
        {

            botonSiguienteVerso.gameObject.SetActive(true);

        }
        else
        {
            ClearButtons();
            SetButtons();
            DimoniActive = true;
            initialTimer = Time.time;
        }

        

    }
    public void ButtonNextVerso()
    {
        ClearButtons();
        SetButtons();
        versos[versosOrder++].gameObject.SetActive(false);
        versos[versosOrder].gameObject.SetActive(true);
        audioObject.GetComponent<AudioSource>().resource = audioSources[versosOrder];
        audioObject.GetComponent<AudioSource>().Play();
        DimoniActive = true;
        initialTimer = Time.time;
        botonSiguienteVerso.gameObject.SetActive(false);
    }
    private void ClearButtons()
    {
        foreach (Button button in wordButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.onClick.RemoveAllListeners();
        }
    }
    private void SetButtons()
    {
        int correctWordSavePosition = Random.Range(0, 5);
        wordButtons[correctWordSavePosition].GetComponentInChildren<TextMeshProUGUI>().text = WordManager.instance.GetWord(wordOrder).GetCorrectWord();
        wordButtons[correctWordSavePosition].onClick.AddListener(() => AddWordToSong(correctWordSavePosition));
        int minimoI = 0;
        for (int i=0; i< wordButtons.Count-1;i=minimoI)
        {
            int newRandomWord= Random.Range(0, WordManager.instance.WordManagerCount());
            int newRandomPosition= Random.Range(0, 5);
            if (newRandomWord != wordOrder && newRandomPosition != correctWordSavePosition && verifyWord(newRandomPosition, newRandomWord)) 
            {
                minimoI++;
                wordButtons[newRandomPosition].GetComponentInChildren<TextMeshProUGUI>().text=WordManager.instance.GetWord(newRandomWord).GetCorrectWord();
                wordButtons[newRandomPosition].onClick.AddListener(() => IncorrectWord());
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
