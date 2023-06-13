using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの操作
/// 聖山由梨
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // 移動
    [SerializeField] GameObject player;          // プレイヤーのオブジェクト
    [SerializeField] Vector3 playerPos;          // プレイヤーの位置
    [SerializeField] float amoMove = 1.0f;      // 移動量
    private float xPos;

    // ジャンプ
    private Rigidbody2D playerCollider;          // プレイヤーの当たり判定
    private float jumpForce = 300f;              // ジャンプ力
    private bool jump = false;                   // ジャンプ中かどうか

    // 投げる関連
    private float throwPower = 0.0f;    // 投げる強さ
    [SerializeField] LineRenderer lineRenderer;
    private int playerPosRenderer = 0;
    private int targetPosRenderer = 1;
    private Vector3 targetPos;

    // test
    [SerializeField] GameObject test;
    private Vector3 rote;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの位置を取得
        // SerializeFieldにしてインスペクターから指定してもいいが、色々いじってる間に参照が外れる可能性がある
        playerPos = player.GetComponent<Transform>().position;  

        // プレイヤーのコライダーを取得
        playerCollider = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // 左右移動
        xPos = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3 (xPos, 0) * amoMove * Time.deltaTime);

        // ジャンプ
        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            playerCollider.AddForce(transform.up * jumpForce);
        }

        // 角度を決める
        if (Input.GetMouseButton(0)) {
            throwPower += Time.deltaTime;
            Debug.Log(throwPower);
        }

        // セーブポイントを投げる
        if (Input.GetMouseButtonUp(0)) SaveThrow();

    }

    /// <summary>
    /// 床についてるときのみジャンプ可
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

    /// <summary>
    /// セーブポイントを投げる処理
    /// </summary>
    void SaveThrow()
    {
        // test
        playerPos = new Vector3(0, 0, 0);
        targetPos = new Vector3(2, 0, 0);
        rote = new Vector3(0, 0, throwPower);

        // 照準表示
        lineRenderer.SetPosition(playerPosRenderer, playerPos);
        lineRenderer.SetPosition(targetPosRenderer, targetPos);

        // 投げる方向を決める
        //targetPos = new Vector3(playerPos.x + 2, playerPos.y + throwPower, 0);

        throwPower = 0;  // 初期化
    }

}
