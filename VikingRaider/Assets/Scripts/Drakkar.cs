using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drakkar : MonoBehaviour
{
    public string nameDrakkar { get; set; }
    public int gold { get; set; }
    public Soldat viking { get; set; }
    public Soldat merc_faibles { get; set; }
    public Soldat merc_moyens { get; set; }
    public Soldat merc_forts { get; set; }
    public List<Espion> espion_list;
    public int min_members;

    public Drakkar(string _name, int _or, Soldat _viking, Soldat _mfaibles, Soldat _mmoyens, Soldat _mforts)
    {
        nameDrakkar = _name;
        gold = _or;
        viking = _viking;
        merc_faibles = _mfaibles;
        merc_moyens = _mmoyens;
        merc_forts = _mforts;
        espion_list = new List<Espion>(3);
        min_members = 50;
    }
}
