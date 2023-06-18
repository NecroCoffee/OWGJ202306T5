using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointUpdate_test : MonoBehaviour
{
    /// <summary>
    /// Q なにこれ?
    /// A チェックポイントの座標更新用スクリプト
    /// チェックポイントにアタッチしてPlayerタグが付いたオブジェクトとの接触を感知したらGameManagerのチェックポイント保存変数を更新する
    /// 多分増やしても大丈夫そう 
    ///
    /// S2G215
    /// </summary>
    private GameObject m_gameManagerObject;
    [SerializeField]private Vector2 m_thisObjectPos;

    private GameManager_test m_gameManagerScript;

    private void Awake()
    {
        m_thisObjectPos = this.gameObject.transform.position;
        m_gameManagerObject = GameObject.FindWithTag("GameManager");
        m_gameManagerScript = m_gameManagerObject.GetComponent<GameManager_test>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_gameManagerScript.currentCheckPointPos = m_thisObjectPos;
            m_gameManagerScript.Is_checkPointIsActive = true;
            Debug.Log("CheckPointActive!{Pos.x="+m_thisObjectPos.x+":Pos.y="+m_thisObjectPos.y+"}");
        }
    }
}
