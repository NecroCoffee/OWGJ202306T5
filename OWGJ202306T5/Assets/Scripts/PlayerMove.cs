using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// プレイヤーの操作(プレイヤーにアタッチする)
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
    // セーブポイント
    [SerializeField] GameObject savePoint;          // セーブポイントのプレハブ
    private const string saveTag = "SaveObject";    // セーブポイントのタグ
    private GameObject savePointObj;                // 生成したセーブポイント
    private Rigidbody2D savePointRd;                // 生成したセーブポイントのRigidBody
    // 角度
    private bool nowThrow = false;      // 投げようとしているか
    Vector3 startPosition;              // 投げ始める位置
    private Vector3 mousePosition;      // マウスの座標
    private Vector3 worldTarget;        // マウスのワールド座標
    private float throwAngle = 0.0f;    // 投げる角度
    // 発射
    private bool throwing = false; // 投げているか
    private float Travel;               // 水平移動量
    private float speed = 1.0f;         // 投げたセーブポイントが動く速さ
    private float throwPower = 1.0f;    // 投げる強さ
    [SerializeField] Vector3 initialVelocity;            // セーブポイントに加わる力


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
        transform.Translate(new Vector3 (xPos, 0) * amoMove * Time.deltaTime);

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
        if (Input.GetMouseButton(0) ) throwPower += Time.deltaTime + 1.5f;
        if (Input.GetMouseButtonUp(0)) SaveThrow();
        //if (nowThrow) Throwing();

    }

    /*private void FixedUpdate()
    {
        if (throwing)
        {
            Vector3 gravity = new Vector3(0f, -this.gravity, 0f);
            savePointRd.AddForce(initialVelocity, (ForceMode2D)ForceMode.VelocityChange);

            if (savePointObj.transform.position.x <= initialVelocity.x / 2)
            {
                savePointRd.AddForce(gravity, (ForceMode2D)ForceMode.Acceleration);
            }
        }
        
    }*/

    /// <summary>
    /// 床についてるときのみジャンプ可にする
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

    /// <summary>
    /// 投げる角度の計算
    /// </summary>
    private void ThrowPoint()
    {
        // プレイヤーの位置を固定
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        // クリックした座標を取得
        worldTarget = Camera.main.ScreenToWorldPoint(mousePosition);

        // 指定子オブジェクトと同じ位置にセーブポイントを生成
        startPosition = GameObject.FindWithTag(saveTag).transform.position;
        savePointObj = Instantiate(savePoint, startPosition, Quaternion.identity);
        savePointRd = savePointObj.GetComponent<Rigidbody2D>();

        // 角度を計算
        float dx = worldTarget.x - playerPos.x;
        float dy = worldTarget.y - playerPos.y;
        float rad = Mathf.Atan2(dy, dx);
        throwAngle = rad * Mathf.Rad2Deg;  // ラジアンを度に直す

        Debug.Log(throwAngle);

        Travel = 0.0f;
        nowThrow = true;
    }

    /// <summary>
    /// 投げる強さの計算
    /// </summary>
    private void SaveThrow()
    {
        float vx = throwPower * Mathf.Cos(throwAngle);
        float vy = throwPower * Mathf.Sin(throwAngle);
        initialVelocity = new Vector3(vx, vy, 0f);

        savePointRd.bodyType = RigidbodyType2D.Dynamic;
        savePointRd.AddForce(initialVelocity);
        nowThrow = false;
        throwing = true;
        playerRb.constraints = RigidbodyConstraints2D.None;
    }

    /*
    private void Throwing()
    {
        savePointRd.bodyType = RigidbodyType2D.Dynamic;

        // 水平移動量を求める
        Travel += speed * Time.deltaTime;

        // 目的地点までの距離を求める
        worldTarget.y = playerPos.y;  // 高さを合わせる
        float distance = Vector3.Distance(startPosition, worldTarget);
        
        // 進行割合を求める
        var t = Travel * distance;

        if (t < 1.0f) 
        {
            //tが0.5（つまり中間地点）からどれだけ離れているかを求める
            //中間地点で0.0、出発地や目的地で1.0となるような値にする
            var d = Mathf.Abs(t - 0.5f) * 2.0f;

            //現在の水平位置を決め...
            var p = Vector3.Lerp(playerPos, worldTarget, t);

            //高さを二次関数の曲線に沿って調整し...
            p.y += Mathf.Tan(Mathf.Deg2Rad * throwAngle) * 0.25f * distance * (1.0f - (d * d));

            //位置を設定する
            savePointObj.transform.position = p;

            return;
        }

        if (t >= 1.0f)
        {
            //tが1.0に到達したら移動終了とする
            savePointObj.transform.position = worldTarget;
            nowThrow = false;
            playerRb.constraints = RigidbodyConstraints2D.None;
        }
        
    }
    */
}
