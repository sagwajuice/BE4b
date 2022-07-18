using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxShotDelay;
    public float curShotDelay;
    public int playerPower;
    public int playerMaxPower;
    public int life;
    public int score;
    public int boom;
    public int boomMax;
    public float speed;
    public bool isTouchTop;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isTouchBottom;
    public bool isHit;
    public bool isBoomTime;
    public Animator anim;
    public GameObject bullet01;
    public GameObject bullet02;
    public GameObject boomEffect;
    public GameManager gameManager;
    public ObjPoolManager objManager;
//----------------------------------------
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    void Update() {
        Move();
        Fire();
        Reload();
        Boom();
    }
    private void Move() {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchLeft && h == -1) || (isTouchRight && h == 1)) {
            h = 0;
        }
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) {
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) {
            anim.SetInteger("Input", (int)h);
        }
    }
    void Fire() {
        if (!Input.GetButton("Fire1")) {
            return;
        }
        if(curShotDelay < maxShotDelay) {
            return;
        }
        switch (playerPower) {
            case 1:
                GameObject bullet = objManager.MakeObj("BulletPlayerA");
            bullet.transform.position = transform.position;
            Debug.Log("aa");
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletL = Instantiate(bullet01, transform.position + Vector3.left * 0.1f, transform.rotation);
                GameObject bulletR = Instantiate(bullet01, transform.position + Vector3.right * 0.1f, transform.rotation);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletLL = Instantiate(bullet01, transform.position + Vector3.left * 0.4f, transform.rotation);
                GameObject bulletCC = Instantiate(bullet02, transform.position, transform.rotation);
                GameObject bulletRR = Instantiate(bullet01, transform.position + Vector3.right * 0.4f, transform.rotation);
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }
        curShotDelay = 0;
    }
    void Reload() {
        curShotDelay += Time.deltaTime;
    }
    void Boom() {
		if(!Input.GetButton("Fire2")) {
            return;
		}
        if(boom == 0) {
            return;
		}
        if(isBoomTime) {
            return;
        }
        isBoomTime = true;
        boom--;
        gameManager.UpdateBoomIcon(boom);
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int index = 0; index < enemies.Length; index++) {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnumyBullet");
        for(int index = 0; index < enemies.Length; index++) {
            Destroy(bullets[index]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Border") {
            switch(collision.gameObject.name) {
                case "wall (0)":
                    isTouchTop = true;
                    break;
                case "wall (1)":
                    isTouchLeft = true;
                    break;
                case "wall (2)":
                    isTouchRight = true;
                    break;
                case "wall (3)":
                    isTouchBottom = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnumyBullet") {
            if (isHit) {
                return;
            }
            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            if(life == 0) {
                gameManager.GameOver();
            }
            else {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Item") {
            Item _item = collision.gameObject.GetComponent<Item> ();
            switch (_item.type) {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if(playerPower == playerMaxPower) {
                        score += 500;
                    }
                    else {
                    playerPower++;
                    }
                    break;
                case "Boom":
                    if(boom == boomMax) {
                        score += 500;
                    }
                    else {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                }
                    break;
            }
        }
        gameObject.SetActive(false); ;
    }
    void OffBoomEffect() {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border") {
            switch (collision.gameObject.name) {
                case "wall (0)":
                    isTouchTop = false;
                    break;
                case "wall (1)":
                    isTouchLeft = false;
                    break;
                case "wall (2)":
                    isTouchRight = false;
                    break;
                case "wall (3)":
                    isTouchBottom = false;
                    break;
            }
        }
    }
}
