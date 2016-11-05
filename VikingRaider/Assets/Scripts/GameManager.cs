using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public string gameName;
    public Drakkar drakkar;
    public List<Villes> TownList;    

	// Use this for initialization
	void Start () {

        // init gameName ici avec les input du joueur

        // init du drakkar
        Soldat viking = new Soldat(3, 3, 3, 3, 30, "Vikings");
        Soldat merc = new Soldat(1, 1, 1, 1, 0, "Mercenaires");
        drakkar = new Drakkar(gameName, 0, viking, merc, 8);

        // init des villes
        Soldat knights = new Soldat(3, 3, 4, 2, 20, "Chevaliers Errants");
        TownList = new List<Villes>();

        // listes des noms
        List<string> nameList = new List<string>
        {
            "city 1", "city 2", "city 3",
            "city 4", "city 5", "city 6",
            "city 7", "city 8", "city 9",
            "city 10","city 11", "city 12"
        };

        // liste des nombres de garnisons à répartir
        List<int> nbrList = new List<int>
        {
            80, 100, 120,
            150, 180, 200,
            220, 250, 280,
            3500, 400, 480
        };

        List<int> posList = new List<int>
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11
        };

        for (int i = 0; i < nameList.Count; i++)
        {
            // rand garnisons
            int randGarn = Random.Range(0, nbrList.Count);
            int garnNum = nbrList[randGarn];
            nbrList.RemoveAt(randGarn);

            Soldat garnison = new Soldat(1, 1, 1, 1, garnNum, "Garnisons");

            // rand fortification
            int Fortif = 0;
            int randFortif = Random.Range(0, 3);
            switch (randFortif)
            {
                case 0:
                    Fortif = 0;
                    break;
                case 1:
                    Fortif = 1;
                    break;
                case 2:
                    Fortif = 3;
                    break;
            }

            // rand productivity
            int randProd = Random.Range(0, 3);
            float Prod = 0.0f;
            switch (randProd)
            {
                case 0:
                    Prod = 0.95f;
                    break;
                case 1:
                    Prod = 1.05f;
                    break;
                case 2:
                    Prod = 1.2f;
                    break;
            }

            // rand capture
            int randCapt = Random.Range(0, 4);
            int Capt = randCapt + 1;

            // rand perception
            int randPerc = Random.Range(0, 4);
            int Perc = randPerc;

            // rand gold
            int randGold = Random.Range(40, 76);
            int Gold = randGold * 1000;

            // rand pos   
            int randPos = Random.Range(0, posList.Count);
            int Pos = posList[randPos];
            posList.RemoveAt(randPos);

            // création de la ville
            Villes City = new Villes(nameList[i], Fortif, Gold, garnison, Capt, Perc, Prod, 0, knights, Pos);

            // ajout de la ville créée à la liste
            TownList.Add(City);
        }
    }
}
