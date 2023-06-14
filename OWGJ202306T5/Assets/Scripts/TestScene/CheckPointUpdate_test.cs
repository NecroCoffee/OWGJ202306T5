using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointUpdate_test : MonoBehaviour
{
    [SerializeField]private GameManager_test m_gameManager;
    private Vector2 m_thisObjectPos;

    private void Awake()
    {
        m_thisObjectPos = this.gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_gameManager.currentCheckPointPos = m_thisObjectPos;
            Debug.Log("CheckPointActive!");
        }
    }
}
