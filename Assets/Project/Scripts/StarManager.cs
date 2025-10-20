using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static StarManager instance { get; private set; }
    private List<Image> stars = new List<Image>();

    [SerializeField] private GameObject starsPrefab;
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
        for(int i = 0; i < maxStars; i++)
        {
            fillAmountStars[i] = 0;
        }
       
    }
    public void Update()
    {
        stars[actualStar].fillAmount = fillAmountStars[actualStar];
    }
    public void SetStarsOnScene(GameObject backgroundNewScene)
    {
        stars.Clear();
        RectTransform rectStarsPrefab = starsPrefab.GetComponent<RectTransform>();
        RectTransform backgroundRect = backgroundNewScene.GetComponent<RectTransform>();
        float initialPose = 0;
        initialPose = initialPose - rectStarsPrefab.rect.width;
        for (int i = 0; i < 3; i++)
        {
            stars.Add(Instantiate(starsPrefab, new Vector3(initialPose + ((rectStarsPrefab.rect.width) * i), -40, 0), Quaternion.identity).GetComponent<Image>());
            stars[i].transform.SetParent(backgroundNewScene.transform, false);
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
        }
        
    }
}
