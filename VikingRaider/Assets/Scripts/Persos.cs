using System;
using UnityEngine;
using System.Collections;

public class Persos : MonoBehaviour
{
    protected string namePerso { get; set; }

    public Persos(string _name)
    {
        namePerso = _name;
    }
}

public class Soldat : Persos
{
    protected int atk { get; set; }
    protected int def { get; set; }
    protected int moral { get; set; }
    protected int intimidate { get; set; }
    protected int number { get; set; }

    public Soldat(int _atk, int _def, int _moral, int _intimidate, int _number, string _name) : base(_name)
    {
        atk = _atk;
        def = _def;
        moral = _moral;
        intimidate = _intimidate;
        number = _number;
    }

    public void add(int n)
    {
        this.number += n;
    }

    public void sub(int n)
    {
        this.number -= n;
    }
}

public class Espion : Persos
{
    protected int fuite { get; set; }
    protected int perception { get; set; }
    protected int discretion { get; set; }

    public Espion(int _fuite, int _perception, int _discretion, string _name) : base(_name)
    {
        fuite = _fuite;
        perception = _perception;
        discretion = _discretion;
    }
}