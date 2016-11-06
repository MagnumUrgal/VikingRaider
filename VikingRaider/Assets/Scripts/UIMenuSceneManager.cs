using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Ne rien passer en invisible, tout doit être en SetActive(true) dans l'Inspector au lancement de la scène.
public class UIMenuSceneManager : MonoBehaviour {
    private GameObject initialMenuPanel;
    private GameObject highScoresPanel;
    private GameObject highScoresReturnButton;

    void Start () {
        SoundManager.PlayMusique("menu");
        initialMenuPanel = GameObject.Find("InitialMenuPanel");
        highScoresPanel = GameObject.Find("HighScoresPanel");
        highScoresReturnButton = GameObject.Find("HighScoresReturnButton");
        highScoresPanel.SetActive(false);
        highScoresReturnButton.SetActive(false);
        updateSizeWindow();
	}

    public void updateSizeWindow()
    {
        initialMenuPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width/4, Screen.height/10);
        initialMenuPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, Screen.height / 15);

        highScoresPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 4, Screen.height / 10);
        highScoresPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 12, Screen.height / 15);

        highScoresReturnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 12, Screen.height / 20);
    }

    public void OnHighScoresButton()
    {
        highScoresPanel.SetActive(true);
        initialMenuPanel.SetActive(false);
        highScoresReturnButton.SetActive(true);
    }

    public void OnHighScoresReturnButton()
    {
        highScoresPanel.SetActive(false);
        initialMenuPanel.SetActive(true);
        highScoresReturnButton.SetActive(false);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
