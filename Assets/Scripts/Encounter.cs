using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encounter : MonoBehaviour
{
    public GameData.EnemyType enemy1;
    public GameData.EnemyType enemy2;
    public GameData.EnemyType enemy3;

    public int battleId;
    int battleSceneId = 2;
    public bool isOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (GameData.lastBattle == battleId)
        {
            BattleOver();
            return;
        }
        GameData.enemy1 = enemy1;
        GameData.enemy2 = enemy2;
        GameData.enemy3 = enemy3;
        GameData.worldPos = collision.transform.position;
        GameData.lastBattle = battleId;
        SceneManager.LoadScene(battleSceneId);
    }

    public void BattleOver()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GameData.inBattle = false;
    }

    public void Awake()
    {
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<OverworldManagement>().encounters.Add(this);
    }
}
