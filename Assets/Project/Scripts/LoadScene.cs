using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private Button botonContinuar;
    public void Awake()
    {
        if(PlayerPrefs.HasKey("FirstLoad")==false)
        {
            PlayerPrefs.SetInt("FirstLoad", 0);
            PlayerPrefs.SetInt("Levels", 0);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetInt("FirstLoad") == 0)
        {
            botonContinuar.gameObject.SetActive(false);
        }
        else
        {
            botonContinuar.gameObject.SetActive(true);
        }

    }
    public void LoadScenes(int scene)
    {
        
        SceneManager.LoadScene(scene);
    }
    public void NuevaPartida()
    {
        PlayerPrefs.SetInt("FirstLoad", 1);
        PlayerPrefs.SetInt("Levels", 0);
        PlayerPrefs.Save();
    }
    
}
