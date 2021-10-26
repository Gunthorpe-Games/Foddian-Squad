using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public string actorName;
    public float hp;

    public BattleManager bm;
    public List<Actor> targets = new List<Actor>();

    public Animator animator;
    public AnimationClip idle;
    public AnimatorOverrideController aoc;
    public int attackTurn = 0;
    public float tempDamageMult = 1;
    public float tempDefenceMult = 1;
    public float permDamageMult = 1;
    public float permDefenceMult = 1;
    public Attack chosenAttack = null;

    // Start is called before the first frame update
    public void Start()
    {
        bm = GameObject.FindGameObjectWithTag("Scripts").GetComponent<BattleManager>();
        animator = GetComponent<Animator>();
        SetAnim(idle, "idle");
    }

    // Update is called once per frame
    private void OnMouseEnter()
    {
        bm.border.position = transform.position;
    }
    private void OnMouseExit()
    {
        bm.border.position = new Vector3(1000,1000,1000);
    }

    public void SetAnim(AnimationClip anim, string animName)
    {
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        foreach (var a in aoc.animationClips)
        {
            if (a.name == animName || (animName == "attack" && a.name.StartsWith("attack")))
            {
                print(a.name);
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, anim));
            }
        }
        aoc.ApplyOverrides(anims);
    }

    public void Attack()
    {
        if (chosenAttack.twoTurnAttack)
        {
            if (attackTurn == 0)
            {
                attackTurn++;
                return;
            }
            else { chosenAttack.SecondaryEffect();
                attackTurn = 0;
            }
        }
        chosenAttack.SecondaryEffect();
        foreach (Actor target in targets)
        {
            if (chosenAttack.attackDamage != 0)
            {
                target.hp -= (permDamageMult * tempDamageMult * chosenAttack.attackDamage) / (target.tempDefenceMult * target.permDefenceMult);
            }
        }
    }
}
