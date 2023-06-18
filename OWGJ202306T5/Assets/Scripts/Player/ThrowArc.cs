using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 軌道予測線の描写
/// 聖山由梨
/// </summary>

public class ThrowArc : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove; // 初速度や生成座標を持つコンポーネント

    
    private bool drawArc = true;            // 放物線を描画しているか
    private int segmentCount = 60;          // 放物線を構成する線分の数
    private float predictionTime = 6.0f;    // 放物線を何秒分計算するか
    private float arkWidth = 0.02f;         // 放物線の幅
    private LineRenderer[] lineRenderers;   // 放物線を構成するLineRenderer
    private Vector3 initialVelocity;        // 初速度
    [SerializeField] Vector3 startPosition;
    Vector3 endPosition;

    LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
        // LineRendererを用意
        CreateLineRengererObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        // 放物線を表示
        float timeStep = predictionTime / segmentCount;
        bool draw = false;
        float hitTime = float.MaxValue;
        if (playerMove.nowThrow && drawArc)
        {
            startPosition = playerMove.savePointRd.position;
            for (int i = 0; i < segmentCount; i++)
            {
                // 線の座標を更新
                float startTime = timeStep * i;
                float endTime = startTime + timeStep;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                /*
                // 衝突判定
                if (!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; // 衝突したらその先の放物線は表示しない
                    }
                }*/
            }//for
        }
        else
        {
            // 放物線とマーカーを表示しない
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].enabled = false;
            }
        }//if-else
        
    }

    /// <summary>
    /// t秒後の物体の位置を計算
    /// </summary>
    /// <param name="time">t秒後</param>
    /// <param name="startPosition">座標の開始位置</param>
    /// <returns>座標</returns>
    Vector3 CalcPositionFromForce(float time, Vector3 startPosition)
    {
        float mass = 1f;
        Vector3 force = playerMove.angle * playerMove.throwPower;
        Vector3 gravity = new Vector2(0f, -9.8f);

        Vector3 speed = (force / mass) * Time.fixedDeltaTime;
        Vector3 position = (speed * time) + (gravity * 0.5f * Mathf.Pow(time, 2));

        return startPosition + position;
    }

    /// <summary>
    /// LineRendererの座標を更新
    /// </summary>
    /// <param name="index"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    private void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {

        if (index == 0) 
        {
            startPosition = CalcPositionFromForce(startTime, startPosition);
            lineRenderers[index].SetPosition(0, startPosition);
            endPosition = CalcPositionFromForce(endTime, startPosition);
            lineRenderers[index].SetPosition(1, endPosition);
            lineRenderers[index].enabled = draw;
            return;
        }

        startPosition = endPosition;
        endPosition = CalcPositionFromForce(startTime, startPosition);
        lineRenderers[index].SetPosition(0, endPosition);
        lineRenderers[index].SetPosition(1, CalcPositionFromForce(endTime, endPosition));
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

    /*
    /// <summary>
    /// 2点間の線分で衝突判定し、衝突する時間を返す
    /// </summary>
    /// <returns>衝突した時間(してない場合はfloat.MaxValue)</returns>
    private float GetArcHitTime(float startTime, float endTime)
    {
        // Linecastする線分の始終点の座標
        Vector3 startPosition = CalcPositionFromForce(startTime);
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
