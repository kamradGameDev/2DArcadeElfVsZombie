using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum classCharacter
{
    Archer, Mage, Warrior
}

public class playerAttribute : MonoBehaviour
{
    public bool status = false;
    public bool attack = false;
    public float speed = 0;
    public float normalSpeed = 5;
    public float timeAttack;

    public bool flip;
    public float damage;
    public classCharacter _classCharacter;
}
