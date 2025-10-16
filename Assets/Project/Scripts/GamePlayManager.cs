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

    [SerializeField] private List<Button> UnorderedButtons=new List<Button>();
    [SerializeField] private List<Button> OrderedButtons=new List<Button>();
    [SerializeField] private Image BackgroundOrderedButtons;
    [SerializeField] private Image BackgroundUnorderedbuttons;


    private float DistanceWithBorders = 25;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private Button NextWordButton;

    private bool startSong;    
    void Start()
    {

        wordOrder = 0;
        startSong = false;
        letterPosition = 0;

        SetActivButton(NextWordButton, false);
        SetNewButtons();

    }

    // Update is called once per frame
    void Update()
    {
        if (startSong)//Song Gameplay
        {
            SceneManager.LoadScene("SongScene");
            return;
        }
        if (wordOrder >= WordManager.instance.WordManagerCount() && !startSong)//Transition
        {
            Destroy(NextWordButton.gameObject);
            CleanButtons();
            startSong = true;
            return;
        }
        if (wordSize == playerWord.Length&& WordManager.instance.WordManagerCount()>wordOrder&&!startSong)//First State Gameplay  Word=playerWord i worManager su tamaño es mayor
        {
            if (WordManager.instance.GetWord(wordOrder).CheckCorrectWord(playerWord))
            {
                Debug.Log("Victory");
            }
            SetActivButton(NextWordButton, true);       
                        
        }
        
        


    }
    public void NextWord()
    {
        ++wordOrder;
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
        SetActivButton(OrderedButtons[letterPosition], true);
        OrderedButtons[letterPosition].GetComponentInChildren<TextMeshProUGUI>().text = letter ;
        playerWord += letter;
        letterPosition++;
    }
    private void SetActivButton(Button button,bool state)
    {
        button.gameObject.SetActive(state);
    }
    public void DesactiveLetter(int buttonPosition)
    {
        UnorderedButtons[buttonPosition].gameObject.SetActive(false);
    }
    private float GetNewWidth(float buttonOffSetX)
    {
        return (wordSize * buttonOffSetX) + buttonOffSetX;
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

        RectTransform ButtonPrefabRectangle = buttonPrefab.GetComponent<RectTransform>();
        float buttonOffSetX = ButtonPrefabRectangle.rect.width / 2 + DistanceWithBorders;

        ModifyWidth(BackgroundOrderedButtons, GetNewWidth(buttonOffSetX));
        ModifyWidth(BackgroundUnorderedbuttons, GetNewWidth(buttonOffSetX));

        float initialPos=0;
        initialPos -= ((GetNewWidth(buttonOffSetX) / 2.0f)-buttonOffSetX);
        for(int i = 0;i<wordSize;i++)
        {
            UnityEngine.Vector3 newPositionPrefab = new UnityEngine.Vector3((initialPos + (buttonOffSetX * i)), 0, 0);
            OrderedButtons.Add(GetButton(BackgroundOrderedButtons, newPositionPrefab));
            UnorderedButtons.Add(GetButton(BackgroundUnorderedbuttons, newPositionPrefab));
            
        }
        foreach (Button button in OrderedButtons)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < wordSize; i++)
        {

            char letter = WordManager.instance.GetWord(wordOrder).GetDesorderedWord()[i];
            UnorderedButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();

            UnorderedButtons[i].onClick.RemoveAllListeners();

            char letterCopy = letter;
            int numberButtonCopy = i;
            UnorderedButtons[i].onClick.AddListener(() => AddLetter(letterCopy.ToString()));
            UnorderedButtons[i].onClick.AddListener(() => DesactiveLetter(numberButtonCopy));
        }

    }


}
