using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Fire, Ice, Poison, Thunder }

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{

    public int maxHealth;
    public int damage;
    public float speed;
    public Element element;

}
