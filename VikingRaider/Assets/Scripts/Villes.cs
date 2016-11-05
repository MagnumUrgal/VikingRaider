using UnityEngine;
using System.Collections;

public class Villes : MonoBehaviour
{
    public string nameVilles { get; set; }
    public int fortification { get; set; }
    public int or { get; set; }
    public Soldat garnison { get; set; }
    public bool king { get; set; }
    public int capture { get; set; }
    public int perception { get; set; }
    public int productivity { get; set; }
    public int fear { get; set; }

    public Villes(string _name, int _fortif, int _or, Soldat _unite, int _capture, int _perception, int _productivity, int _fear)
    {
        king = false;
        nameVilles = _name;
        fortification = _fortif;
        or = _or;
        garnison = _unite;
        capture = _capture;
        perception = _perception;
        productivity = _productivity;
        fear = _fear;
    }
}
