using UnityEngine;
using System.Collections;

public class Villes : MonoBehaviour
{
    protected string nameVilles { get; set; }
    protected int fortification { get; set; }
    protected int or { get; set; }
    protected Soldat garnison { get; set; }
    protected bool king { get; set; }
    protected int capture { get; set; }
    protected int perception { get; set; }
    protected int productivity { get; set; }
    protected int fear { get; set; }

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
