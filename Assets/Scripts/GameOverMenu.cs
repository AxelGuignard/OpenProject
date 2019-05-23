using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    public Button QuitButton;
    public Text textScore;

    public static GameOverMenu instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        QuitButton.onClick.AddListener(OnClickQuit);
        Toggle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameScene.MainMenu.ToString());
    }

    public static void SetFinalScore()
    {
        Time.timeScale = 0;
        instance.Toggle();
        instance.textScore.text = "Final score : " + GenerateMap.score + "\nBest combo : " + GenerateMap.bestCombo;
    }
}
