using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
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
     private List<Word> wordManager = new List<Word>();
    private float DistanceWithBorders = 25;
    [SerializeField] GameObject buttonPrefab;
    void Start()
    {
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

        wordOrder = 0;

        SetNewButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (wordSize == playerWord.Length)
        {
            if (wordManager[wordOrder].CheckCorrectWord(playerWord))
            {
                Debug.Log("Victory");
            }
            ++wordOrder;
            CleanButtons();
            SetNewButtons();
        }
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
        OrderedButtons[letterPosition].gameObject.SetActive(true);
        OrderedButtons[letterPosition].GetComponentInChildren<TextMeshProUGUI>().text = letter ;
        playerWord += letter;
        ++letterPosition;
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
        wordSize = wordManager[wordOrder].GetCorrectWord().Length;

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

            char letter = wordManager[wordOrder].GetDesorderedWord()[i];
            UnorderedButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();

            UnorderedButtons[i].onClick.RemoveAllListeners();

            char letterCopy = letter;
            UnorderedButtons[i].onClick.AddListener(() => AddLetter(letterCopy.ToString()));
        }

    }


}
