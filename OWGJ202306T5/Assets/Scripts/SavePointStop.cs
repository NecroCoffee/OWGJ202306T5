using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointStop : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;
    Rigidbody2D rd;
    public Vector3 savePosition { get; private set; }

    /// <summary>
    /// 着弾地点でセーブポイントを停止させる
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rd = gameObject.GetComponent<Rigidbody2D>();
        savePosition = gameObject.transform.position;
        rd.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
