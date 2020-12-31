using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{

    public StairsType type = StairsType.Up;
    public int floor = 0;

    public void Action()
    {
        GameManager.instance.ChangeLevel(type, floor);
    }
}

public enum StairsType
{
    Up,
    Down
}
