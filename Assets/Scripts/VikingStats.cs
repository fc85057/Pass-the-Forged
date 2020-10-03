using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewVikingStats", menuName = "Viking Stats")]
public class VikingStats : ScriptableObject
{

    public int maxHealth;
    public int maxStamina;
    public float movementSpeed = 5;
    public int meleeDamage;
    public int rangeDamage;
    public int healing;
    public Element[] immunities;
    public float jumpForce = 10;

}
