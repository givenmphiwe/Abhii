using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int howManyPlayers;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TwoPlayers()
    {
        SoundManager.buttonAudioSource.Play();
        howManyPlayers = 2;
        SceneManager.LoadScene("Ngendo");

    }
    public void ThreePlayers()
    {
        SoundManager.buttonAudioSource.Play();
        howManyPlayers = 3;
        SceneManager.LoadScene("Ngendo");
    }
    public void FourPlayers()
    {
        SoundManager.buttonAudioSource.Play();
        howManyPlayers = 4;
        SceneManager.LoadScene("Ngendo");
    }
    public void quit()
    {
        SoundManager.buttonAudioSource.Play();
        Application.Quit();
    }
}

