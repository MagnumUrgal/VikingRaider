﻿using UnityEngine;
using System.Collections;
using System;


public class Actions : MonoBehaviour {

    public void getinfo (Villes town, Espion spy, Time time)
    {
        int quality = 20 - spy.perception;
        int info = UnityEngine.Random.Range(1, 2);
         if (info == 1)
        {
            town.garni_known.is_known = true;
            town.garni_known.turn_known = time.currentTurn;
            float percent = town.garnison.number / 100;
            int success = UnityEngine.Random.Range(100 - quality, 100 + quality);
            town.garni_known.value_known = (int)Math.Floor(success * percent);
            //espionnage de la garnison
        }
        else 
        {
            town.gold_known.is_known = true;
            town.gold_known.turn_known = time.currentTurn;
            float percent = town.gold / 100;
            int success = UnityEngine.Random.Range(100 - quality, 100 + quality);
            town.gold_known.value_known = (int)Math.Floor(success * percent);
            //espionnage de l'or
        }
}
    

	public void Espionnage(Drakkar joueur, Espion spy, Villes town, Time time)
    {
        if (town.perception>spy.discretion)
        {
            if (town.capture>spy.fuite)
            {
                //delete espion
            }
            else { getinfo(town, spy, time); }
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
            +town.knights.atk*town.knights.number;
        return result;
    }
    public int garni_def(Villes town)
    {
        int result = town.garnison.def * town.garnison.number
            + town.knights.def * town.knights.number;
        return result;
    }
    public int garni_moral(Villes town)
    {
        int result = town.garnison.moral * town.garnison.number
            + town.knights.moral * town.knights.number;
        return result;
    }
    public int garni_inti(Villes town)
    {
        int result = town.garnison.intimidate * town.garnison.number
            + town.knights.intimidate * town.knights.number;
        return result;
    }

    //résolution
    public void Pillage(Drakkar joueur, Villes town)
    {
        bool end = false;
        while ( !end )
        {
            int attaque_joueur = vikings_atk(joueur);
            //tour des vikings
            if (attaque_joueur >= garni_def(town))
            {
                town.knights.number = 0;
                town.garnison.number = 0;
                joueur.gold += town.gold;
                town.gold = 0;
                end = true;
                //victoire 
            }
            else if (attaque_joueur >= town.knights.number * town.knights.def)
            {
                //cas du rekt des chevaliers ou de pas de chevaliers
                attaque_joueur -= town.knights.number * town.knights.def;
                town.knights.number = 0;
                if (attaque_joueur > 0)
                {
                    //diminution de la garnison
                    town.garnison.number = (int)Math.Floor((float) attaque_joueur / town.garnison.def);
                    if (vikings_inti(joueur)>garni_moral(town))
                    {
                        joueur.gold += town.gold;
                        town.gold = 0;
                        end = true;
                        //cas ou la ville se rend
                    }
                    //sinon on continue
                }
                else if (vikings_inti(joueur) > garni_moral(town))
                {
                    //cas ou plus d'attaque restante, mais les defenseurs se rendent quand même
                    joueur.gold += town.gold;
                    town.gold = 0;
                    end = true;
                }
                //sinon on continue
            }
            else
            {
                //cas ou les knights se font pas tous peter
                //diminution de la garnison
                town.knights.number = (int)Math.Floor((float)attaque_joueur / town.knights.def);
                if (vikings_inti(joueur) > garni_moral(town))
                {
                    end = true;
                    joueur.gold += town.gold;
                    town.gold = 0;
                    //cas ou la ville se rend 
                }
                //sinon on continue
            }
            //fin du tour des vikings, tour de la garnison
            int attaque_garni = garni_atk(town);
            if (attaque_garni > vikings_def(joueur))
            {
                //le joueur a perdu
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
                    joueur.viking.number = (int)Math.Floor((float)attaque_garni / joueur.viking.def);
                    if (garni_inti(town) > vikings_moral(joueur))
                    {
                        end = true;
                        //cas ou les vikings se barrent
                    }
                    //sinon on continue
                }
                else if (garni_inti(town) > vikings_moral(joueur))
                {
                    end = true;
                    //cas ou les vikings se barrent 
                }
                //sinon on continue

            }
            else
            {
                //diminution de l'équipage
                joueur.viking.number = (int)Math.Floor((float)attaque_garni / joueur.viking.def);
                if (garni_inti(town) > vikings_moral(joueur))
                {
                    end = true;
                    //cas ou les vikings se barrent
                }
            }
            //sinon on continue
        }
    }
}
