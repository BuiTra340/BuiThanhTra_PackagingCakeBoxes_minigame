using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private float time = 45;
    [SerializeField] private Image[] starsImage;
    [SerializeField] private Sprite starWin;
    private float timeCountdown;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private GameObject completePanel;
    [SerializeField] private GameObject failedPanel;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else Destroy(instance.gameObject);
    }
    private void Start()
    {
        timeCountdown = time;
    }
    private void Update()
    {
        if (timeCountdown > 0)
            timeCountdown -= Time.deltaTime;
        else
        {
            timeCountdown = 0;
            gameOver();
        }

        int minutes = Mathf.FloorToInt(timeCountdown / 60);
        int seconds = Mathf.FloorToInt(timeCountdown % 60);
        textTimer.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
    public void loadUIScence(string nameScence)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nameScence);
    }
    public void starRating()
    {
        completePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        int timeStep = (int)time / 3;
        int starData = 0;
        if(timeCountdown < timeStep && timeCountdown > 0)
        {
            starsImage[0].sprite = starWin;
            starData = 1;
        }
        else if(timeCountdown >= timeStep && timeCountdown < timeStep * 2)
        {
            starsImage[0].sprite = starWin;
            starsImage[1].sprite = starWin;
            starData = 2;
        }
        else if(timeCountdown >= timeStep * 2 && timeCountdown < time)
        {
            starsImage[0].sprite = starWin;
            starsImage[1].sprite = starWin;
            starsImage[2].sprite = starWin;
            starData = 3;
        }
        unlockNewLevel(starData);
    }
    public void gameOver() => failedPanel.gameObject.SetActive(true);
    public void unlockNewLevel(int starData)
    {
        Scene currentScence = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt("starData" + currentScence.name, starData);

        int nextLevel = int.Parse(currentScence.name) + 1;
        PlayerPrefs.SetString(nextLevel+"", "unlocked");
    }
}
