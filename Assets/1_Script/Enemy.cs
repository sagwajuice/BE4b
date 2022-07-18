using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public int enumyScore;
    public float maxShotDelay;
    public float curShotDelay;
    public GameObject bullet01;
    public GameObject bullet02;
    public GameObject player;
    public Sprite [] sprites;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;
    private SpriteRenderer spriteRenderer;
    //----------------------------------------본문
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update() {
        Fire();
        Reload();
    }
    void Fire()
    {
        if (curShotDelay < maxShotDelay) {
            return;
        }
        if (enemyName == "L") {
            Vector3 dirVec = player.transform.position - transform.position;
            GameObject bulletL = Instantiate(bullet02, transform.position + Vector3.left * 0.1f, transform.rotation);
            
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            
            rigidL.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
            
        }
        else if (enemyName == "S") {
            Vector3 dirVec = player.transform.position - transform.position;
            GameObject bullet = Instantiate(bullet01, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
        curShotDelay = 0;
    }
    void Reload() {
        curShotDelay += Time.deltaTime;
    }
    public void OnHit(int dmg) {
        if(health <= 0) {
            return;
		}
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("RetunSprite", 0.1f);
        if (health <= 0) {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enumyScore;
            int ran = Random.Range(0, 10);
            if(ran < 5) {
                Debug.Log("not itme");
            }
            else if(ran < 8) {
                Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
			}
            else if(ran < 9) {
                Instantiate(itemPower, transform.position, itemCoin.transform.rotation);
            }
            else if(ran < 10) {
                Instantiate(itemBoom, transform.position, itemCoin.transform.rotation);
            }
            gameObject.SetActive(false);
        }
    }
    private void RetunSprite() {
        spriteRenderer.sprite = sprites[0];
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "borderBullet") {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Bullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            //gameObject.SetActive(false);
        }
    }
}