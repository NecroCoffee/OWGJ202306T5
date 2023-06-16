using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ThrowArc : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove; // 初速度や生成座標を持つコンポーネント

    private bool drawArc = true;            // 放物線を描画しているか
    private int segmentCount = 60;          // 放物線を構成する線分の数
    private float predictionTime = 6.0f;    // 放物線を何秒分計算するか
    private float arkWidth = 0.02f;         // 放物線の幅
    private LineRenderer[] lineRenderers;   // 放物線を構成するLineRenderer
    [SerializeField] Vector3 startPosition; // 放物線の開始座標

    private Vector3 initialVelocity;    // 初速度
    private float throwPower = 1.0f;    // 投げる強さ


    // Start is called before the first frame update
    void Start()
    {
        /*
       // LineRendererを用意
       CreateLineRengererObjects();
       */
    }

    // Update is called once per frame
    void Update()
    {

        /*
        // 初速度を更新
        if (Input.GetMouseButton(0)) throwPower += throwPower * Time.deltaTime;

        if (drawArc)
        {
            // 放物線を表示
            float timeStep = predictionTime / segmentCount;
            bool draw = false;
            float hitTime = float.MaxValue;
            for (int i = 0; i < segmentCount; i++)
            {
                // 線の座標を更新
                float startTime = timeStep * i;
                float endTime = startTime + timeStep;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                // 衝突判定
                if (!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; // 衝突したらその先の放物線は表示しない
                    }
                }
            }
        }
        else
        {
            // 放物線を表示しない
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].enabled = false;
            }
        }
        */
    }

    /*
    /// <summary>
    /// 指定時間に対するアーチの放物線上の座標を返す
    /// </summary>
    /// <param name="time">経過時間</param>
    /// <returns>座標</returns>
    private Vector3 GetArcPositionAtTime(float time)
    {
        return (startPosition + ((initialVelocity * time) + (0.5f * time * time) * Physics.gravity));
    }

    /// <summary>
    /// LineRendererの座標を更新
    /// </summary>
    /// <param name="index"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    private void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {
        lineRenderers[index].SetPosition(0, GetArcPositionAtTime(startTime));
        lineRenderers[index].SetPosition(1, GetArcPositionAtTime(endTime));
        lineRenderers[index].enabled = draw;
    }

    /// <summary>
    /// 放物線描画用のLineRendererを作成
    /// </summary>
    private void CreateLineRengererObjects()
    {
        // 親を作成
        GameObject arcObjectsParent = new GameObject("ArcObject");
        lineRenderers = new LineRenderer[segmentCount];
        // LineRendererを持つ子を作成
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject newObject = new GameObject("LineRenderer_" + i);
            newObject.transform.SetParent(arcObjectsParent.transform);
            lineRenderers[i] = newObject.AddComponent<LineRenderer>();

            // 線の幅
            lineRenderers[i].startWidth = arkWidth;
            lineRenderers[i].endWidth = arkWidth;
            lineRenderers[i].numCapVertices = 5;    // 角を丸くする
            lineRenderers[i].enabled = false;
        }
    }

    /// <summary>
    /// 2点間の線分で衝突判定し、衝突する時間を返す
    /// </summary>
    /// <returns>衝突した時間(してない場合はfloat.MaxValue)</returns>
    private float GetArcHitTime(float startTime, float endTime)
    {
        // Linecastする線分の始終点の座標
        Vector3 startPosition = GetArcPositionAtTime(startTime);
        Vector3 endPosition = GetArcPositionAtTime(endTime);

        // 衝突判定
        RaycastHit hitInfo;
        if (Physics.Linecast(startPosition, endPosition, out hitInfo))
        {
            // 衝突したColliderまでの距離から実際の衝突時間を算出
            float distance = Vector3.Distance(startPosition, endPosition);
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        return float.MaxValue;
    }
    */
}
