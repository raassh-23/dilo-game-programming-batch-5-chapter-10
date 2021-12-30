using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    public Text debug;
    public void ButtonClicked(string buttonName)
    {
        if (buttonName == "problem-9")
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            OpenBuild(buttonName);
        }
    }

    private void OpenBuild(string name)
    {
        string buildPath = $"{Application.streamingAssetsPath}/{name}/dilo-game-programming-batch-5-chapter-10.exe";
        debug.text = "Opening " + buildPath + "\n"
        + "Try alt + tab if problem build doesn't show up after a while";

        try
        {
            System.Diagnostics.Process.Start(buildPath);
        }
        catch (System.Exception e)
        {
            debug.text = "Open failed\n" + e.Message;
        }
    }
}
