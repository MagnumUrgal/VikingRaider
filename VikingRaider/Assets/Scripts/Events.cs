using UnityEngine;
using System.Collections;

public class Events 
{
    public int nbRound { get; set; }
    public int modifAtk { get; set; }
    public int modifDef { get; set; }
    public int modifMoral { get; set; }
    public int modifIntim { get; set; }
    public int id { get; set; }
    public string description { get; set; }
    public Soldat soldiermodifier { get; set; }

    public Events(int _nbRound, int _modifAtk, int _modifDef,
        int _modifMoral, int _modifIntim, int _id, string _description, Soldat _soldier)
    {
        nbRound = _nbRound;
        modifAtk = _modifAtk;
        modifDef = _modifDef;
        modifMoral = _modifMoral;
        modifIntim = _modifIntim;
        id = _id;
        description = _description;
        soldiermodifier = _soldier;
    }
}

public class townEvents : Events
{
    public int modifFortif { get; set; }
    public int modifCapture { get; set; }
    public int modifPerc { get; set; }
    public float modifProd { get; set; }
    public float modifFear { get; set; }
    public float goldmult { get; set; }

    public townEvents(int _nbRound, int _modifAtk, int _modifDef, 
        int _modifMoral, int _modifIntim, int _modifFortif, int _modifCapture, 
        int _modifPerc, float _modifProd, float _modifFear, int _id, string _description, Soldat _soldier, float _goldmult) :
        base(_nbRound, _modifAtk, _modifDef, _modifMoral, _modifIntim, _id, _description, _soldier)
    {
        modifFortif = _modifFortif;
        modifCapture = _modifCapture;
        modifPerc = _modifPerc;
        modifProd = _modifProd;
        modifFear = _modifFear;
        goldmult = _goldmult;
    }
}