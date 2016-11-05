using UnityEngine;
using System.Collections;
using System;

public class Time : MonoBehaviour
{

    public int currentTurn { get; set; }
    public bool event_occuring { get; set; }
    public int raidcount { get; set; }
    public int max_turn { get; set; }

    public Time()
    {
        currentTurn = 0;
        event_occuring = false;
        raidcount = 0;
        max_turn = 42;
    }

    public void updateTurn(GameManager gamemanager)
    {
        //update villes
        currentTurn += 1;
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
                if (gamemanager.TownList[i].raided > 3)
                {
                    gamemanager.TownList[i].raided = 43;
                    gamemanager.TownList[i].gold = 30000;
                }
                if (raidcount == 3)
                {
                    switch (gamemanager.TownList[i].garnison.number)
                    {
                        case 480:
                            gamemanager.TownList[i].garnison.number = 480;
                            break;
                        case 400:
                            gamemanager.TownList[i].garnison.number = 420;
                            break;
                        case 350:
                            gamemanager.TownList[i].garnison.number = 380;
                            break;
                        case 280:
                            gamemanager.TownList[i].garnison.number = 330;
                            break;
                        case 250:
                            gamemanager.TownList[i].garnison.number = 300;
                            break;
                        case 220:
                            gamemanager.TownList[i].garnison.number = 250;
                            break;
                        case 200:
                            gamemanager.TownList[i].garnison.number = 220;
                            break;
                        case 180:
                            gamemanager.TownList[i].garnison.number = 180;
                            break;
                        case 150:
                            gamemanager.TownList[i].garnison.number = 150;
                            break;
                        case 120:
                            gamemanager.TownList[i].garnison.number = 120;
                            break;
                        case 100:
                            gamemanager.TownList[i].garnison.number = 100;
                            break;
                        case 80:
                            gamemanager.TownList[i].garnison.number = 80;
                            break;
                        default:
                            gamemanager.TownList[i].garnison.number = 80;
                            break;
                    }
                }
                else if (raidcount == 7)
                {
                    switch (gamemanager.TownList[i].garnison.number)
                    {
                        case 480:
                            gamemanager.TownList[i].garnison.number = 600;
                            break;
                        case 420:
                            gamemanager.TownList[i].garnison.number = 520;
                            break;
                        case 380:
                            gamemanager.TownList[i].garnison.number = 460;
                            break;
                        case 330:
                            gamemanager.TownList[i].garnison.number = 420;
                            break;
                        case 300:
                            gamemanager.TownList[i].garnison.number = 380;
                            break;
                        case 250:
                            gamemanager.TownList[i].garnison.number = 330;
                            break;
                        case 220:
                            gamemanager.TownList[i].garnison.number = 300;
                            break;
                        case 180:
                            gamemanager.TownList[i].garnison.number = 260;
                            break;
                        case 150:
                            gamemanager.TownList[i].garnison.number = 230;
                            break;
                        case 120:
                            gamemanager.TownList[i].garnison.number = 200;
                            break;
                        case 100:
                            gamemanager.TownList[i].garnison.number = 160;
                            break;
                        case 80:
                            gamemanager.TownList[i].garnison.number = 120;
                            break;
                        default:
                            gamemanager.TownList[i].garnison.number = 80;
                            break;
                    }
                }
                else if (raidcount > 7)
                {
                    if (gamemanager.TownList[i].garnison.number < 120)
                        gamemanager.TownList[i].garnison.number = 120;
                    else
                    {
                        gamemanager.TownList[i].garnison.number = (int)Math.Floor
                            (gamemanager.TownList[i].garnison.number * gamemanager.TownList[i].fear);

                    }
                }

                //update event
                //TODO
                if (event_occuring)
                {

                }
            }
        }
    }
}
