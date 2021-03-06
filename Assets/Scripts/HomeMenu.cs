﻿using System;
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
    public Button MusicListBackButton;

    // Start is called before the first frame update
    void Start()
    {
        //PlayButton.onClick.AddListener(OnClickPlay);
        InstructionButton.onClick.AddListener(LoadInstructionPanel);
        BackButton.onClick.AddListener(Back);
        panelInfo.SetActive(false);
        QuitButton.onClick.AddListener(onClickQuit);
        Button[] difficulties = DificultyPanel.GetComponentsInChildren<Button>();
        foreach (Button difficulty in difficulties)
        {
            if (!difficulty.gameObject.name.Equals("ButtonBack"))
                difficulty.onClick.AddListener(() => { DifficultySelect(difficulty.gameObject.GetComponentInChildren<Text>().text); });
        }
        DificultyBackButton.onClick.AddListener(DificutyBack);
        PlayButton.onClick.AddListener(ShowDifficultyPanel);
        MusicListBackButton.onClick.AddListener(MusicListBack);
        Button[] musics = MusicListPanel.GetComponentsInChildren<Button>();
        foreach (Button music in musics)
        {
            if (!music.gameObject.name.Equals("MusicListBack"))
                music.onClick.AddListener(() => { OnClickPlay(music.gameObject.GetComponentInChildren<Text>().text); });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnClickPlay(string level)
    {
        StaticData.Level = level.ToUpper();
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

    private void DifficultySelect(string difficulty)
    {
        StaticData.Difficulty = difficulty.ToUpper();
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
