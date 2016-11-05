using UnityEngine;
using System.Collections;

public class Known : MonoBehaviour
{
    public bool is_known { get; set; }
    public int value_known { get; set; }
    public int turn_known { get; set; }

    public Known()
    {
        is_known = false;
        value_known = 0;
        turn_known = 0;
    }
}

public class Villes : MonoBehaviour
{
    public string nameVilles { get; set; }
    public int fortification { get; set; }
    public int gold { get; set; }
    public Known gold_known { get; set; }
    public Soldat garnison { get; set; }
    public Known garni_known { get; set; }
    public bool is_king { get; set; }
    public int capture { get; set; }
    public int perception { get; set; }
    public float productivity { get; set; }
    public int fear { get; set; }
    public bool is_event { get; set; }
    public bool is_knights { get; set; }
    public Soldat knights { get; set; }
    public int pos { get; set; }


    public Villes(string _name, int _fortif, int _gold, Soldat _unite, 
        int _capture, int _perception, float _productivity, int _fear, Soldat _knights, int _pos)
    {
        is_event = false;
        is_knights = false;
        is_king = false;
        nameVilles = _name;
        fortification = _fortif;
        gold = _gold;
        gold_known = new Known();
        garnison = _unite;
        garni_known = new Known();
        capture = _capture;
        perception = _perception;
        productivity = _productivity;
        fear = _fear;
        knights = _knights;
        pos = _pos;
    }
}
