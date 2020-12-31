using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogManager : MonoBehaviour
{

    private int _currentDialogID = 0;
    public GameObject fc;
    private Flowchart _fc;
    public List<string> tips = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        _currentDialogID = 0;
        _fc = fc.GetComponent<Flowchart>();
        fc.SetActive(false);
    }

    public void Talk(int dialogID)
    {
        _currentDialogID = dialogID;
        _fc.SetIntegerVariable("DialogID", dialogID);
        fc.SetActive(true);
    }

    public void AfterTalk()
    {
        switch(_currentDialogID)
        {
            case 0:
                AfterDialog1();
                fc.SetActive(false);
                break;
            case 3:
                AfterDialog3();
                fc.SetActive(false);
                break;
            case 4:
                AfterDialog4();
                fc.SetActive(false);
                break;
            case 5:
                AfterDialog5();
                fc.SetActive(false);
                break;
            case 10:
                AfterDialog10();
                fc.SetActive(false);
                break;
            case 11:
                AfterDialog11();
                fc.SetActive(false);
                break;
            default:
                fc.SetActive(false);
                break;
        }
    }

    // dialog 0 对话
    public void AfterDialog1()
    {
        GameManager.instance.pa.keyYellow += 1;
        GameManager.instance.pa.keyBlue += 1;
        GameManager.instance.pa.keyRed += 1;
        GameManager.instance.ui.UpdateUI();
        GameManager.instance.ui.ActiveTips(tips[0]);
        // 改变NPC的对话id
        GameObject npc = GameObject.Find("NPC0");
        NPCs npcs = npc.GetComponent<NPCs>();
        npcs.dialogID = 1;
    }

    // dialog 2 对话
    public void AfterDialog2()
    {
        int option = _fc.GetIntegerVariable("Option");
        if (GameManager.instance.pa.gold < 25)
        {
            GameManager.instance.ui.ActiveTips("别摸！");
            return;
        }
        GameManager.instance.pa.gold -= 25;
        switch (option)
        {
            case 1:
                GameManager.instance.pa.health += 800;
                GameManager.instance.ui.ActiveTips(tips[1]);
                break;
            case 2:
                GameManager.instance.pa.attack += 4;
                GameManager.instance.ui.ActiveTips(tips[2]);
                break;
            case 3:
                GameManager.instance.pa.defend += 4;
                GameManager.instance.ui.ActiveTips(tips[3]);
                break;
            default:
                break;
        }
        GameManager.instance.ui.UpdateUI();
    }

    // dialog 3 对话
    public void AfterDialog3()
    {
        GameManager.instance.pa.haveSpecial = true;
        GameManager.instance.ui.ActiveTips(tips[4]);
    }

    // dialog 4 对话
    public void AfterDialog4()
    {
        GameManager.instance.pa.attack += 30;
        GameManager.instance.ui.ActiveTips(tips[5]);
        GameManager.instance.ui.UpdateUI();
    }

    // dialog 5 对话
    public void AfterDialog5()
    {
        GameManager.instance.pa.defend += 30;
        GameManager.instance.ui.ActiveTips(tips[6]);
        GameManager.instance.ui.UpdateUI();
    }
    // dialog 6 对话
    public void AfterDialog6()
    {
        int option = _fc.GetIntegerVariable("Option");
        switch (option)
        {
            case 1:
                if (GameManager.instance.pa.exp < 100)
                {
                    GameManager.instance.ui.ActiveTips("多打怪吧！经验不够");
                    return;
                }
                GameManager.instance.pa.level += 1;
                GameManager.instance.pa.health += 600;
                GameManager.instance.pa.attack += 7;
                GameManager.instance.pa.defend += 7;
                GameManager.instance.pa.exp -= 100;
                GameManager.instance.ui.ActiveTips(tips[7]);
                break;
            case 2:
                if (GameManager.instance.pa.exp < 30)
                {
                    GameManager.instance.ui.ActiveTips("多打怪吧！经验不够");
                    return;
                }
                GameManager.instance.pa.attack += 5;
                GameManager.instance.pa.exp -= 30;
                GameManager.instance.ui.ActiveTips(tips[8]);
                break;
            case 3:
                if (GameManager.instance.pa.exp < 30)
                {
                    GameManager.instance.ui.ActiveTips("多打怪吧！经验不够");
                    return;
                }
                GameManager.instance.pa.defend += 5;
                GameManager.instance.pa.exp -= 30;
                GameManager.instance.ui.ActiveTips(tips[9]);
                break;
            default:
                break;
        }
        GameManager.instance.ui.UpdateUI();
    }
    // dialog 7 对话
    public void AfterDialog7()
    {
        int option = _fc.GetIntegerVariable("Option");
        if (GameManager.instance.pa.gold < 100)
        {
            GameManager.instance.ui.ActiveTips("别摸！");
            return;
        }
        GameManager.instance.pa.gold -= 100;
        switch (option)
        {
            case 1:
                GameManager.instance.pa.health += 4000;
                GameManager.instance.ui.ActiveTips(tips[10]);
                break;
            case 2:
                GameManager.instance.pa.attack += 20;
                GameManager.instance.ui.ActiveTips(tips[11]);
                break;
            case 3:
                GameManager.instance.pa.defend += 20;
                GameManager.instance.ui.ActiveTips(tips[12]);
                break;
            default:
                break;
        }
        GameManager.instance.ui.UpdateUI();
    }

    // dialog 8 对话
    public void AfterDialog8()
    {
        int option = _fc.GetIntegerVariable("Option");
        switch (option)
        {
            case 1:
                if (GameManager.instance.pa.keyYellow < 1)
                {
                    GameManager.instance.ui.ActiveTips("你没有足够的钥匙");
                    return;
                }
                GameManager.instance.pa.keyYellow -= 1;
                GameManager.instance.pa.gold += 7;
                GameManager.instance.ui.ActiveTips(tips[13]);
                break;
            case 2:
                if (GameManager.instance.pa.keyBlue < 1)
                {
                    GameManager.instance.ui.ActiveTips("你没有足够的钥匙");
                    return;
                }
                GameManager.instance.pa.keyBlue -= 1;
                GameManager.instance.pa.gold += 35;
                GameManager.instance.ui.ActiveTips(tips[14]);
                break;
            case 3:
                if (GameManager.instance.pa.keyRed < 1)
                {
                    GameManager.instance.ui.ActiveTips("你没有足够的钥匙");
                    return;
                }
                GameManager.instance.pa.keyRed -= 1;
                GameManager.instance.pa.gold += 70;
                GameManager.instance.ui.ActiveTips(tips[15]);
                break;
            default:
                break;
        }
        GameManager.instance.ui.UpdateUI();
    }

    // dialog 9 对话
    public void AfterDialog9()
    {
        int option = _fc.GetIntegerVariable("Option");
        switch (option)
        {
            case 1:
                if (GameManager.instance.pa.exp < 270)
                {
                    GameManager.instance.ui.ActiveTips("多打怪吧！经验不够");
                    return;
                }
                GameManager.instance.pa.level += 3;
                GameManager.instance.pa.health += 1800;
                GameManager.instance.pa.attack += 21;
                GameManager.instance.pa.defend += 21;
                GameManager.instance.pa.exp -= 270;
                GameManager.instance.ui.ActiveTips(tips[16]);
                break;
            case 2:
                if (GameManager.instance.pa.exp < 95)
                {
                    GameManager.instance.ui.ActiveTips("多打怪吧！经验不够");
                    return;
                }
                GameManager.instance.pa.attack += 17;
                GameManager.instance.pa.exp -= 95;
                GameManager.instance.ui.ActiveTips(tips[17]);
                break;
            case 3:
                if (GameManager.instance.pa.exp < 95)
                {
                    GameManager.instance.ui.ActiveTips("多打怪吧！经验不够");
                    return;
                }
                GameManager.instance.pa.defend += 17;
                GameManager.instance.pa.exp -= 95;
                GameManager.instance.ui.ActiveTips(tips[18]);
                break;
            default:
                break;
        }
        GameManager.instance.ui.UpdateUI();
    }

    // dialog 10 对话
    public void AfterDialog10()
    {
        GameManager.instance.pa.attack += 200;
        GameManager.instance.ui.ActiveTips(tips[19]);
        GameManager.instance.ui.UpdateUI();
    }

    // dialog 11 对话
    public void AfterDialog11()
    {
        GameManager.instance.pa.defend += 200;
        GameManager.instance.ui.ActiveTips(tips[20]);
        GameManager.instance.ui.UpdateUI();
    }
}
