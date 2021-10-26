using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Vector2 char1Pos;
    public Vector2 char2Pos;
    public Vector2 char3Pos;
    public Vector2 enemy1Pos;
    public Vector2 enemy2Pos;
    public Vector2 enemy3Pos;

    public Character char1;
    public Character char2;
    public Character char3;

    public GameObject char1Obj;
    public GameObject char2Obj;
    public GameObject char3Obj;

    public Enemy enemy1;
    public Enemy enemy2;
    public Enemy enemy3;
    public List<Actor> activeEnemies;

    public List<Enemy> allEnemies;
    public Text attackDescText;
    public Text targetText;
    public GameObject canvas;

    public Transform border;
    public Actor clickedActor = null;

    public AnimatorOverrideController aoc1;
    public AnimatorOverrideController aoc2;
    public AnimatorOverrideController aoc3;

    public int battleStage = 0;
    float bufferTimer = 0;
    bool animPlayed = false;

    void Start()
    {
        GameData.inBattle = true;
        char1 = Instantiate(char1Obj, char1Pos, Quaternion.Euler(0, 0, 0)).GetComponent<Character>();
        char2 = Instantiate(char2Obj, char2Pos, Quaternion.Euler(0, 0, 0)).GetComponent<Character>();
        char3 = Instantiate(char3Obj, char3Pos, Quaternion.Euler(0, 0, 0)).GetComponent<Character>();
        char1.hp = GameData.dioHP;
        char2.hp = GameData.pogoHP;
        char3.hp = GameData.kingHP;
        char1.canvas = canvas;
        char2.canvas = canvas;
        char3.canvas = canvas;

        char1.ToggleAttacks(false);
        char2.ToggleAttacks(false);
        char3.ToggleAttacks(false);

        foreach (Enemy enemy in allEnemies)
        {
            GameObject e;

            if (enemy.enemyType == GameData.enemy1)
            {
                e = Instantiate(enemy.gameObject, enemy1Pos, Quaternion.Euler(0,0,0));
                enemy1 = e.GetComponent<Enemy>();
                enemy1.aoc = aoc1;
                activeEnemies.Add(e.GetComponent<Enemy>());
            }
            if (enemy.enemyType == GameData.enemy2)
            {
                e = Instantiate(enemy.gameObject, enemy2Pos, Quaternion.Euler(0, 0, 0));
                enemy2 = e.GetComponent<Enemy>();
                enemy2.aoc = aoc2;
                activeEnemies.Add(e.GetComponent<Enemy>());
            }
            if (enemy.enemyType == GameData.enemy3)
            {
                e = Instantiate(enemy.gameObject, enemy3Pos, Quaternion.Euler(0, 0, 0));
                enemy3 = e.GetComponent<Enemy>();
                enemy3.aoc = aoc3;
                activeEnemies.Add(e.GetComponent<Enemy>());
            }
        }

        attackDescText.text = "";
    }

    private void Update()
    {
        switch (battleStage)
        {
            case 0:
                char1.ToggleAttacks(true);
                targetText.gameObject.SetActive(false);
                break;
            case 1:
                targetText.gameObject.SetActive(true);
                char1.ToggleAttacks(false);
                if (clickedActor)
                {
                    char1.targets.Add(clickedActor);
                    char1.targets = null;
                    clickedActor = null;
                    battleStage++;
                }
                break;
            case 2:
                targetText.gameObject.SetActive(false);
                char1.ToggleAttacks(false);
                char2.ToggleAttacks(true);
                break;
            case 3:
                targetText.gameObject.SetActive(true);
                char2.ToggleAttacks(false);
                if (clickedActor)
                {
                    char2.targets.Add(clickedActor);
                    char2.targets = null;
                    clickedActor = null;
                    battleStage++;
                }
                break;
            case 4:
                targetText.gameObject.SetActive(false);
                char2.ToggleAttacks(false);
                char3.ToggleAttacks(true);
                break;
            case 5:
                targetText.gameObject.SetActive(true);
                char3.ToggleAttacks(false);
                if (clickedActor)
                {
                    char3.targets.Add(clickedActor);
                    char3.targets = null;
                    clickedActor = null;
                    animPlayed = false;
                    battleStage++;
                    bufferTimer = 0.5f;
                }
                break;
            case 6:
                if (bufferTimer > 0) break;
                char1.animator.SetBool("attackAnim", true);
                if (!char1.animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) animPlayed = true;
                if (char1.animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && animPlayed)
                {
                    char1.animator.SetBool("attackAnim", false);
                    battleStage++;
                    bufferTimer = 1f;
                    char1.Attack();
                }
                targetText.gameObject.SetActive(false);
                char3.ToggleAttacks(false);
                break;
            case 7:
                if (bufferTimer > 0) break;
                char2.animator.SetBool("attackAnim", true);
                if (!char2.animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) animPlayed = true;
                if (char2.animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && animPlayed)
                {
                    char2.animator.SetBool("attackAnim", false);
                    battleStage++;
                    bufferTimer = 1f;
                    char2.Attack();
                }
                break;
            case 8:
                if (bufferTimer > 0) break;
                char3.animator.SetBool("attackAnim", true);
                if (!char3.animator.GetCurrentAnimatorStateInfo(0).IsName("idle")) animPlayed = true;
                if (char3.animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && animPlayed)
                {
                    char3.animator.SetBool("attackAnim", false);
                    battleStage++;
                    bufferTimer = 1f;
                    char3.Attack();
                }
                break;

        }
        bufferTimer -= Time.deltaTime;
    }
}
