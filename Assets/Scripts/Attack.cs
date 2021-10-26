using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public string attackName;
    public string attackDesc;
    public int attackDamage;

    public Text nameText;
    public Text descText;

    public Character character;

    BattleManager bm;
    public bool isSingleTarget;
    public bool twoTurnAttack;

    public AnimationClip anim;

    public enum AttackType
    {
        Roundhouse,
        Flail,
        Hide,
        Pounce,
        HighJump,
        Shimmy,
        Bounce,
        Backflip,
        Brace
    }
    public AttackType attackType;

    public void Awake()
    {
        nameText.text = attackName;
        bm = GameObject.FindGameObjectWithTag("Scripts").GetComponent<BattleManager>();
        descText = bm.attackDescText;
    }

    public void ChooseAttack()
    {
        character.SetAnim(anim, "attack");
        descText.text = "";
        character.chosenAttack = this;
        if (isSingleTarget)
        {
            bm.battleStage++;
        }
        else
        {
            bm.battleStage += 2;
            character.targets = bm.activeEnemies;
        }
    }

    public void MouseOver()
    {
        descText.text = attackDesc;
    }

    public void MouseOff()
    {
        descText.text = "";
    }

    public void SecondaryEffect()
    {
        switch (attackType)
        {
            case AttackType.Hide:
                character.tempDefenceMult = Mathf.Infinity;
                break;
            case AttackType.Shimmy:
                character.permDamageMult += 1.5f;
                break;
            case AttackType.Brace:
                character.tempDefenceMult += 1.5f;
                break;
            case AttackType.HighJump:
                character.tempDefenceMult = Mathf.Infinity;
                break;
        }
    }
}
