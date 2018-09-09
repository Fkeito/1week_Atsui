using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameObject player;
    public MoveMode moveMode = MoveMode.dontMove;
    public float moveDistance;
    public float moveSpeed;
    public Vector2 moveDirection;
    private Vector3 _moveDirection;

    private bool move = false;
    private float dir = 1;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player"); 
        if (moveMode == MoveMode.dontMove) Destroy(this);
        if (moveMode != MoveMode.meteor) move = true;

        _moveDirection = new Vector3(moveDirection.x, moveDirection.y).normalized;
	}
	
	// Update is called once per frame
	void Update () {
        if(!move) if(this.transform.position.y - player.transform.position.y < moveDistance){
                move = true;
        }

        if(move){
            if (this.transform.position.x > 13)
            {
                dir = 1f;
                if(moveMode == MoveMode.bird) this.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            }
            if (this.transform.position.x < -14)
            {
                if (moveMode == MoveMode.bird)
                {
                    dir = -1f;
                    this.transform.localScale = new Vector3(-0.3f, 0.3f, 1);
                }
                if (moveMode == MoveMode.plane)
                {
                    Vector3 tmp = this.transform.position;
                    tmp.x = 14;
                    this.transform.position = tmp;
                }
            }
            this.transform.position += moveSpeed * dir * _moveDirection * Time.deltaTime;
        }


	}
}
public enum MoveMode{
    dontMove,
    bird,
    plane,
    meteor,
}
