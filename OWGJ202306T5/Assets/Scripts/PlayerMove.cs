using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// プレイヤーの操作(プレイヤーにアタッチ)
/// 聖山由梨
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // 移動
    [SerializeField] GameObject player;        // プレイヤーのオブジェクト
    private Vector3 playerPos;                 // プレイヤーの位置
    [SerializeField] float amoMove = 1.0f;     // 移動量
    public float gravity { get; private set; } = -9.81f;     // 重力
    private float xPos;

    // ジャンプ
    private Rigidbody2D playerRb;          // プレイヤーの当たり判定
    private float jumpForce = 350f;        // ジャンプ力
    private bool jump = false;             // ジャンプ中かどうか

    // 投げる
    [SerializeField] ThrowArc throwArc;
    // セーブポイント
    [SerializeField] GameObject savePoint;          // セーブポイントのプレハブ
    private const string saveTag = "SaveObject";    // セーブポイントのタグ
    private GameObject savePointObj;                // 生成したセーブポイント
    public Rigidbody2D savePointRd { get; private set; }                // 生成したセーブポイントのRigidBody
    // 角度
    public bool nowThrow { get; private set; } = false;      // 投げようとしているか
    Vector3 startPosition;              // 投げ始める位置
    private Vector3 mousePosition;      // マウスの座標
    private Vector3 worldTarget;        // マウスのワールド座標
    // 発射
    private bool throwing = false; // 投げているか
    public float throwPower { get; private set; } = 1.0f;    // 投げる強さ
    public Vector3 angle { get; private set; } // 投げる方向
    public float t { get; private set; }    // 最高点に達するまでの時間
    public float high { get; private set; }    // 最高点の高さ

    //test


    void Start()
    {
        // プレイヤーの位置を取得
        playerPos = player.transform.position;

        // プレイヤーのRigidBodyを取得
        playerRb = player.GetComponent<Rigidbody2D>();

        startPosition = savePoint.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // 左右移動
        xPos = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(xPos, 0) * amoMove * Time.deltaTime);

        // ジャンプ
        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            playerRb.AddForce(transform.up * jumpForce);
        }
        if (jump) playerRb.AddForce(new Vector3(0, gravity, 0), (ForceMode2D)ForceMode.Force);

        // 投げる方向を決める
        if (!nowThrow) mousePosition = Input.mousePosition;

        // 投げる
        if (Input.GetMouseButtonDown(0)) ThrowPoint();
        if (Input.GetMouseButton(0)) throwPower += Time.deltaTime + 1.0f;
        if (Input.GetMouseButtonUp(0)) SaveThrow();
        //if (throwing) Throwing();
    }

    
    private void FixedUpdate()
    {
        if (throwing && savePointRd.transform.position.y >= high) GravityUp();
    }
    

    /// <summary>
    /// 床についてるときのみジャンプ可にする
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

    /// <summary>
    /// 投げる方向の決定
    /// </summary>
    private void ThrowPoint()
    {
        // 投げる強さをリセット
        throwPower = 0;

        // プレイヤーの位置を固定
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        // クリックした座標を取得
        worldTarget = Camera.main.ScreenToWorldPoint(mousePosition);
        worldTarget.z = 0;

        // 指定子オブジェクトと同じ位置にセーブポイントを生成
        startPosition = GameObject.FindWithTag(saveTag).transform.position;
        savePointObj = Instantiate(savePoint, startPosition, Quaternion.identity);
        savePointRd = savePointObj.GetComponent<Rigidbody2D>();

        // 投げる方向を決定
        angle = (worldTarget - playerPos).normalized;

        nowThrow = true;
    }//ThrowPoint()


    /// <summary>
    /// 投げる強さを決定
    /// </summary>
    private void SaveThrow()
    {
        savePointRd.bodyType = RigidbodyType2D.Dynamic;

        // 投げる
        savePointRd.AddForce(angle * throwPower);

        // 最高到達点に達するまでの時間
        t = angle.y / gravity;

        // 最高点の高さ
        high = angle.y * t - 0.5f * gravity * Mathf.Pow(t, 2);

        nowThrow = false;
        throwing = true;
        playerRb.constraints = RigidbodyConstraints2D.None;
    }

    /// <summary>
    /// 軌道の調整
    /// </summary>
    private void GravityUp()
    {
        savePointRd.AddForce(new Vector2(1.0f, -5.0f), (ForceMode2D)ForceMode.Acceleration);
        throwing = false;
    }
}
    
