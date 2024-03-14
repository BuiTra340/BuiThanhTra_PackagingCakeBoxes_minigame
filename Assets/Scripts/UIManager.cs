using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private Image[] starImages;
    [SerializeField] private Sprite starWin;
    [SerializeField] private GameObject lockPanel;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private bool unlocked;
    private int starData;
    private void Start()
    {
        if (PlayerPrefs.HasKey(level.ToString()))
            unlocked = true;

        if (!unlocked)
        {
            lockPanel.gameObject.SetActive(true);
            textLevel.gameObject.SetActive(false);
            return;
        }
        loadData();
    }

    private void loadData()
    {
        starData = PlayerPrefs.GetInt("starData" + level);
        for (int i = 0; i < starData; i++)
        {
            starImages[i].sprite = starWin;
        }
    }
}
