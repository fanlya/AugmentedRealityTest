using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void doplaygame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void doexit()
    {
        Debug.Log("udh keluar");
        Application.Quit();
    }
}
