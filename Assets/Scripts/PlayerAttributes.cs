using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributes : MonoBehaviour
{
    // 玩家属性
    public int health = 100;
    public int attack = 10;
    public int defend = 10;
    public int exp = 0;
    public int gold = 0;
    public int level = 1;
    // 钥匙
    public int keyYellow = 1;
    public int keyBlue = 1;
    public int keyRed = 1;
    public bool haveSpecial = false;
    // 特殊道具
    public bool book = false;
    public bool flyShoe = false;
}
