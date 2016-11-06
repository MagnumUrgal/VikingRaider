using UnityEngine;
using System.Collections;
using System;

public class Time : MonoBehaviour
{
    public int currentTurn { get; set; }
    public bool event_occuring { get; set; }
    public int raidcount { get; set; }
    public int max_turn { get; set; }
    public Events current_event { get; set; }
    public int CurrentEventTurn { get; set; }
    public string CurrentEventTown { get; set; }

    private UIMainSceneManager UIManager;

    public Soldat no_one;
    public Soldat escorte;
    public Soldat probleme;

    public townEvents mariage;
    public townEvents epidemie;
    public townEvents priest;
    public townEvents famine;
    public Events Hollandais;
    public Events None;

    public int factProba;

    public Time()
    {
        currentTurn = 0;
        event_occuring = false;
        raidcount = 0;
        max_turn = 42;
        CurrentEventTurn = 0;
    }

    void Start()
    {
        UIManager = GameObject.Find("Canvas").GetComponent<UIMainSceneManager>();
        UIManager.updateTurnsGUI(1, 42);
        no_one = new Soldat(0, 0, 0, 0, 0, "personne");
        escorte = new Soldat(1, 1, 1, 1, 35, "escortemariage");
        probleme = new Soldat(1, 1, 1, 1, -20, "pb");
        mariage = new townEvents(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, "Un mariage de personnes importantes va bientôt se dérouler en ville. Je pense qu’il y aura pas mal de personne fortunées présentes, avec aussi un peu plus de soldats, mais ça reste des soldats, hin hin hin.", escorte, 1.2f);
        epidemie = new townEvents(5, 0, 0, -1, 0, 0, 0, 0, 0, 0, 2, "Ca craint, la ville a été touchée par une épidémie, et croyez moi c’est pas beau à voir.Bon du coup ça va être plus facile de voler, ils sont bien trop mal en point pour se défendre correctement !", probleme, 1f);
        priest = new townEvents(2,1,0,50,0,0,0,0,0,0,3, "Ces fanatiques ont galvanisé leurs paysans en sacrifiant diverses bestioles (et aussi des gens je crois) à leur Dieu, et bah je peux vous dire que ça marche plutôt pas mal, maintenant ce sont des paysans de compète, du genre dopé aux amphétamines !",no_one,1f);
        famine = new townEvents(4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, "Dans cette ville ils n’ont déjà pas assez de bouffe pour subvenir aux besoins de tous les habitants. Oui c’est la misère. Le pillage sera pas terrible mais au moins ils sont déjà affaiblis.",probleme, 0.6f);
        Hollandais = new Events(2, 0, 0, -2, 0, 5,"Hollandais volant", no_one);
        None = new Events(0, 0, 0, 0, 0, 0, "Rien d'intéressant", no_one);

        factProba = 10;
    }

    public void applyEventCity(townEvents ev, Villes city)
    {
        CurrentEventTurn = ev.nbRound;
        city.current_event = ev.id;
        city.is_event = true;

        Soldat army = city.garnison;
        army.atk += ev.modifAtk;
        army.def += ev.modifDef;
        army.moral += ev.modifMoral;
        army.intimidate += ev.modifIntim;
        army.number += ev.soldiermodifier.number;

        city.fortification += ev.modifFortif;
        city.capture += ev.modifCapture;
        city.perception += ev.modifPerc;
        city.productivity += ev.modifProd;
        city.fear += ev.modifFear;
        // voir si le cast suivant ne produit pas d'erreurs
        city.gold = city.gold * (int)ev.goldmult;
    }

    public string getdescrpevent (int id)
    {
        switch (id)
        {
            case 0:
                return None.description;
            case 1:
                return mariage.description;
            case 2:
                return epidemie.description;
            case 3:
                return priest.description;
            case 4:
                return famine.description;
            case 5:
                return Hollandais.description;
            default:
                return "error, event does not exit";
                    }
    }

    public void disapplyEventCity(townEvents ev, Villes city)
    {
        city.current_event = 0;
        city.is_event = false;

        Soldat army = city.garnison;
        army.atk -= ev.modifAtk;
        army.def -= ev.modifDef;
        army.moral -= ev.modifMoral;
        army.intimidate -= ev.modifIntim;
        army.number -= ev.soldiermodifier.number;

        city.fortification -= ev.modifFortif;
        city.capture -= ev.modifCapture;
        city.perception -= ev.modifPerc;
        city.productivity -= ev.modifProd;
        city.fear -= ev.modifFear;
        // voir si le cast suivant ne produit pas d'erreurs

        city.gold = (int) (city.gold / ev.goldmult);
    }

    public void applyEvent(Events ev, Drakkar drakkar)
    {
        CurrentEventTurn = ev.nbRound;

        Soldat Vikings = drakkar.viking;
        Soldat Merc = drakkar.merc_moyens;

        Vikings.atk += ev.modifAtk;
        Vikings.def += ev.modifDef;
        Vikings.moral += ev.modifMoral;
        Vikings.intimidate += ev.modifIntim;
        Vikings.number += ev.soldiermodifier.number;

        Merc.atk += ev.modifAtk;
        Merc.def += ev.modifDef;
        Merc.moral += ev.modifMoral;
        Merc.intimidate += ev.modifIntim;
        Merc.number += ev.soldiermodifier.number;
    }

    public void disapplyEvent(Events ev, Drakkar drakkar)
    {
        Soldat Vikings = drakkar.viking;
        Soldat Merc = drakkar.merc_moyens;

        Vikings.atk -= ev.modifAtk;
        Vikings.def -= ev.modifDef;
        Vikings.moral -= ev.modifMoral;
        Vikings.intimidate -= ev.modifIntim;
        Vikings.number -= ev.soldiermodifier.number;

        Merc.atk -= ev.modifAtk;
        Merc.def -= ev.modifDef;
        Merc.moral -= ev.modifMoral;
        Merc.intimidate -= ev.modifIntim;
        Merc.number -= ev.soldiermodifier.number;
    }

    public void updateTurn(GameManager gamemanager)
    {
        //update villes
        currentTurn += 1;
        UIManager.updateTurnsGUI(currentTurn, max_turn);
        if (currentTurn >= max_turn)
        {
            gamemanager.king = true;
            for (int i = 0; i < gamemanager.TownList.Count; i++)
            {
                gamemanager.TownList[i].is_king = true;
            }
        }
        else
        {
            for (int i = 0; i < gamemanager.TownList.Count; i++)
            {
                gamemanager.TownList[i].gold =
                    (int)Math.Floor(gamemanager.TownList[i].gold * gamemanager.TownList[i].productivity);
                gamemanager.TownList[i].productivity += 0.005f;
                if (currentTurn-gamemanager.TownList[i].raided > 3)
                {
                    gamemanager.TownList[i].raided = 43;
                    gamemanager.TownList[i].gold = 30000;
                }
                if (gamemanager.TownList[i].garnison.number < 80)
                {
                    gamemanager.TownList[i].garnison.number = 80;
                }
                else
                {
                    gamemanager.TownList[i].garnison.number = (int)Math.Floor
                        (gamemanager.TownList[i].garnison.number * gamemanager.TownList[i].fear);

                }
                //gestion des chevaliers
                if (gamemanager.TownList[i].is_knights)
                {
                    if (currentTurn % 3 == 0)
                    {
                        if (i > 0)
                        {
                            gamemanager.TownList[i - 1].knights = gamemanager.TownList[i].knights;
                            gamemanager.TownList[i].knights = no_one;
                            gamemanager.TownList[i - 1].is_knights = true;
                            gamemanager.TownList[i].is_knights = false;
                        }
                        else
                        {
                            gamemanager.TownList[11].knights = gamemanager.TownList[i].knights;
                            gamemanager.TownList[i].knights = no_one;
                            gamemanager.TownList[11].is_knights = true;
                            gamemanager.TownList[i].is_knights = false;
                        }
                    }
                }
            }
            //update event
            if (event_occuring)
            {
                CurrentEventTurn -= 1;
                if (CurrentEventTurn == 0)
                {
                    event_occuring = false;
                    if (current_event.id == 5)
                    {
                        disapplyEvent(current_event, gamemanager.drakkar);
                    }
                    else if (current_event.id != 0)
                    {
                        int indexTown = 0;
                        for (int j = 0; j < gamemanager.TownList.Count; j++)
                        {
                            if (gamemanager.TownList[j].nameVilles == CurrentEventTown)
                            {
                                indexTown = j;
                                break;
                            }
                        }
                        disapplyEventCity((townEvents)current_event, gamemanager.TownList[indexTown]);
                    }
                    CurrentEventTown = "";
                    current_event = None;
                }
            }
            else
            // pas d'event en cours
            {
                int proba = UnityEngine.Random.Range(0, factProba);
                if (proba == 0)
                {
                    factProba = 10;
                    event_occuring = true;
                    int evId = UnityEngine.Random.Range(0, 5);
                    switch (evId)
                    {
                        case 0:
                            current_event = mariage;
                            break;
                        case 1:
                            current_event = epidemie;
                            break;
                        case 2:
                            current_event = priest;
                            break;
                        case 3:
                            current_event = famine;
                            break;
                        case 4:
                            current_event = Hollandais;
                            break;
                    }
                    if (evId == 4)
                    {
                        applyEvent(current_event, gamemanager.drakkar);
                    }
                    else
                    {
                        int randCity = UnityEngine.Random.Range(0, gamemanager.TownList.Count);
                        CurrentEventTown = gamemanager.TownList[randCity].nameVilles;     
                        applyEventCity((townEvents)current_event, gamemanager.TownList[randCity]);
                    }
                }
                else
                {
                    factProba -= 1;
                }
            }
        }
    }
}

