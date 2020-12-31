using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPCs : MonoBehaviour
{
    public int dialogID = 0;
    public bool oneTime = false;
    // Start is called before the first frame update

    public void Action()
    {
        GameManager.instance.dm.Talk(dialogID);
        if (oneTime)
        {
            gameObject.tag = "TalkedNPC";
        }
    }
}
