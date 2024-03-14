using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManager : MonoBehaviour
{
    public void play(string scenceName)
    {
        SceneManager.LoadScene(scenceName);
    }
    public void loadScene(string scenceName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scenceName);
    }
}
