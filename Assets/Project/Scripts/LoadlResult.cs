using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadlResult : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject background;

    void Start()
    {
        StarManager.instance.SetStarsOnScene(background,2);
        
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    // Update is called once per frame
    
}
