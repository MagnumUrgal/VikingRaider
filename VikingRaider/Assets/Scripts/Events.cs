using UnityEngine;
using System.Collections;

public class Events : MonoBehaviour
{
    // carac de la ville avant l'event, histoire de pouvoir les remettre à la fin de l'event
    public int nbRound { get; set; }

    public int modifAtk { get; set; }
    public int modifDef { get; set; }
    public int modifMoral { get; set; }
    public int modifIntim { get; set; }

    public Events(int _nbRound, int _modifAtk, int _modifDef, int _modifMoral, int _modifIntim)
    {
        nbRound = _nbRound;
        modifAtk = _modifAtk;
        modifDef = _modifDef;
        modifMoral = _modifMoral;
        modifIntim = _modifIntim;
    }
}

public class vikingEvents : Events
{
    public vikingEvents(int _nbRound, int _modifAtk, int _modifDef, int _modifMoral, int _modifIntim) : 
        base(_nbRound, _modifAtk, _modifDef, _modifMoral, _modifIntim)
    {}
}

public class townEvents : Events
{
    public int modifFortif { get; set; }
    public int modifCapture { get; set; }
    public int modifPerc { get; set; }
    public float modifProd { get; set; }
    public float modifFear { get; set; }

    public townEvents(int _nbRound, int _modifAtk, int _modifDef, int _modifMoral, int _modifIntim, int _modifFortif, int _modifCapture, int _modifPerc, float _modifProd, float _modifFear) :
        base(_nbRound, _modifAtk, _modifDef, _modifMoral, _modifIntim)
    {
        modifFortif = _modifFortif;
        modifCapture = _modifCapture;
        modifPerc = _modifPerc;
        modifProd = _modifProd;
        modifFear = _modifFear;
    }
}