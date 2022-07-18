using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;
    float viewHeight;

	private void Awake() {
        viewHeight = Camera.main.orthographicSize;
	}
	void Update() {
        Move();
        Scrolling();
    }
	private void Move() {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }
    private void Scrolling() {
        if(sprites[endIndex].position.y < 12 * (-1)) {
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * 12;
            int sratrIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = sratrIndexSave - 1 == -1 ? sprites.Length - 1 : sratrIndexSave - 1;
        }
    }

}