using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject player,startUI,endUI,door;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform playerPosition;
    [SerializeField] List<Transform> enemySpawnPos;
    [SerializeField] TextMeshProUGUI winLoseText;
    int deadAmount;
    public void Begin()
    {
        GameObject playerInstance = Instantiate(player, playerPosition.position,Quaternion.identity);
        if (playerInstance.TryGetComponent<Character_Health>(out Character_Health playerHealth))
        {
            playerHealth.OnDead += PlayerHealth_OnDead;
        }
        for (int i = 0; i < enemySpawnPos.Count; i++)
        {
            GameObject instance = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], enemySpawnPos[i].position,Quaternion.identity);
            if (instance.TryGetComponent<Character_Health>(out Character_Health health))
            {
                health.OnDead += Health_OnDead;
            }
        }
        startUI.SetActive(false);
    }

    private void PlayerHealth_OnDead(object sender, EventArgs e)
    {
       winLoseText.text = "You lose";
       endUI.SetActive(true);
    }

    private void Health_OnDead(object sender, EventArgs e)
    {
        winLoseText.text = "You win";
        deadAmount++;
        //Debug.Log("Dead Amount");
       // door.SetActive(true);
        if (deadAmount >= enemySpawnPos.Count)
        {
            door.SetActive(true);
        }
    }
}
