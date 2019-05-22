using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text score;
    public Text combo;
    public Text startMessage;

    private static HUD instance;
    private AudioSource audioSource;

    public AudioClip loseComboClip;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetScore(int scoreVal)
    {
        instance.score.text = "Score : " + scoreVal;
    }

    public static void SetCombo(int comboVal)
    {
        instance.combo.text = "Combo : " + comboVal;
    }

    public static void playLoseComboSound()
    {
        instance.audioSource.clip = instance.loseComboClip;
        instance.audioSource.Play();
    }

    public static void HideStartMessage()
    {
        instance.startMessage.enabled = false;
    }
}
