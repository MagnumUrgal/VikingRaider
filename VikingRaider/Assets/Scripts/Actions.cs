using UnityEngine;
using System.Collections;
using System;


public class Actions : MonoBehaviour {
    //Urgal : liaison de l'UIMainSceneManager
    private UIMainSceneManager UIManager;

    //Urgal : liaison de l'UIMainSceneManager
    void Start()
    {
        UIManager = GameObject.Find("Canvas").GetComponent<UIMainSceneManager>();
    }

    public void getinfo (Villes town, Espion spy, Time time)
    {
        int quality = 20 - spy.perception;
        int info = UnityEngine.Random.Range(1, 3);
         if (info == 1)
        {
            town.garni_known.is_known = true;
            town.garni_known.turn_known = time.currentTurn;
            float percent = (float) town.garnison.number / 100f;
            int success = UnityEngine.Random.Range(100 - quality, 100 + quality);
            town.garni_known.value_known = (int)Math.Floor((float) success * percent);
            //Urgal : espionnage de la garnison
            UIManager.DrawSpyResult(town.garni_known.value_known, ResultType.GARNISON, spy);
        }
        else 
        {
            town.gold_known.is_known = true;
            town.gold_known.turn_known = time.currentTurn;
            float percent = town.gold / 100;
            int success = UnityEngine.Random.Range(100 - quality, 100 + quality);
            town.gold_known.value_known = (int)Math.Floor(success * percent);
            //Urgal : espionnage de l'or
            UIManager.DrawSpyResult(town.gold_known.value_known, ResultType.GOLD, spy);
        }
        if (town.is_knights)
        {
            //todo urgal, add message knights
        }
        if (town.is_trebuchet)
        {
            //todo urgal, add message trebuchet
        }
        if (town.is_event)
        {
            //todo urgal
            string descrption = time.getdescrpevent(town.current_event);
        }
    }
    

	public void Espionnage(Drakkar joueur, Espion spy, Villes town, Time time)
    {
        if (town.perception>spy.discretion)
        {
            time.max_turn -= 1;
            if (town.capture>spy.fuite)
            {
                //Urgal : delete espion
                UIManager.DrawSpyResult(0, ResultType.DEADSPY, spy);
            }
            else
            {
                getinfo(town, spy, time);
                //todo faire la distinction de cas
            }
        }
        else { getinfo(town, spy, time); }

    }


    //débuts des fonctions auxilliaires de calcul coté vikings
    public int vikings_atk (Drakkar joueur)
    {
        int result = joueur.viking.number * joueur.viking.atk
    //+ joueur.merc_faibles.number * joueur.merc_faibles.atk
    + joueur.merc_moyens.number * joueur.merc_moyens.atk;
    //+ joueur.merc_forts.number * joueur.merc_forts.atk;

        return result;
    }

    public int vikings_def(Drakkar joueur)
    {
        int result = joueur.viking.number * joueur.viking.def
    //  + joueur.merc_faibles.number * joueur.merc_faibles.def
    + joueur.merc_moyens.number * joueur.merc_moyens.def;
    //+ joueur.merc_forts.number * joueur.merc_forts.def;

        return result;
    }
    public int vikings_moral(Drakkar joueur)
    {
        int result = joueur.viking.number * joueur.viking.moral
    // + joueur.merc_faibles.number * joueur.merc_faibles.moral
    + joueur.merc_moyens.number * joueur.merc_moyens.moral;
   // + joueur.merc_forts.number * joueur.merc_forts.moral;

        return result;
    }
    public int vikings_inti(Drakkar joueur)
    {
        int result = joueur.viking.number * joueur.viking.intimidate
    //+ joueur.merc_faibles.number * joueur.merc_faibles.intimidate
    + joueur.merc_moyens.number * joueur.merc_moyens.intimidate;
   // + joueur.merc_forts.number * joueur.merc_forts.intimidate;

        return result;
    }

    //début des fonctions auxiliaires de calcul coté garnison
    public int garni_atk(Villes town)
    {
        int result = town.garnison.atk*town.garnison.number
            +town.knights.atk*town.knights.number+town.trebuchet.atk;
        return result;
    }
    public int garni_def(Villes town)
    {
        int result = (town.garnison.def+town.fortification) * town.garnison.number
            + (town.knights.def + town.fortification) * town.knights.number+town.trebuchet.def;
        return result;
    }
    public int garni_moral(Villes town)
    {
        int result = town.garnison.moral * town.garnison.number
            + town.knights.moral * town.knights.number+town.trebuchet.moral;
        return result;
    }
    public int garni_inti(Villes town)
    {
        int result = town.garnison.intimidate * town.garnison.number
            + town.knights.intimidate * town.knights.number+town.trebuchet.intimidate;
        return result;
    }

    public void BattleRoyale(Drakkar joueur)
    {
        //TODO Urgal
        Soldat garderoyale = new Soldat(7, 8, 100, 0, 100, "garderoyale");
        Soldat no_one = new Soldat(0, 0, 0, 0, 0, "personne");
        Villes roi = new Villes("roi", 0, 0, garderoyale, 0, 0, 0, no_one, no_one, 0);
        bool end = false;
        int initialSoldiersNumber = joueur.viking.number;
        int initialMercenaireNumber = joueur.merc_moyens.number;
        while (!end )
        {
            int attaque_joueur = vikings_atk(joueur);
            if (attaque_joueur >= garni_def(roi))
            {
                joueur.gold += 999999;
                //TODO Urgal : victoire totale
                end = true;
            }
            //diminution de la garnison
            roi.garnison.number = (int)Math.Floor((float)attaque_joueur / (roi.garnison.def));                    
            //fin du tour des vikings, tour de la garnison
            int attaque_garni = garni_atk(roi);
            if (attaque_garni > vikings_def(joueur))
            {
                //TODO Urgal : le joueur Game Over
                end = true;
            }
            else if (attaque_garni > joueur.merc_moyens.number * joueur.merc_moyens.def)
            {
                //cas du rekt des merc ou de pas de merc
                attaque_garni -= joueur.merc_moyens.number * joueur.merc_moyens.def;
                joueur.merc_moyens.number = 0;
                //diminution de l'équipage
                joueur.viking.number = (int)Math.Floor((float)attaque_garni / joueur.viking.def);                
            }
            else
            {
                //diminution de l'équipage
                joueur.merc_moyens.number = (int)Math.Floor((float)attaque_garni / joueur.merc_moyens.def);                
            }
        }           
    }

    //résolution
    public void Pillage(Drakkar joueur, Villes town, Time time)
    {
        time.max_turn -= 1;
        time.raidcount += 1;
        town.fear += 0.05f;
        town.productivity = 0.95f;
        bool end = false;
        //Urgal : print pertes humaines
        int initialSoldiersNumber = joueur.viking.number;
        int initialMercenaireNumber = joueur.merc_moyens.number;
        while ( !end )
        {
            int attaque_joueur = vikings_atk(joueur);
            //tour des vikings
            if (attaque_joueur >= garni_def(town))
            {
                town.knights.number = 0;
                town.garnison.number = 0;
                town.is_knights = false;
                town.is_trebuchet = false;
                joueur.gold += town.gold;
                //Urgal : victoire 
                UIManager.DrawPillageResult(town.gold, initialSoldiersNumber - joueur.viking.number, initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGEWIN);
                town.gold = 0;
                end = true;
            }
            else if (attaque_joueur >= town.knights.number * town.knights.def)
            {
                //cas du rekt des chevaliers ou de pas de chevaliers
                attaque_joueur -= town.knights.number * (town.knights.def + town.fortification);
                town.knights.number = 0;
                town.is_knights = false;
                if (attaque_joueur > 0)
                {
                    //diminution de la garnison
                    town.garnison.number -= (int)Math.Floor((float) attaque_joueur / (town.garnison.def + town.fortification));
                    if (vikings_inti(joueur)>garni_moral(town))
                    {
                        joueur.gold += town.gold;
                        //Urgal : cas ou la ville se rend
                        UIManager.DrawPillageResult(town.gold, initialSoldiersNumber - joueur.viking.number, 
                            initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGESURRENDER);
                        town.gold = 0;
                        end = true;
                    }
                    //sinon on continue
                }
                else if (vikings_inti(joueur) > garni_moral(town))
                {
                    joueur.gold += town.gold;
                    //Urgal : cas ou plus d'attaque restante, mais les defenseurs se rendent quand même
                    UIManager.DrawPillageResult(town.gold, initialSoldiersNumber - joueur.viking.number,
                        initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGEWIN);
                    town.gold = 0;
                    end = true;
                }
                //sinon on continue
            }
            else
            {
                //cas ou les knights se font pas tous peter
                //diminution de la garnison
                town.knights.number -= (int)Math.Floor((float)attaque_joueur / (town.knights.def + town.fortification));
                if (vikings_inti(joueur) > garni_moral(town))
                {
                    end = true;
                    joueur.gold += town.gold;
                    //Urgal : cas où la ville se rend
                    UIManager.DrawPillageResult(town.gold, initialSoldiersNumber - joueur.viking.number,
                        initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGESURRENDER);
                    town.gold = 0;
                }
                //sinon on continue
            }
            //fin du tour des vikings, tour de la garnison
            int attaque_garni = garni_atk(town);
            if (attaque_garni > vikings_def(joueur))
            {
                //Urgal : le joueur Game Over
                UIManager.DrawPillageResult(0, initialSoldiersNumber - joueur.viking.number, initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGEGAMEOVER);
                end = true;
            }
            else if (attaque_garni > joueur.merc_moyens.number * joueur.merc_moyens.def)
            {
                //cas du rekt des merc ou de pas de merc
                attaque_garni -= joueur.merc_moyens.number * joueur.merc_moyens.def;
                joueur.merc_moyens.number = 0;
                if (attaque_joueur > 0)
                {
                    //diminution de l'équipage
                    joueur.viking.number -= (int)Math.Floor((float)attaque_garni / joueur.viking.def);
                    if (garni_inti(town) > vikings_moral(joueur))
                    {
                        end = true;
                        //Urgal : cas ou les vikings se barrent
                        UIManager.DrawPillageResult(town.gold, initialSoldiersNumber - joueur.viking.number, initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGELOST);
                    }
                    //sinon on continue
                }
                else if (garni_inti(town) > vikings_moral(joueur))
                {
                    end = true;
                    //Urgal : cas ou les vikings se barrent
                    UIManager.DrawPillageResult(town.gold, initialSoldiersNumber - joueur.viking.number, initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGELOST);
                }
                //sinon on continue

            }
            else
            {
                //diminution de l'équipage
                joueur.merc_moyens.number -= (int)Math.Floor((float)attaque_garni / joueur.merc_moyens.def);
                if (garni_inti(town) > vikings_moral(joueur))
                {
                    end = true;
                    //Urgal : cas ou les vikings se barrent
                    UIManager.DrawPillageResult(town.gold, initialSoldiersNumber - joueur.viking.number,
                        initialMercenaireNumber - joueur.merc_moyens.number, ResultType.PILLAGELOST);
                }
            }
            //sinon on continue
        }
    }
}
