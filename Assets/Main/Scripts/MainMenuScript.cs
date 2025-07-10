using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(play);
        quitButton.onClick.AddListener(quit);
    }

   
    void play()
    {
        SceneManager.LoadScene("Main");
    }
    void quit()
    {
        Application.Quit();
    }
}
