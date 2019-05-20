using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : Menu
{
    public static MainMenuManager instance;

    public HomeMenu homeMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<MainMenuManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GameScene
{
    MainMenu,
    game
}