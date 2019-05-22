using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenu : Menu
{
    public MainMenuManager mainMenuManager;

    public Button PlayButton;
    public Button OptionButton;
    public Button QuitButton;
    public Button InstructionButton;
    public Button BackButton;
    public GameObject panelInfo;

    public GameObject MainPanel;
    public GameObject DificultyPanel;
    public Button EasyButton;
    public Button NormalButton;
    public Button HardButton;
    public Button DificultyBackButton;

    public GameObject MusicListPanel;
    public Button Music1Button;
    public Button MusicListBackButton;

    // Start is called before the first frame update
    void Start()
    {
        //PlayButton.onClick.AddListener(OnClickPlay);
        InstructionButton.onClick.AddListener(LoadInstructionPanel);
        BackButton.onClick.AddListener(Back);
        panelInfo.SetActive(false);
        QuitButton.onClick.AddListener(onClickQuit);
        DificultyBackButton.onClick.AddListener(DificutyBack);
        PlayButton.onClick.AddListener(ShowDifficultyPanel);
        EasyButton.onClick.AddListener(EasyClick);
        MusicListBackButton.onClick.AddListener(MusicListBack);
        Music1Button.onClick.AddListener(OnClickPlay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnClickPlay()
    {
        SceneManager.LoadScene(GameScene.game.ToString());
    }


    private void LoadInstructionPanel()
    {
        panelInfo.SetActive(true);
        //WriteStuff();
    }

    private void ShowDifficultyPanel()
    {
        DificultyPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    private void DificutyBack()
    {
        MainPanel.SetActive(true);
        DificultyPanel.SetActive(false);
    }

    private void EasyClick()
    {
        MusicListPanel.SetActive(true);
        DificultyPanel.SetActive(false);
    }

    private void MusicListBack()
    {
        DificultyPanel.SetActive(true);
        MusicListPanel.SetActive(false);
    }

    private void Back()
    {
        panelInfo.SetActive(false);
    }

    private void onClickQuit()
    {
        Application.Quit();
    }
}
