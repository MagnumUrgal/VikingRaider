using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour
{

    protected int costViking { get; set; }
    protected int costMercFai { get; set; }
    protected int costMercMoy { get; set; }
    protected int costMercFor { get; set; }
    // potentiellement mettre les autres valeurs, et la GameManager ne fera que changer les valeurs ici

    // Je considère qu'on gère dans le GameManager le fait qu'on vérifie si le joueur a assez d'argent pour cliquer sur le bouton

    public void recrutSoldat(Drakkar drakkar, string _name, int n)
    {
        // potentiellement refaire le systeme de switch
        if (drakkar.viking.name == _name)
        {
            drakkar.viking.add(n);
            drakkar.gold -= costViking;
        }
        else if (drakkar.merc_faibles.name == _name)
        {
            drakkar.merc_faibles.add(n);
            drakkar.gold -= costMercFai;
        }
        else if (drakkar.merc_moyens.name == _name)
        {
            drakkar.merc_moyens.add(n);
            drakkar.gold -= costMercMoy;
        }
        else if (drakkar.merc_forts.name == _name)
        {
            drakkar.merc_forts.add(n);
            drakkar.gold -= costMercFor;
        }
    }

    public void recrutEspion(Drakkar drakkar, Espion espion, int costEspion)
    {  
        drakkar.gold -= costEspion;   
        drakkar.espion_list.Add(espion);
    }
    // METTRE CA DANS LE GAME MANAGER
    // valeur propre à chaque espion, donc pas en attribut ici
    // int costEspion = 1; /* = un prix à déterminer en fonction des carac. de l'espion */
    // Debug.Log("NEED UNE FONCTION DU PRIX");
    // if (drakkar.espion_list.Count != drakkar.espion_list.Capacity)

    // peut etre mettre chaque costStat dans la classe
    public void UpgradeStat(Drakkar drakkar, string _stat, int costStatUpgrade, int upValue)
    {
        // potentiellement refaire le systeme de switch
        if (_stat == "atk")
        {
            drakkar.viking.atk += upValue;
            drakkar.gold -= costStatUpgrade;
        }
        else if (_stat == "def")
        {
            drakkar.viking.def += upValue;
            drakkar.gold -= costStatUpgrade;
        }
        else if (_stat == "moral")
        {
            drakkar.viking.moral += upValue;
            drakkar.gold -= costStatUpgrade;
        }
        else if (_stat == "intimidate")
        {
            drakkar.viking.intimidate += upValue;
            drakkar.gold -= costStatUpgrade;
        }
    }

    public void DecreaseMinMembers(Drakkar drakkar, int costDecrease, int subValue)
    {
        drakkar.min_members -= subValue;
        drakkar.gold -= costDecrease;
    }
}