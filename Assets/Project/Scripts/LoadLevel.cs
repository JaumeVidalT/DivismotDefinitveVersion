using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLeve : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]Button imagenNivel;
    [SerializeField] private List<Sprite> levelSprites = new List<Sprite>();

    public void LoadLevel(int level)
    {
       SceneManager.LoadScene(level);
    }
    public void Start()
    {
        StarManager.instance.SetStarsInMap(imagenNivel, levelSprites);
    }
}
