using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    //GameObject player_Prefab;

    private Vector2 pos;
    private float horizontal;

    public bool Is_player_canmove = true;//プレイヤーは移動可能かどうか

    public float player_speed = 1.0f;//プレイヤースピード

    public void Player_Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector2(horizontal, 0)*player_speed * Time.deltaTime);

        


    }

    private void Start()
    {
        pos = transform.position;//シーン呼び出し時現在位置取得
    }

    private void Update()
    {
        //移動
        if (Is_player_canmove == true)
        {
            Player_Move();
        }
    }
}
