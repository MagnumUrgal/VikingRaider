using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ResultType { GARNISON, GOLD, DEADSPY, TREBUCHET, KNIGHTS, EVENT, PILLAGEWIN, PILLAGESURRENDER, PILLAGELOST, PILLAGEGAMEOVER };

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
    private GameObject ChoseSpyPanel;
    private GameObject ReturnFromNotificationButton;
    private GameObject fortFortFortifie;
    private GameObject fortPeuFortifie;
    private GameObject fortNonFortifie;
    private GameObject goldButton;
    private GameObject toursText;
    private GameObject CheatDebugPanel;
    private GameObject IsKnightNotificationPanel;
    private GameObject IsTrebuchetNotificationPanel;
    private GameObject IsEventNotificationPanel;

    private Text GarnisonText;
    private Text GoldText;
    private Text DeadSpyText;
    private Text PillageWinText;
    private Text PillageLostText;
    private Text PillageSurrenderText;
    private Text GameOverText;
    private Text KnightsText;
    private Text TrebText;
    private Text EventText;

    private Villes cursorOnThisVille;
    private Villes VilleConcernedByAction;
    private Actions actionScript;
    private Time timeScript;

    private List<string> historiqueList;

    public Sprite voidSpySprite;

    [HideInInspector]
    public Drakkar drakkar; //is set by the GameManager

    // Use this for initialization
    void Awake()
    {
        actionScript = GameObject.Find("GameManager").GetComponent<Actions>();
        timeScript = GameObject.Find("GameManager").GetComponent<Time>();
        fortFortFortifie = (GameObject)Resources.Load("ville_fortFortifié");
        fortPeuFortifie = (GameObject)Resources.Load("ville_peuFortifié");
        fortNonFortifie = (GameObject)Resources.Load("ville_nonFortifié");

        CheatDebugPanel = GameObject.Find("CheatDebugPanel");
        IsKnightNotificationPanel = GameObject.Find("IsKnightNotificationPanel");
        IsTrebuchetNotificationPanel = GameObject.Find("IsTrebuchetNotificationPanel");
        IsEventNotificationPanel = GameObject.Find("IsEventNotificationPanel");

        botPanel = GameObject.Find("BotPanelButtons");
        topPanel = GameObject.Find("TopPanelButtons");
        botPanelHover = GameObject.Find("BotPanelHoverButtons");
        EquipagePanel = GameObject.Find("EquipagePanel");
        AmeliorationPanel = GameObject.Find("AmeliorationPanel");
        HistoriquePanel = GameObject.Find("HistoriquePanel");
        MenuPanel = GameObject.Find("MenuPanel");
        Notifications = GameObject.Find("Notifications");
        CityInfoPanel = GameObject.Find("CityInfoPanel");
        ChoseSpyPanel = GameObject.Find("ChoseSpyPanel");
        ReturnFromNotificationButton = GameObject.Find("ReturnFromNotificationButton");
        goldButton = GameObject.Find("GoldButton");
        toursText = GameObject.Find("ToursText");

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
        CityInfoPanel.SetActive(false);
        ChoseSpyPanel.SetActive(false);
        Notifications.SetActive(false);
        GarnisonText.transform.parent.gameObject.SetActive(false);
        GoldText.transform.parent.gameObject.SetActive(false);
        DeadSpyText.transform.parent.gameObject.SetActive(false);
        PillageWinText.transform.parent.gameObject.SetActive(false);
        PillageLostText.transform.parent.gameObject.SetActive(false);
        PillageSurrenderText.transform.parent.gameObject.SetActive(false);
        GameOverText.transform.parent.gameObject.SetActive(false);

        IsKnightNotificationPanel.SetActive(false);
        IsTrebuchetNotificationPanel.SetActive(false);
        IsEventNotificationPanel.SetActive(false);

        updateSizeWindow();

        historiqueList = new List<string>();
    }

    void Start()
    {
        updateGoldGUI();
    }

    void OnGUI()
    {
        if (!(CityInfoPanel.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(CityInfoPanel.GetComponentInChildren<RectTransform>(), Input.mousePosition)))
        {
            if (!EquipagePanel.activeSelf && !AmeliorationPanel.activeSelf && !HistoriquePanel.activeSelf && !MenuPanel.activeSelf && !Notifications.activeSelf)
            {
                RaycastHit hit;
                bool b = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
                if (b && hit.collider.GetComponent<Villes>())
                {
                    cursorOnThisVille = hit.collider.GetComponent<Villes>();
                    drawVilleInfo(cursorOnThisVille, Input.mousePosition);
                }
                else
                {
                    cursorOnThisVille = null;
                    CityInfoPanel.SetActive(false);
                }
            }
        }
    }

    public void updateSizeWindow()
    {
        Screen.SetResolution(1616,909, true);//TODO à essayer en build
        botPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        botPanelHover.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanelHover.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        topPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        topPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 1.25f, 0);

        Notifications.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2.5f, Screen.height / 2.5f);
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
            EquipagePanel.transform.GetChild(0).GetComponent<Text>().text = "Viking : " + drakkar.viking.number + ".\nMercenaires : " + drakkar.merc_moyens.number + 
                ".\nEspions : " + drakkar.espion_list.Count + ".";
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
            HistoriquePanel.transform.GetChild(0).GetComponent<Text>().text = "";
            if (historiqueList.Count > 0)
            {
                for (int i = 1; i < 10; i++)
                {
                    if (i <= historiqueList.Count)
                    {
                        HistoriquePanel.transform.GetChild(0).GetComponent<Text>().text += "[Tour " + (timeScript.currentTurn - i) + "] ";
                        HistoriquePanel.transform.GetChild(0).GetComponent<Text>().text += historiqueList[historiqueList.Count - i];
                        HistoriquePanel.transform.GetChild(0).GetComponent<Text>().text += "\n";
                    }
                }
            }
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
    public void DrawSpyResult(int info, ResultType resultType, Espion spy, Villes town)
    {
        if (resultType == ResultType.GARNISON)
        {
            Notifications.SetActive(true);
            GarnisonText.transform.parent.gameObject.SetActive(true);
            ReturnFromNotificationButton.SetActive(true);
            CityInfoPanel.SetActive(false);
            GarnisonText.text = "Votre espion " + spy.namePerso + " revient victorieux de sa mission d'espionnage dans la ville " + town.nameVilles + ". Il y a découvert l'étendue des forces présentes : " + info + " hommes.";
        }
        else if (resultType == ResultType.GOLD)
        {
            Notifications.SetActive(true);
            GoldText.transform.parent.gameObject.SetActive(true);
            ReturnFromNotificationButton.SetActive(true);
            CityInfoPanel.SetActive(false);
            GoldText.text = "Votre espion " + spy.namePerso + " revient victorieux de sa mission d'espionnage dans la ville " + town.nameVilles +  ". Il y a découvert l'étendue des richesses présentes : " + info + " Or.";
        }
        else if (resultType == ResultType.DEADSPY)
        {
            Notifications.SetActive(true);
            DeadSpyText.transform.parent.gameObject.SetActive(true);
            ReturnFromNotificationButton.SetActive(true);
            CityInfoPanel.SetActive(false);
            DeadSpyText.text = "Votre espion " + spy.namePerso + " n'est jamais revenu de sa mission d'espionnage dans la ville " + town.nameVilles + ".";
        }
        addEspionnageToHistoriqueList(info, resultType, spy, town);
    }

    public void DrawSpySpecial(string info, ResultType resultType, Espion spy, Villes town)
    {
        if (resultType == ResultType.KNIGHTS)
        {
            IsKnightNotificationPanel.SetActive(true);
            IsKnightNotificationPanel.GetComponent<Text>().text = info;
            // A FINIR
        } 
        else if (resultType == ResultType.TREBUCHET)
        {
            IsTrebuchetNotificationPanel.SetActive(true);
            IsTrebuchetNotificationPanel.GetComponent<Text>().text = info;
            // A FINIR
        }
        else if (resultType == ResultType.EVENT)
        {
            IsEventNotificationPanel.SetActive(true);
            IsEventNotificationPanel.GetComponent<Text>().text = info;
            // A FINIR
        }
    }

    public void DrawPillageResult(int goldWin, int pertesSoldiers, int pertesMercenaires, ResultType resultType, Villes town)
    {
        if (resultType == ResultType.PILLAGEWIN)
        {
            Notifications.SetActive(true);
            PillageWinText.transform.parent.gameObject.SetActive(true);
            ReturnFromNotificationButton.SetActive(true);
            CityInfoPanel.SetActive(false);
            PillageWinText.text = "Votre assaut sur la ville " + town.nameVilles + " est un succès ! \nVous avez gagné " + goldWin + " Or.\n Vous avez perdu " + pertesSoldiers + " soldats. \n Vous avez perdu " + pertesMercenaires + " mercenaires.";
        }
        else if (resultType == ResultType.PILLAGESURRENDER)
        {
            Notifications.SetActive(true);
            PillageSurrenderText.transform.parent.gameObject.SetActive(true);
            ReturnFromNotificationButton.SetActive(true);
            CityInfoPanel.SetActive(false);
            PillageSurrenderText.text = "La ville " + town.nameVilles + " se rend ! \nVous avez gagné " + goldWin + " Or.\n Vous avez perdu " + pertesSoldiers + " soldats. \n Vous avez perdu " + pertesMercenaires + " mercenaires.";
        }
        else if (resultType == ResultType.PILLAGELOST)
        {
            Notifications.SetActive(true);
            PillageLostText.transform.parent.gameObject.SetActive(true);
            ReturnFromNotificationButton.SetActive(true);
            CityInfoPanel.SetActive(false);
            PillageLostText.text = "Ville " + town.nameVilles + " : Défaite.\nSoldats perdus : " + pertesSoldiers + ".\nMercenaires perdus : " + pertesMercenaires + ".";
        }
        else if (resultType == ResultType.PILLAGEGAMEOVER)
        {
            Notifications.SetActive(true);
            GameOverText.transform.parent.gameObject.SetActive(true);
            ReturnFromNotificationButton.SetActive(true);
            CityInfoPanel.SetActive(false);
            GameOverText.text = "Vous avez attaqué " + town.nameVilles + ". \nC'est une défaite écrasante. \nLe Valhalla vous accueille.";
        }
        addPillageToHistoriqueList(goldWin, pertesSoldiers, pertesMercenaires, resultType, town);
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
                if (timeScript.currentTurn - ville.garni_known.turn_known > 10)
                    toPrint = toPrint + "<color=red> Garnison : " + ville.garni_known.value_known + "</color>\n";

                else if (timeScript.currentTurn - ville.garni_known.turn_known > 4)
                    toPrint = toPrint + "<color=orange> Garnison : " + ville.garni_known.value_known + "</color>\n";
                else
                    toPrint = toPrint + "<color=green> Garnison : " + ville.garni_known.value_known + "</color>\n";
            }
            if (ville.gold_known.is_known)
            {
                if (timeScript.currentTurn - ville.gold_known.turn_known > 10)
                    toPrint = toPrint + "<color=red> Or : " + ville.gold_known.value_known + "</color>\n";

                else if (timeScript.currentTurn - ville.garni_known.turn_known > 4)
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

    public void OnSpyButton()
    {
        if(cursorOnThisVille != null)
        {
            VilleConcernedByAction = cursorOnThisVille;
            ChoseSpyPanel.SetActive(true);
            int i = 0;
            foreach (Espion e in drakkar.espion_list)
            {
                //Les sprites des espions doivent porter le nom exact du personnage
                ChoseSpyPanel.transform.GetChild(i).GetComponent<Image>().sprite = (Sprite)Resources.Load(e.namePerso);
                i++;
            }
            if(drakkar.espion_list.Count < 3)
            {
                ChoseSpyPanel.transform.GetChild(2).GetComponent<Image>().sprite = voidSpySprite;
            }
            if(drakkar.espion_list.Count < 2)
            {
                ChoseSpyPanel.transform.GetChild(1).GetComponent<Image>().sprite = voidSpySprite;
            }
            if (drakkar.espion_list.Count == 0)
            {
                ChoseSpyPanel.transform.GetChild(0).GetComponent<Image>().sprite = voidSpySprite;
            }
        }
    }

    public void OnSpyOneButton()
    {
        if(drakkar.espion_list.Count > 0)
        {
            actionScript.Espionnage(drakkar, drakkar.espion_list[0], VilleConcernedByAction,timeScript);
            VilleConcernedByAction = null;
            ChoseSpyPanel.SetActive(false);
        }
    }

    public void OnSpyTwoButton()
    {
        if (drakkar.espion_list.Count > 1)
        {
            actionScript.Espionnage(drakkar, drakkar.espion_list[1], VilleConcernedByAction, timeScript);
            VilleConcernedByAction = null;
            ChoseSpyPanel.SetActive(false);
        }

    }

    public void OnSpyThreeButton()
    {
        if (drakkar.espion_list.Count > 2)
        {
            actionScript.Espionnage(drakkar, drakkar.espion_list[2], VilleConcernedByAction, timeScript);
            VilleConcernedByAction = null;
            ChoseSpyPanel.SetActive(false);
        }
    }

    public void OnReturnSpyButton()
    {
        VilleConcernedByAction = null;
        ChoseSpyPanel.SetActive(false);
    }

    public void OnReturnFromNotificationButton()
    {
        Notifications.SetActive(false);
        GarnisonText.transform.parent.gameObject.SetActive(false);
        GoldText.transform.parent.gameObject.SetActive(false);
        DeadSpyText.transform.parent.gameObject.SetActive(false);
        PillageWinText.transform.parent.gameObject.SetActive(false);
        PillageLostText.transform.parent.gameObject.SetActive(false);
        PillageSurrenderText.transform.parent.gameObject.SetActive(false);
        GameOverText.transform.parent.gameObject.SetActive(false);
        ReturnFromNotificationButton.SetActive(false);
        updateGoldGUI();
    }

    public void OnRaidButton()
    {
        if (cursorOnThisVille != null)
        {
            VilleConcernedByAction = cursorOnThisVille;
            if (!VilleConcernedByAction.is_king)
            {
                actionScript.Pillage(drakkar, VilleConcernedByAction, timeScript);
            }
            else
            {
                actionScript.BattleRoyale(drakkar);
            }
        }
    }

    public void updateGoldGUI()
    {
        goldButton.transform.GetChild(0).GetComponent<Text>().text = "<color=white><b>" + drakkar.gold + "</b></color>";
    }

    public void updateTurnsGUI(int currentTurn, int maxTurn)
    {
        toursText.GetComponent<Text>().text = "<color=white><b>" + currentTurn + "  /  " + maxTurn + "</b></color>";
    }

    public void Update()
    {
        if (cursorOnThisVille != null)
        {
            CheatDebugPanel.transform.GetChild(0).GetComponent<Text>().text = "Last selected city :\nGarnison : " +
                cursorOnThisVille.garnison + "\nOr : " + cursorOnThisVille.gold + "\nTrebuchets : " + cursorOnThisVille.is_trebuchet + "\nIs_event : " + cursorOnThisVille.is_event +
                "\nPerception : " + cursorOnThisVille.perception +"\nevID : " + cursorOnThisVille.current_event +"\nknights : " + cursorOnThisVille.is_knights;
        }
    }

    void addPillageToHistoriqueList(int goldWin, int pertesSoldiers, int pertesMercenaires, ResultType resultType, Villes town)
    {
        string toPrint = "Le pillage sur " + town.nameVilles;
        if(resultType == ResultType.PILLAGEWIN)
        {
            toPrint += " a rapporté " + goldWin + " Or, a coûté " + pertesSoldiers + " vikings et " + pertesMercenaires + " mercenaires.";
        }
        else if (resultType == ResultType.PILLAGESURRENDER)
        {
            toPrint += " a rapporté " + goldWin + " Or, a coûté " + pertesSoldiers + " vikings et " + pertesMercenaires + " mercenaires. Le village s'est rendu.";
        }
        else if (resultType == ResultType.PILLAGELOST)
        {
            toPrint += " était une cuisante défaite. Vous avez perdu " + pertesSoldiers + " vikings et " + pertesMercenaires + " mercenaires.";
        }
        else if (resultType == ResultType.PILLAGEGAMEOVER)
        {
            toPrint += " était une cuisante défaite. Vous avez perdu " + pertesSoldiers + " vikings et " + pertesMercenaires + " mercenaires. Vous avez tout perdu.";
        }
        historiqueList.Add(toPrint);
    }

    void addEspionnageToHistoriqueList(int info, ResultType resultType, Espion spy, Villes town)
    {
        if(resultType == ResultType.GOLD)
        {
            historiqueList.Add(town.nameVilles + " possède " + town.gold_known.value_known + " Or. Merci " + spy.namePerso + ".");
        }
        else if (resultType == ResultType.GARNISON)
        {
            historiqueList.Add(town.nameVilles + " possède " + town.garni_known.value_known + " hommes. Merci " + spy.namePerso + ".");
        }
        else if (resultType == ResultType.DEADSPY)
        {
            historiqueList.Add(spy.namePerso + ", " + town.nameVilles + ", RIP.");
        }
    }

    public void OnReturnTrebuchet()
    {
        IsTrebuchetNotificationPanel.SetActive(false);
    }

    public void OnReturnKnights()
    {
        IsKnightNotificationPanel.SetActive(false);
    }

    public void OnReturnEvents()
    {
        IsEventNotificationPanel.SetActive(false);
    }
}
