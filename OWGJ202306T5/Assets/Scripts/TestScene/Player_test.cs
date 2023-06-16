using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    //GameObject player_Prefab;
    //[Header("ここにGameManagerオブジェクトを入れる")]
    //public GameObject gameManagerObject;//GameManager保存用

    private Vector2 pos;
    private float horizontal;

    public bool Is_player_canmove = true;//プレイヤーは移動可能かどうか

    [SerializeField]public float player_Speed = 1.0f;//プレイヤースピード

    public void Player_Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector2(horizontal, 0)*player_Speed * Time.deltaTime);


    }

    private void Start()
    {
        pos = transform.position;//シーン呼び出し時現在位置取得
    }

    private void FixedUpdate()
    {
        //移動
        if (Is_player_canmove == true)
        {
            Player_Move();
        }
    }
}
