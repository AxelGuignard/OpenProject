using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    public Button ResumeButton;
    public Button OptionButton;
    public Button QuitButton;

    public Menu optionMenu;

    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(OnClickResume);
        OptionButton.onClick.AddListener(OnClickOption);
        QuitButton.onClick.AddListener(OnClickQuit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickResume()
    {
        Time.timeScale = 1;
        Toggle();
    }

    private void OnClickOption()
    {
        Toggle();
        optionMenu.Toggle();
    }

    private void OnClickQuit()
    {
        SceneManager.LoadScene(GameScene.MainMenu.ToString());
    }
}
