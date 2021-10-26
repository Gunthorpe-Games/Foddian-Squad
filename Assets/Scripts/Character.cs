using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    public List<Attack> attacks = new List<Attack>();
    public bool selectedAttack = false;

    public List<Attack> activeAttacks = new List<Attack>();
    public GameObject canvas; 

    public void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        GameObject a;
        foreach (Attack attack in attacks)
        {
            a = Instantiate(attack.gameObject, canvas.transform);
            a.GetComponent<Attack>().character = this;
            activeAttacks.Add(a.GetComponent<Attack>());
        }
    }

    public void ToggleAttacks(bool state)
    {
        foreach (Attack attack in activeAttacks)
        {
            attack.gameObject.SetActive(state);
        }
    }
}
