using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ResultType { GARNISON, GOLD, DEADSPY, PILLAGEWIN, PILLAGESURRENDER, PILLAGELOST, PILLAGEGAMEOVER };

public class UIMainSceneManager : MonoBehaviour
{
    private GameObject botPanel;
    private GameObject topPanel;
    private GameObject botPanelHover;
    private GameObject EquipagePanel;
    private GameObject AmeliorationPanel;
    private GameObject HistoriquePanel;
    private GameObject MenuPanel;
    private GameObject Notifications;
    private GameObject CityInfoPanel;

    private GameObject fortFortFortifie;
    private GameObject fortPeuFortifie;
    private GameObject fortNonFortifie;

    private Text GarnisonText;
    private Text GoldText;
    private Text DeadSpyText;
    private Text PillageWinText;
    private Text PillageLostText;
    private Text PillageSurrenderText;
    private Text GameOverText;

    //private Button logoButton;
    //private Button GoldButton;

    // Use this for initialization
    void Awake()
    {
        fortFortFortifie = (GameObject)Resources.Load("ville_fortFortifié");
        fortPeuFortifie = (GameObject)Resources.Load("ville_peuFortifié");
        fortNonFortifie = (GameObject)Resources.Load("ville_nonFortifié");

        botPanel = GameObject.Find("BotPanelButtons");
        topPanel = GameObject.Find("TopPanelButtons");
        botPanelHover = GameObject.Find("BotPanelHoverButtons");
        EquipagePanel = GameObject.Find("EquipagePanel");
        AmeliorationPanel = GameObject.Find("AmeliorationPanel");
        HistoriquePanel = GameObject.Find("HistoriquePanel");
        MenuPanel = GameObject.Find("MenuPanel");
        Notifications = GameObject.Find("Notifications");
        CityInfoPanel = GameObject.Find("CityInfoPanel");


        GarnisonText = GameObject.Find("GarnisonText").GetComponent<Text>();
        GoldText = GameObject.Find("GoldText").GetComponent<Text>();
        DeadSpyText = GameObject.Find("DeadSpyText").GetComponent<Text>();
        PillageWinText = GameObject.Find("PillageWinText").GetComponent<Text>();
        PillageLostText = GameObject.Find("PillageLostText").GetComponent<Text>();
        PillageSurrenderText = GameObject.Find("PillageSurrenderText").GetComponent<Text>();
        GameOverText = GameObject.Find("GameOverText").GetComponent<Text>();


        EquipagePanel.SetActive(false);
        AmeliorationPanel.SetActive(false);
        HistoriquePanel.SetActive(false);
        MenuPanel.SetActive(false);
        Notifications.SetActive(false);
        CityInfoPanel.SetActive(false);

        updateSizeWindow();
    }

    void OnGUI()
    {
        if (!EquipagePanel.activeSelf && !AmeliorationPanel.activeSelf && !HistoriquePanel.activeSelf && !MenuPanel.activeSelf && !Notifications.activeSelf 
            && !RectTransformUtility.RectangleContainsScreenPoint(CityInfoPanel.GetComponentInChildren<RectTransform>(), Input.mousePosition))
        {
            RaycastHit hit;
            bool b = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (b && hit.collider.GetComponent<Villes>())
            {
                Villes cursorOnThisVille;
                cursorOnThisVille = hit.collider.GetComponent<Villes>();
                drawVilleInfo(cursorOnThisVille, Input.mousePosition);
            }
            else
            {
                CityInfoPanel.SetActive(false);
            }
        }
    }


    public void updateSizeWindow()
    {
        botPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        botPanelHover.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanelHover.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        topPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        topPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 1.25f, 0);

        Notifications.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2.5f, Screen.height / 2.5f);
    }

    public void updateInfosAfterTour()
    {

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

    //Liaison avec le script Action
    public void DrawSpyResult(int info, ResultType resultType, Espion spy)
    {
        if (resultType == ResultType.GARNISON)
        {
            GarnisonText.text = "Votre espion " + spy.namePerso + " revient victorieux de sa mission d'espionnage dans la ville [ville todo]. Il y a découvert l'étendue des forces présentes : " + info + " hommes.";
        }
        else if (resultType == ResultType.GOLD)
        {
            GoldText.text = "Votre espion " + spy.namePerso + " revient victorieux de sa mission d'espionnage dans la ville [ville todo]. Il y a découvert l'étendue des richesses présentes : " + info + " Or.";
        }
        else if (resultType == ResultType.DEADSPY)
        {
            DeadSpyText.text = "Votre espion " + spy.namePerso + " n'est jamais revenu de sa mission d'espionnage dans la ville [ville todo].";
        }
    }

    public void DrawPillageResult(int goldWin, int pertesSoldiers, int pertesMercenaires, ResultType resultType)
    {
        if(resultType == ResultType.PILLAGEWIN)
        {
            PillageWinText.text = "Votre assaut sur la ville [ville todo] est un succès ! Vous avez gagné " + goldWin + " Or.\n Vous avez perdu " + pertesSoldiers + " soldats. \n Vous avez perdu " + pertesMercenaires + " mercenaires.";
        }
        else if (resultType == ResultType.PILLAGESURRENDER)
        {
            PillageSurrenderText.text = "La ville [ville todo] se rend ! Vous avez gagné " + goldWin + " Or.\n Vous avez perdu " + pertesSoldiers + " soldats. \n Vous avez perdu " + pertesMercenaires + " mercenaires.";
        }
        else if (resultType == ResultType.PILLAGELOST)
        {
            PillageLostText.text = "Ville [ville todo] : Défaite.\n Soldats perdus : " + pertesSoldiers + ".\nMercenaires perdus : " + pertesMercenaires + ".";
        }
        else if (resultType == ResultType.PILLAGEGAMEOVER)
        {
            GameOverText.text = "Vous avez attaqué [ville todo]. C'est une défaite écrasante. Le Valhalla vous accueille.";
        }
    }

    void drawVilleInfo(Villes ville, Vector2 mouseScreenPos)
    {
        if (!CityInfoPanel.activeSelf)
        {
            CityInfoPanel.SetActive(true);
            CityInfoPanel.GetComponent<RectTransform>().anchoredPosition = mouseScreenPos;
            string toPrint = "Nom : " + ville.nameVilles + "\n" + "Fortifications : " + ville.fortification + "\n";
            if (ville.garni_known.is_known)
            {
                if (ville.garni_known.turn_known > 10)
                    toPrint = toPrint + "<color=red> Garnison : " + ville.garni_known.value_known + "</color>\n";

                else if (ville.garni_known.turn_known > 4)
                    toPrint = toPrint + "<color=orange> Garnison : " + ville.garni_known.value_known + "</color>\n";
                else
                    toPrint = toPrint + "<color=green> Garnison : " + ville.garni_known.value_known + "</color>\n";
            }
            if (ville.gold_known.is_known)
            {
                if (ville.gold_known.turn_known > 10)
                    toPrint = toPrint + "<color=red> Or : " + ville.gold_known.value_known + "</color>\n";

                else if (ville.garni_known.turn_known > 4)
                    toPrint = toPrint + "<color=orange> Or : " + ville.gold_known.value_known + "</color>\n";
                else
                    toPrint = toPrint + "<color=green> Or : " + ville.gold_known.value_known + "</color>\n";
            }
            CityInfoPanel.transform.GetChild(0).GetComponent<Text>().text = toPrint;
        }
    }

    //C'est une classe Villes sans s en vrai, ne représente qu'une ville
    public void InstantiateCity(Villes city)
    {
        GameObject obj;
        if (city.fortification == 0)
        {
            obj = (GameObject)Instantiate(fortNonFortifie, city.transform);
        }
        else if(city.fortification == 1)
        {
            obj = (GameObject)Instantiate(fortPeuFortifie, city.transform);
        }
        else
        {
            obj = (GameObject)Instantiate(fortFortFortifie, city.transform);
        }

        obj.name = city.nameVilles + city.fortification;
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = new Vector3(1, 1, 1);
    }

}
