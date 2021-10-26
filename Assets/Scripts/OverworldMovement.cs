using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMovement : MonoBehaviour
{
    private float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xMove, yMove) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void FixedUpdate()
    {
        
    }

    void Save()
    {
        PlayerPrefs.SetFloat("playerX", transform.position.x);
        PlayerPrefs.SetFloat("playerY", transform.position.y);
        PlayerPrefs.SetInt("char1Hp", GameData.dioHP);
        PlayerPrefs.SetInt("char2Hp", GameData.pogoHP);
        PlayerPrefs.SetInt("char3Hp", GameData.kingHP);
    }
}
