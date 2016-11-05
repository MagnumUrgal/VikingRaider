using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainSceneManager : MonoBehaviour
{
    private GameObject botPanel;
    private GameObject topPanel;
    private GameObject botPanelHover;
    private GameObject EquipagePanel;
    private GameObject AmeliorationPanel;
    private GameObject HistoriquePanel;
    private GameObject MenuPanel;

    //private Button logoButton;
    
    private Button GoldButton;

    // Use this for initialization
    void Start()
    {
        botPanel = GameObject.Find("BotPanelButtons");
        topPanel = GameObject.Find("TopPanelButtons");
        botPanelHover = GameObject.Find("BotPanelHoverButtons");
        GoldButton = GameObject.Find("GoldButton").GetComponent<Button>();
        EquipagePanel = GameObject.Find("EquipagePanel");
        AmeliorationPanel = GameObject.Find("AmeliorationPanel");
        HistoriquePanel = GameObject.Find("HistoriquePanel");
        MenuPanel = GameObject.Find("MenuPanel");
        //logoButton = GameObject.Find("LogoButton").GetComponent<Button>();

        EquipagePanel.SetActive(false);
        AmeliorationPanel.SetActive(false);
        HistoriquePanel.SetActive(false);
        MenuPanel.SetActive(false);

        updateSizeWindow();
    }


    public void updateSizeWindow()
    {
        botPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        botPanelHover.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanelHover.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        topPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        topPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 1.25f, 0);

        //logoButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.height / 10);
    }

    public void OnEquipageButton()
    {
        if (AmeliorationPanel.activeSelf)
        {
            AmeliorationPanel.SetActive(false);
        }
        if (HistoriquePanel.activeSelf)
        {
            HistoriquePanel.SetActive(false);
        }
        if (MenuPanel.activeSelf)
        {
            MenuPanel.SetActive(false);
        }

        if (!EquipagePanel.activeSelf)
        {
            EquipagePanel.SetActive(true);
        }
        else
        {
            EquipagePanel.SetActive(false);
        }
    }

    public void OnAmeliorationButton()
    {
        if (EquipagePanel.activeSelf)
        {
            EquipagePanel.SetActive(false);
        }
        if (HistoriquePanel.activeSelf)
        {
            HistoriquePanel.SetActive(false);
        }
        if (MenuPanel.activeSelf)
        {
            MenuPanel.SetActive(false);
        }

        if (!AmeliorationPanel.activeSelf)
        {
            AmeliorationPanel.SetActive(true);
        }
        else
        {
            AmeliorationPanel.SetActive(false);
        }
    }

    public void OnHistoriqueButton()
    {
        if (EquipagePanel.activeSelf)
        {
            EquipagePanel.SetActive(false);
        }
        if (AmeliorationPanel.activeSelf)
        {
            AmeliorationPanel.SetActive(false);
        }
        if (MenuPanel.activeSelf)
        {
            MenuPanel.SetActive(false);
        }

        if (!HistoriquePanel.activeSelf)
        {
            HistoriquePanel.SetActive(true);
        }
        else
        {
            HistoriquePanel.SetActive(false);
        }
    }

    public void OnMenuButton()
    {
        if (EquipagePanel.activeSelf)
        {
            EquipagePanel.SetActive(false);
        }
        if (AmeliorationPanel.activeSelf)
        {
            AmeliorationPanel.SetActive(false);
        }
        if (HistoriquePanel.activeSelf)
        {
            HistoriquePanel.SetActive(false);
        }

        if (!MenuPanel.activeSelf)
        {
            MenuPanel.SetActive(true);
        }
        else
        {
            MenuPanel.SetActive(false);
        }
    }

    public void OnGoldButton()
    {

    }
}
