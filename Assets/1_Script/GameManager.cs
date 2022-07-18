using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float maxSpawnDelay;
    public float curSpawnDelay;
    public GameObject player;
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public GameObject gameOverSet;
    public Image[] lifeImg;
    public Image[] boomImg;
    public TextMeshProUGUI scoreText;
    public ObjPoolManager poolManager;

	private void Awake() {
        enemyObjs = new string[] { "Enemys", "EnemyM", "EnemyL" };
	}
	private void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay > maxSpawnDelay) {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format ("{0:n0}", playerLogic.score);
    }
    void SpawnEnemy() {
        int ranEnemy = Random.Range(0, 3);
        int ranPos = Random.Range(0, 8);
        GameObject enemy = poolManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPos].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        if(ranPos == 5 || ranPos == 6) {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if(ranPos == 7 || ranPos == 8) {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
    public void RespawnPlayer() {
        Invoke("RespawnLogic", 2f);
    }
    private void RespawnLogic() {
        player.transform.position = Vector3.down * 5f;
        player.SetActive (true);
        Player playerLogic = player.GetComponent<Player> ();
        playerLogic.isHit = false;
    }
    public void UpdateLifeIcon (int life) {
        for(int index = 0; index < 3; index++) {
            lifeImg[index].color = new Color (1, 1, 1, 0);
        }
        for(int index = 0; index < life; index++) {
            lifeImg[index].color = new Color (1, 1, 1, 1);
        }
    }
    public void UpdateBoomIcon(int boom) {
        for(int index = 0; index < 3; index++) {
            boomImg[index].color = new Color(1, 1, 1, 0);
        }
        for(int index = 0; index < boom; index++) {
            boomImg[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void GameOver() {
        gameOverSet.SetActive(true);
    }
    public void GameReStart() {
        SceneManager.LoadScene(0);
    }
}
