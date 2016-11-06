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
    private GameObject IsKnightNotificationPanel;
    private GameObject IsTrebuchetNotificationPanel;
    private GameObject IsEventNotificationPanel;
    private GameObject BuyEspionPanel;
    
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

    private List<Upgrades> UpList;
    private int currentSelectedEspion;

    private Villes cursorOnThisVille;
    private Villes VilleConcernedByAction;
    private Actions actionScript;
    private Time timeScript;
    private GameManager gameManager;

    private List<string> historiqueList;

    [HideInInspector]
    public Drakkar drakkar; //is set by the GameManager

    // Use this for initialization
    void Awake()
    {
        actionScript = GameObject.Find("GameManager").GetComponent<Actions>();
        timeScript = GameObject.Find("GameManager").GetComponent<Time>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        fortFortFortifie = (GameObject)Resources.Load("ville_fortFortifié");
        fortPeuFortifie = (GameObject)Resources.Load("ville_peuFortifié");
        fortNonFortifie = (GameObject)Resources.Load("ville_nonFortifié");
        
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
        BuyEspionPanel = GameObject.Find("BuyEspionPanel");
        currentSelectedEspion = 0;

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
        BuyEspionPanel.SetActive(false);

        IsKnightNotificationPanel.SetActive(false);
        IsTrebuchetNotificationPanel.SetActive(false);
        IsEventNotificationPanel.SetActive(false);

        updateSizeWindow();

        historiqueList = new List<string>();
        UpList = new List<Upgrades>();
    }

    void Start()
    {
        updateGoldGUI();
        AmeliorationPanel.transform.GetChild(0).gameObject.AddComponent<Upgrades>();
        AmeliorationPanel.transform.GetChild(0).GetComponent<Upgrades>().upgradeType = UpgradeType.CASSIMP;
        AmeliorationPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("CasqueSimple");
        AmeliorationPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "20 000";
        AmeliorationPanel.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "Par Odin ! C’est un casque Lebohaume !";

        AmeliorationPanel.transform.GetChild(1).gameObject.AddComponent<Upgrades>();
        AmeliorationPanel.transform.GetChild(1).GetComponent<Upgrades>().upgradeType = UpgradeType.BOTFOUR;
        AmeliorationPanel.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("BotteFourree");
        AmeliorationPanel.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "20 000";
        AmeliorationPanel.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Le confort de la maison même au combat.";

        AmeliorationPanel.transform.GetChild(2).gameObject.AddComponent<Upgrades>();
        AmeliorationPanel.transform.GetChild(2).GetComponent<Upgrades>().upgradeType = UpgradeType.HACGUER;
        AmeliorationPanel.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("HacheGuerre");
        AmeliorationPanel.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "30 000";
        AmeliorationPanel.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "Woaw, c’est une durandil ! Une vraie de vraie ! D’occasion !";

        AmeliorationPanel.transform.GetChild(3).gameObject.AddComponent<Upgrades>();
        AmeliorationPanel.transform.GetChild(3).GetComponent<Upgrades>().upgradeType = UpgradeType.BARDE;
        AmeliorationPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("Barde");
        AmeliorationPanel.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "40 000";
        AmeliorationPanel.transform.GetChild(3).GetChild(2).GetComponent<Text>().text = "Parce qu'avec son chant, on n'entend pas vos hurlements.";
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
        Screen.SetResolution(1616, 909, true);//TODO à essayer en build
        botPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        botPanelHover.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        botPanelHover.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 8, 0);

        topPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 15, Screen.width / 15);
        topPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(Screen.width / 1.25f, 0);

        Notifications.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2.5f, Screen.height / 2.5f);
    }

    public void EspionOwnedDisplayStart()
    {
        if (gameManager.drakkar.espion_list[0].namePerso == "Blake")
            BuyEspionPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
        if (gameManager.drakkar.espion_list[0].namePerso == "Flantier")
            BuyEspionPanel.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
        if (gameManager.drakkar.espion_list[0].namePerso == "Sammy")
            BuyEspionPanel.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
        if (gameManager.drakkar.espion_list[0].namePerso == "Willy")
            BuyEspionPanel.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
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
            updateDrawEquipageBilan();
        }
        else
        {
            EquipagePanel.SetActive(false);
        }
    }

    public void updateDrawEquipageBilan()
    {
        EquipagePanel.transform.GetChild(0).GetComponent<Text>().text = "Viking : " + drakkar.viking.number + ".\nMercenaires : " + drakkar.merc_moyens.number +
            ".\nEspions : " + drakkar.espion_list.Count + ".";
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
            GoldText.text = "Votre espion " + spy.namePerso + " revient victorieux de sa mission d'espionnage dans la ville " + town.nameVilles + ". Il y a découvert l'étendue des richesses présentes : " + info + " Or.";
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
        Notifications.SetActive(true);
        if (resultType == ResultType.KNIGHTS)
        {
            IsKnightNotificationPanel.SetActive(true);
            IsKnightNotificationPanel.GetComponentInChildren<Text>().text = info;
        }
        else if (resultType == ResultType.TREBUCHET)
        {
            IsTrebuchetNotificationPanel.SetActive(true);
            IsTrebuchetNotificationPanel.GetComponentInChildren<Text>().text = info;
        }
        else if (resultType == ResultType.EVENT)
        {
            IsEventNotificationPanel.SetActive(true);
            IsEventNotificationPanel.GetComponentInChildren<Text>().text = info;
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
            string toPrint = ville.nameVilles + "\n" + "Fortifications : " + ville.fortification + "\n";
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
        else if (city.fortification == 1)
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
        if (cursorOnThisVille != null)
        {
            VilleConcernedByAction = cursorOnThisVille;
            ChoseSpyPanel.SetActive(true);
            int i = 0;
            foreach (Espion e in drakkar.espion_list)
            {
                //Les sprites des espions doivent porter le nom exact du personnage
                ChoseSpyPanel.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(e.namePerso);
                i++;
            }
            if (drakkar.espion_list.Count < 3)
            {
                ChoseSpyPanel.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
            }
            if (drakkar.espion_list.Count < 2)
            {
                ChoseSpyPanel.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
            }
            if (drakkar.espion_list.Count == 0)
            {
                ChoseSpyPanel.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
            }
        }
    }

    public void OnSpyOneButton()
    {
        if (drakkar.espion_list.Count > 0)
        {
            actionScript.Espionnage(drakkar, drakkar.espion_list[0], VilleConcernedByAction, timeScript);
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

    void addPillageToHistoriqueList(int goldWin, int pertesSoldiers, int pertesMercenaires, ResultType resultType, Villes town)
    {
        string toPrint = "Le pillage sur " + town.nameVilles;
        if (resultType == ResultType.PILLAGEWIN)
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
        if (resultType == ResultType.GOLD)
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

    public void OnFirstPosition()
    {
        GameObject Amelioration = AmeliorationPanel.transform.GetChild(0).gameObject;
        if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.CASSIMP)
        {
            if (drakkar.gold >= 20000)
            {
                drakkar.gold = drakkar.gold - 20000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "def", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.CASCORN;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("CasqueCorne");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = "30 000";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = "CROOOOM";
            }
        }
        else if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.CASCORN)
        {
            if (drakkar.gold >= 30000)
            {
                drakkar.gold = drakkar.gold - 30000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "def", 1);
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "intimidate", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.CASEMBR;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("CasqueEmbrase");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = "50 000";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = "Les légions de Hel marchent désormais sur le monde.";
            }
        }
        else if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.CASEMBR)
        {
            if (drakkar.gold >= 50000)
            {
                drakkar.gold = drakkar.gold - 50000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "def", 1);
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "intimidate", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.NOTHING;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("ItemNothing");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = " ";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = " ";
            }
        }
        updateGoldGUI();
    }

    public void OnSecondPosition()
    {
        GameObject Amelioration = AmeliorationPanel.transform.GetChild(1).gameObject;
        if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.BOTFOUR)
        {
            if (drakkar.gold >= 20000)
            {
                drakkar.gold = drakkar.gold - 20000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "moral", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.MASCOT;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("Mascotte");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = "20000";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = "C’est mignon, c’est chou, mais notre virilité en prend un coup.";
            }
        }
        else if(Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.MASCOT)
        {
            if (drakkar.gold >= 20000)
            {
                drakkar.gold = drakkar.gold - 20000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "moral", 1);
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "intimidate", -1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.NOTHING;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("ItemNothing");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = " ";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = " ";
            }
        }
        updateGoldGUI();
    }

    public void OnThirdPosition()
    {
        GameObject Amelioration = AmeliorationPanel.transform.GetChild(2).gameObject;
        if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.HACGUER)
        {
            if (drakkar.gold >= 30000)
            {
                drakkar.gold = drakkar.gold - 30000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "atk", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.COTMAIL;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("CotteMaille");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = "40000";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = "Pour quoi faire ? La douleur n’est qu’une simple information.";
            }
        }
        else if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.COTMAIL)
        {
            if (drakkar.gold >= 40000)
            {
                drakkar.gold = drakkar.gold - 40000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "def", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.ULBSWORD;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("UlbertSword");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = "80000";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = "L'épée encore plus énervée que le viking qui la porte.";
            }
        }
        else if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.ULBSWORD)
        {
            if (drakkar.gold >= 80000)
            {
                drakkar.gold = drakkar.gold - 80000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "atk", 3);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.NOTHING;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("ItemNothing");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = " ";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = " ";
            }
        }
        updateGoldGUI();
    }

    public void OnFourthPosition()
    {
        GameObject Amelioration = AmeliorationPanel.transform.GetChild(3).gameObject;
        if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.BARDE)
        {
            if (drakkar.gold >= 40000)
            {
                drakkar.gold = drakkar.gold - 40000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "moral", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.CUISIN;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("Cuisinier");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = "50000";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = "Quand même meilleur que le brouet aux endives de Bior.";
            }
        }
        else if (Amelioration.GetComponent<Upgrades>().upgradeType == UpgradeType.CUISIN)
        {
            if (drakkar.gold >= 50000)
            {
                drakkar.gold = drakkar.gold - 50000;
                Amelioration.GetComponent<Upgrades>().UpgradeStat(drakkar, "moral", 1);
                Amelioration.GetComponent<Upgrades>().upgradeType = UpgradeType.NOTHING;
                Amelioration.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("ItemNothing");
                Amelioration.transform.GetChild(1).GetComponent<Text>().text = " ";
                Amelioration.transform.GetChild(2).GetComponent<Text>().text = " ";
            }
        }

        updateGoldGUI();
    }

    public void OnBuyEspion()
    {
        BuyEspionPanel.SetActive(true);
    }

    public void OnFireMercenaire()
    {
        drakkar.merc_moyens.sub(1);
    }

    public void OnBuyMercenaire()
    {
        if (drakkar.gold > 500)
        {
            drakkar.gold = drakkar.gold - 500;
            updateGoldGUI();
            drakkar.merc_moyens.add(1);
            updateDrawEquipageBilan();
        }
    }

    public void OnBuyViking()
    {
        if (drakkar.gold > 5000)
        {
            drakkar.gold = drakkar.gold - 5000;
            updateGoldGUI();
            drakkar.viking.add(1);
            updateDrawEquipageBilan();
        }
    }

    public void OnBuyConfirmedEspion()
    {
        if (drakkar.gold > 15000)
        {
            gameManager.drakkar.gold = drakkar.gold - 5000;
            updateGoldGUI();
            if (currentSelectedEspion == 1)
            {
                gameManager.drakkar.espion_list.Add(gameManager.Blake);
            }
            else if(currentSelectedEspion == 2)
            {
                gameManager.drakkar.espion_list.Add(gameManager.Flantier);
            }
            else if (currentSelectedEspion == 3)
            {
                gameManager.drakkar.espion_list.Add(gameManager.Sammy);
            }
            else if (currentSelectedEspion == 4)
            {
                gameManager.drakkar.espion_list.Add(gameManager.Willy);
            }
            
            
            BuyEspionPanel.transform.GetChild(0).GetChild(currentSelectedEspion - 1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Espion_block");
            updateDrawEquipageBilan();
            Debug.Log(drakkar.espion_list.Count);
            BuyEspionPanel.SetActive(false);
        }
    }

    public void OnCancelBuyEspion()
    {
        BuyEspionPanel.SetActive(false);
        currentSelectedEspion = 0;
    }

    //ShopList = la liste des espions que je n'ai pas
    public void OnCharacterSelected(int i)
    {
        currentSelectedEspion = i;
    }

    public void OnDragonTail()
    {

    }

    public void defeat()
    {

    }
}
