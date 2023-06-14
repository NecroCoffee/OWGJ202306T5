using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// プレイヤーの操作
/// 聖山由梨
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // 移動
    [SerializeField] GameObject player;        // プレイヤーのオブジェクト
    private Vector3 playerPos;                 // プレイヤーの位置
    [SerializeField] float amoMove = 1.0f;     // 移動量
    [SerializeField] float gravity = 9.8f;     // 重力
    private float xPos;

    // ジャンプ
    private Rigidbody2D playerRb;          // プレイヤーの当たり判定
    private float jumpForce = 350f;        // ジャンプ力
    private bool jump = false;             // ジャンプ中かどうか

    // 投げる
    [SerializeField] GameObject savePoint;  // セーブポイントのプレハブ
    [SerializeField] Vector3 startPosition;    // 投げ始める位置
    private float throwPower = 1.0f;        // 投げる強さ
    Vector3 initialVelocity;                // 初速度
    private float throwAngle = 0.0f;        // 投げる角度
    private float speed = 1.0f;             // 動く速さ

    void Start()
    {
        // プレイヤーの位置を取得
        // SerializeFieldにしてインスペクターから指定してもいいが、色々いじってる間に参照が外れる可能性がある
        playerPos = player.transform.position;  

        // プレイヤーのコライダーを取得
        playerRb = player.GetComponent<Rigidbody2D>();

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
            playerRb.AddForce(transform.up * jumpForce);
        }
        if (jump) playerRb.AddForce(new Vector3(0, gravity, 0), (ForceMode2D)ForceMode.Force);

        // 投げる
        if (Input.GetMouseButton(0)) 
        {
            // 角度から初速度を決定
            Vector3 from = playerPos;
            Vector3 to = Input.mousePosition;
            Vector3 planeNormal = Vector3.up;   // 上向き
            Vector3 planeFrom = Vector3.ProjectOnPlane(from, planeNormal);
            Vector3 planeTo = Vector3.ProjectOnPlane(to, planeNormal);
            throwAngle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);
            float vx = throwPower * Mathf.Cos(throwAngle * Mathf.Deg2Rad);
            float vy = throwPower * Mathf.Sin(throwAngle * Mathf.Deg2Rad);
            initialVelocity = new Vector3(vx, vy, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameObject obj = Instantiate(savePoint, startPosition, Quaternion.identity);
            Rigidbody2D rd = obj.GetComponent<Rigidbody2D>();
            rd.AddForce(initialVelocity, (ForceMode2D)ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 床についてるときのみジャンプ可にする
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

}
