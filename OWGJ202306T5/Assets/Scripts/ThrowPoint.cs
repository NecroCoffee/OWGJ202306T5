using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 軌道の計算(没)
/// 聖山由梨
/// </summary>
public class ThrowPoint : MonoBehaviour
{
    /*[SerializeField] GameObject player;         // プレイヤーのオブジェクト
    Vector3 playerPos;                          // プレイヤーの位置
    //GameObject savePoint;
    Rigidbody2D savePointRd;
    //Vector3 initialVelocity;
    // 投げる関連
    //private float throwPower = 0.0f;    // 投げる強さ
    private float throwAngle = 0.0f;         // 投げる角度
    [SerializeField] GameObject savePointPre;  // セーブポイントのプレハブ

    private bool throwSamePoint = false;        // セーブポイントを投げているか

    [SerializeField] LineRenderer simuLine = null; // 予測した軌道
    Vector3 gravity = new Vector3(0f, -9.8f, 0f);   // 重力

    // test
    [SerializeField] GameObject test;
    private Vector3 rote;
    private int playerPosRenderer = 0;
    private int targetPosRenderer = 1;
    private Vector3 targetPos;


    // Update is called once per frame
    void Update()
    {
        // 軌道を決める
        if (throwPower <= 500f && Input.GetMouseButton(0)) throwPower += 5.0f;
        if (Input.GetMouseButtonDown(0)) StartCoroutine(Simulation());

        // セーブポイントを投げる
        if (Input.GetMouseButtonUp(0)) SaveThrow();

        if (throwSamePoint)
        {
            savePointRd.AddForce(gravity, (ForceMode2D)ForceMode.Acceleration);
            savePointRd.AddForce(initialVelocity, (ForceMode2D)ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// 軌道の予測
    /// </summary>
    /// <returns></returns>
    private IEnumerator Simulation()
    {
        // 操作キーをオフにする

        // プレイヤーの現在位置を取得
        playerPos = player.transform.position;
        // 運動を停止させ処理負荷をなくす
        Physics.autoSimulation = false;
        // セーブポイントを生成
        Vector3 savePointPlace = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z);
        savePoint = Instantiate(savePointPre, savePointPlace, Quaternion.identity);
        // 予測ポイントを格納するリスト
        //List<Vector3> points = new List<Vector3> { savePointRd.transform.position };

        // 角度を決定
        Vector3 from = playerPos;
        Vector3 to = Input.mousePosition;
        Vector3 planeNormal = Vector3.up;   // 上向き
        Vector3 planeFrom = Vector3.ProjectOnPlane(from,planeNormal);
        Vector3 planeTo = Vector3.ProjectOnPlane(to,planeNormal);
        throwAngle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);

        float vx = throwPower * Mathf.Cos(throwAngle * Mathf.Deg2Rad);
        float vy = throwPower * Mathf.Sin(throwAngle * Mathf.Deg2Rad);
        initialVelocity = new Vector3(vx, vy, 0);

        
        // 軌道をシミュレーション
        for (int i = 0; i < 16; i++)
        {
            Physics.Simulate(Time.deltaTime);
            points.Add(savePointRd.position);
        }
        // 元の位置へ
        savePointRd.velocity = Vector3.zero;
        savePointRd.transform.position = playerPos;
        // 予測地点をつなぐ
        simuLine.positionCount = points.Count;
        simuLine.SetPositions(points.ToArray());
        

        Physics.autoSimulation = true;

        yield return new WaitForFixedUpdate();
    }

    /// <summary>
    /// セーブポイントを投げる処理
    /// </summary>
    private void SaveThrow()
    {
        throwSamePoint = true;

        // test
        playerPos = new Vector3(0, 0, 0);
        targetPos = new Vector3(2, 0, 0);
        rote = new Vector3(0, 0, throwPower);

        // 照準表示
        simuLine.SetPosition(playerPosRenderer, playerPos);
        simuLine.SetPosition(targetPosRenderer, targetPos);

        throwPower = 0;  // 初期化
    }*/
}
