using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static StarManager instance { get; private set; }
    private List<Image> stars = new List<Image>();

    [SerializeField] private GameObject starsPrefab;
    [SerializeField] private GameObject starsEmpty;
    [SerializeField] private GameObject background;
    private int starsLevel;
    private int maxStars=3;
    private int actualStar;
    float []fillAmountStars;
    private void Awake()
    {
       
        if(instance != null)
        {
            Destroy(instance.gameObject );
            return;
        }
        instance = this;
        DontDestroyOnLoad( instance );

        actualStar = 0;
        fillAmountStars = new float[maxStars];
       
    }
    public void Update()
    {
        if(stars.Count!=0)
        {
            stars[actualStar].fillAmount = fillAmountStars[actualStar];
        }
        
    }
    public void ResetStars()
    {
        for (int i = 0; i < maxStars; i++)
        {
            fillAmountStars[i] = 0;
        }
        actualStar = 0;
    }
    public void SetStarsOnScene(GameObject backgroundNewScene,int scale)
    {
        stars.Clear();
        RectTransform rectStarsPrefab = starsPrefab.GetComponent<RectTransform>();
        RectTransform backgroundRect = backgroundNewScene.GetComponent<RectTransform>();
        float initialPose = 0;
        initialPose = (initialPose - rectStarsPrefab.rect.width)*scale;
        for (int i = 0; i < 3; i++)
        {
            Image emptyStar =Instantiate(starsEmpty, new Vector3(initialPose + ((rectStarsPrefab.rect.width) * i*scale), -40, 0), Quaternion.identity).GetComponent<Image>();
            emptyStar.transform.SetParent(backgroundNewScene.transform, false); 
            emptyStar.transform.localScale = new Vector2(scale,scale);


            stars.Add(Instantiate(starsPrefab, new Vector3(initialPose + ((rectStarsPrefab.rect.width) * i*scale), -40, 0), Quaternion.identity).GetComponent<Image>());
            stars[i].transform.SetParent(backgroundNewScene.transform, false);
            stars[i].transform.localScale = new Vector2(scale, scale);
            stars[i].fillAmount = fillAmountStars[i];
        }

    }
    public void AddFillAmount(float  amount)
    {
        fillAmountStars[actualStar] += amount;
        if (fillAmountStars[actualStar]>=1)
        {
            fillAmountStars[actualStar] = 1;
            stars[actualStar].fillAmount = fillAmountStars[actualStar++];
            if(actualStar>3)
            {
                actualStar = 3;
            }
        }
        
    }
    public void SetStarsInMap(Image imageLevel,List<Sprite>spriteLevel)
    {
        int Stars = PlayerPrefs.GetInt("Levels");
        imageLevel.sprite= spriteLevel[Stars];
    }
    public int GiveScore()
    {
        float result=0;
        for (int i = 0; i < fillAmountStars.Length; i++)
        {
            result += fillAmountStars[i] * 100.0f;
        }
        return (int)result;
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt("Levels", actualStar);
        PlayerPrefs.Save();
    }
}
