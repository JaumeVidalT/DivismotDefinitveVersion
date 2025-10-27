using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GamePlayManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string playerWord;
    private int wordSize;
    private int wordOrder;
    private int letterPosition;
    private int fontSize=90;


    [SerializeField] private List<Button> UnorderedButtons=new List<Button>();
    [SerializeField] private List<Button> OrderedButtons=new List<Button>();
    
    [SerializeField] private Sprite correctWord;
    [SerializeField] private Sprite incorrectWord;

    [SerializeField] private List<Sprite> wordImages = new List<Sprite>();
    [SerializeField] private Image imageWord;

    [SerializeField] private Image BackgroundOrderedButtons;
    [SerializeField] private Image BackgroundUnorderedbuttons;


    private float DistanceWithBorders = 60;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private Button NextWordButton;
    [SerializeField] private Button ResetWordButton;
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI wordOrderText;

    private float fillAmountAdd;  
    void Start()
    {
        fillAmountAdd = 1.0f / WordManager.instance.WordManagerCount();
        wordOrder = 0;
        letterPosition = 0;
        wordOrderText.text=wordOrder.ToString();

        SetActivButton(NextWordButton, false);
        SetActivButton(ResetWordButton, false);
        SetNewButtons();
        StarManager.instance.ResetStars();
        StarManager.instance.SetStarsOnScene(background,1);

    }

    // Update is called once per frame
    public void ResetWord()
    {
        CleanButtons();
        SetNewButtons();
        SetActivButton(ResetWordButton, false);
    }
    public void NextWord()
    {
        if(++wordOrder >= WordManager.instance.WordManagerCount())
        {
            SceneManager.LoadScene(4);
            return;
        }
        wordOrderText.text = wordOrder.ToString();
        CleanButtons();
        SetNewButtons();
        SetActivButton(NextWordButton, false);
    }
    private void CleanButtons()
    {
        foreach (Button button in UnorderedButtons) { Destroy(button.gameObject); }
        foreach (Button button in OrderedButtons) { Destroy(button.gameObject); }
        UnorderedButtons.Clear();
        OrderedButtons.Clear();
    }
    public void AddLetter(string letter)
    {
        if (letter == WordManager.instance.GetWord(wordOrder).GetCorrectWord()[letterPosition].ToString())
        {
            OrderedButtons[letterPosition].GetComponent<Image>().sprite = correctWord;
        }
        else
        {
            OrderedButtons[letterPosition].GetComponent<Image>().sprite = incorrectWord;
        }   

        SetActivButton(OrderedButtons[letterPosition], true);
        OrderedButtons[letterPosition].GetComponentInChildren<TextMeshProUGUI>().text = letter ;
        OrderedButtons[letterPosition].GetComponentInChildren<TextMeshProUGUI>().fontSize = fontSize;
        playerWord += letter;
        letterPosition++;


        if (wordSize == playerWord.Length)//First State Gameplay  Word=playerWord i worManager su tamaño es mayor
        {
            if (WordManager.instance.GetWord(wordOrder).CheckCorrectWord(playerWord))
            {
                StarManager.instance.AddFillAmount(fillAmountAdd);
                SetActivButton(NextWordButton, true);
            }
            else
            {
                SetActivButton(ResetWordButton, true);
            }
           

        }
    }
    private void SetActivButton(Button button,bool state)
    {
        button.gameObject.SetActive(state);
    }
    public void DesactiveLetter(int buttonPosition)
    {
        UnorderedButtons[buttonPosition].gameObject.SetActive(false);

    }
    private float GetNewWidth(float buttonWidth)
    {
        return (wordSize * buttonWidth) + (DistanceWithBorders*2);
    }
    private Button GetButton(Image background, UnityEngine.Vector3 newPositionPrefab)
    {
        Button newButtonUnordered = Instantiate(buttonPrefab, newPositionPrefab, UnityEngine.Quaternion.identity).GetComponent<Button>();
        newButtonUnordered.transform.SetParent(background.transform, false);
        return newButtonUnordered;
    }
    private void ModifyWidth(Image background,float newWidth)
    {
        RectTransform backgroundRect = background.GetComponent<RectTransform>();
        UnityEngine.Vector2 sizeDelta = backgroundRect.sizeDelta;
        sizeDelta.x= newWidth;
        backgroundRect.sizeDelta = sizeDelta;
    }
    public void SetNewButtons()
    {
        playerWord ="";
        letterPosition = 0;
        wordSize = WordManager.instance.GetWord(wordOrder).GetCorrectWord().Length;
        imageWord.sprite = wordImages[wordOrder];

        RectTransform ButtonPrefabRectangle = buttonPrefab.GetComponent<RectTransform>();
        float buttonOffSetX = ButtonPrefabRectangle.rect.width / 2 + DistanceWithBorders;
        float newWidth = GetNewWidth(ButtonPrefabRectangle.rect.width);
        ModifyWidth(BackgroundOrderedButtons, newWidth);
        ModifyWidth(BackgroundUnorderedbuttons, newWidth);

        float initialPos=0;
        initialPos -= (newWidth / 2.0f)-(buttonOffSetX);
        for(int i = 0;i<wordSize;i++)
        {
            UnityEngine.Vector3 newPositionPrefab = new UnityEngine.Vector3((initialPos + (ButtonPrefabRectangle.rect.width * i)), 0, 0);
            OrderedButtons.Add(GetButton(BackgroundOrderedButtons, newPositionPrefab));
            UnorderedButtons.Add(GetButton(BackgroundUnorderedbuttons, newPositionPrefab));
            
        }
        foreach (Button button in OrderedButtons)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < wordSize; i++)
        {

            char letterOrdered = WordManager.instance.GetWord(wordOrder).GetDesorderedWord()[i];
            UnorderedButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = letterOrdered.ToString();
            UnorderedButtons[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = fontSize;

            UnorderedButtons[i].onClick.RemoveAllListeners();

            char letterCopy = letterOrdered;
            int numberButtonCopy = i;
            UnorderedButtons[i].onClick.AddListener(() => AddLetter(letterCopy.ToString()));
            UnorderedButtons[i].onClick.AddListener(() => DesactiveLetter(numberButtonCopy));

            
        }

    }


}
