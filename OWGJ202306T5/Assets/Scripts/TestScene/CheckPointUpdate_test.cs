using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointUpdate_test : MonoBehaviour
{
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
            Debug.Log("CheckPointActive!{Pos.x="+m_thisObjectPos.x+":Pos.y="+m_thisObjectPos.y+"}");
        }
    }
}
