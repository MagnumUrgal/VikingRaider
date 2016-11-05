using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drakkar : MonoBehaviour
{
    public string nameDrakkar { get; set; }
    public int gold { get; set; }
    public Soldat viking { get; set; }
   // public Soldat merc_faibles { get; set; }
    public Soldat merc_moyens { get; set; }
   // public Soldat merc_forts { get; set; }
    public List<Espion> espion_list;
    public int min_members;

    public Drakkar(string _name, int _gold, Soldat _viking /*,Soldat _mfaibles*/, Soldat _mmoyens /*,Soldat _mforts*/, int _minMembers)
    {
        nameDrakkar = _name;
        gold = _gold;
        viking = _viking;
       // merc_faibles = _mfaibles;
        merc_moyens = _mmoyens;
       // merc_forts = _mforts;
        espion_list = new List<Espion>(3);
        min_members = _minMembers;
    }

    // supprime l'élément de la liste en cas d'égalité sur le nom
    public void delEspion(Espion espion)
    {
        for (int i = 0; i < espion_list.Capacity; i++)
        {
            if (espion_list[i].name == espion.name)
            {
                espion_list.RemoveAt(i);
                break;
                // on break car on est sûr de supprimer au maximum un seul espion, et la longueur de la liste a diminué de 1 après la suppression, donc erreur de pointeur si on continue
            }
        }
    }

}
