using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadlResult : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject background;
    [SerializeField] private Image ScoreBackground;
    [SerializeField] private List<Sprite> scoreSprites = new List<Sprite>();
    [SerializeField] private TextMeshProUGUI textoScore;

    void Start()
    {
        StarManager.instance.SaveScore();
        StarManager.instance.SetStarsInMap(ScoreBackground, scoreSprites);
        textoScore.text= StarManager.instance.GiveScore().ToString();



    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    // Update is called once per frame
    
}
