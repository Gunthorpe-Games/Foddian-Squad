using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    public GameData.EnemyType enemyType;
    private void OnMouseDown()
    {
        if (bm.battleStage == 1 || bm.battleStage == 3 || bm.battleStage == 5)
        {
            Debug.Log("clicked");
            bm.clickedActor = this;
        }
    }
}
