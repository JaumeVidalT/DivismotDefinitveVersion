using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLeve : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]Image imagenNivel;

    public void LoadLevel(int level)
    {
       SceneManager.LoadScene(level);
    }
    public void Start()
    {
        StarManager.instance.SetStarsInMap(imagenNivel);
    }
}
