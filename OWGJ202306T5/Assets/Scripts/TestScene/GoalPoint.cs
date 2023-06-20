using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    /// <summary>
    /// ゴール判定通知用
    /// ほぼチェックポイント用のコピペ
    /// 助けてくれ
    /// 
    /// S2G215
    /// </summary>
    private GameObject m_gameManagerObject;
    private GameManager_test m_gameManagerScript;

    private void Awake()
    {
        m_gameManagerObject = GameObject.FindWithTag("GameManager");
        m_gameManagerScript = m_gameManagerObject.GetComponent<GameManager_test>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_gameManagerScript.Is_Goal = true;
            Debug.Log("GoalFlagIsTrue!");
        }
    }
}
