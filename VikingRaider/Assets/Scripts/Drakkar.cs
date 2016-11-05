using UnityEngine;
using System.Collections;

public class Drakkar : MonoBehaviour
{
    protected string nameDrakkar { get; set; }
    protected int or { get; set; }
    protected Soldat viking { get; set; }
    protected Soldat merc_faibles { get; set; }
    protected Soldat merc_moyens { get; set; }
    protected Soldat merc_forts { get; set; }

    public Drakkar(string _name, int _or, Soldat _viking, Soldat _mfaibles, Soldat _mmoyens, Soldat _mforts)
    {
        nameDrakkar = _name;
        or = _or;
        viking = _viking;
        merc_faibles = _mfaibles;
        merc_moyens = _mmoyens;
        merc_forts = _mforts;
    }
}
