using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager: MonoBehaviour
{
	public GameObject enemyLPrefab;
	public GameObject enemyMPrefab;
	public GameObject enemySPrefab;
	public GameObject itemPowerPrefab;
	public GameObject itemBoomPrefab;
	public GameObject itemCoinPrefab;
	public GameObject bulletPlayerAPrefab;
	public GameObject bulletPlayerBPrefab;
	public GameObject bulletEnemyAPrefab;
	public GameObject bulletEnemyBPrefab;
	GameObject[] enemyL;
	GameObject[] enemyM;
	GameObject[] enemyS;
	GameObject[] itemPower;
	GameObject[] itemBoom;
	GameObject[] itemCoin;
	GameObject[] bulletPlayerA;
	GameObject[] bulletPlayerB;
	GameObject[] bulletEnemyA;
	GameObject[] bulletEnemyB;
	GameObject[] targetPool;

	private void Awake() {
		enemyL = new GameObject[10];
		enemyM = new GameObject[10];
		enemyS = new GameObject[10];
		itemPower = new GameObject[10];
		itemBoom = new GameObject[10];
		itemCoin = new GameObject[10];
		bulletPlayerA = new GameObject[100];
		bulletPlayerB = new GameObject[100];
		bulletEnemyA = new GameObject[100];
		bulletEnemyB = new GameObject[100];
		Generate();
	}
	void Generate() {
		for(int index = 0; index < enemyL.Length; index++) {
			enemyL[index] = Instantiate(enemyLPrefab);
			enemyL[index].SetActive(false);
		}
		for(int index = 0; index < enemyM.Length; index++) {
			enemyM[index] = Instantiate(enemyMPrefab);
			enemyM[index].SetActive(false);
		}
		for(int index = 0; index < enemyS.Length; index++) {
			enemyS[index] = Instantiate(enemySPrefab);
			enemyS[index].SetActive(false);
		}
		for(int index = 0; index < itemPower.Length; index++) {
			itemPower[index] = Instantiate(itemPowerPrefab);
			itemPower[index].SetActive(false);
		}
		for(int index = 0; index < itemBoom.Length; index++) {
			itemBoom[index] = Instantiate(itemBoomPrefab);
			itemBoom[index].SetActive(false);
		}
		for(int index = 0; index < itemCoin.Length; index++) {
			itemCoin[index] = Instantiate(itemCoinPrefab);
			itemCoin[index].SetActive(false);
		}
		for(int index = 0; index < bulletPlayerA.Length; index++) {
			bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
			bulletPlayerA[index].SetActive(false);
		}
		for(int index = 0; index < bulletPlayerB.Length; index++) {
			bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
			bulletPlayerB[index].SetActive(false);
		}
		for(int index = 0; index < bulletEnemyA.Length; index++) {
			bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
			bulletEnemyA[index].SetActive(false);
		}
		for(int index = 0; index < bulletEnemyB.Length; index++) {
			bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
			bulletEnemyB[index].SetActive(false);
		}
	}
	public GameObject MakeObj(string type) {
		switch(type) {
			case "EnemyL":
				targetPool = enemyL;
				break;
			case "EnemyM":
				targetPool = enemyM;
				break;
			case "EnemyS":
				targetPool = enemyS;
				break;
			case "ItemPower":
				targetPool = itemPower;
				break;
			case "ItemBoom":
				targetPool = itemBoom;
				break;
			case "ItemCoin":
				targetPool = itemCoin;
				break;
			case "BulletPlayerA":
				targetPool = bulletPlayerA;
				break;
			case "BulletPlayerB":
				targetPool = bulletPlayerB;
				break;
			case "BulletEnemyA":
				targetPool = bulletEnemyA;
				break;
			case "BulletEnemyB":
				targetPool = bulletEnemyB;
				break;
		}
		for(int index = 0; index < targetPool.Length; index++) {
			if(!targetPool[index].activeSelf) {
				targetPool[index].SetActive(true);
				return targetPool[index];
			}
		}
		return null;
	}
}

