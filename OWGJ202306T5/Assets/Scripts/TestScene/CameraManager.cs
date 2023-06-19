using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]private GameObject m_cameraTarget;
    private Vector2 m_Pos;
    private Vector3 m_cameraPos;

    private bool Is_findCameraTarget=false;

    [SerializeField] GameManager_test gameManager;
    

    private void Awake()
    {
        

    }

    private void Start()
    {
        m_Pos = Camera.main.transform.position;
        m_cameraTarget = gameManager.player;
    }

    private void Update()
    {

        if (Is_findCameraTarget == false)
        {
            m_cameraTarget = GameObject.FindWithTag("Player");
            Is_findCameraTarget = true;
        }

        m_cameraPos = m_cameraTarget.transform.position;
        /*
        if (m_cameraTarget.transform.position.x < 0)
        {
            m_cameraPos.x = 0;
        }
        */
        if (m_cameraTarget.transform.position.y < 0)
        {
            m_cameraPos.y = 0;
        }

        if (m_cameraTarget.transform.position.y > 0)
        {
            m_cameraPos.y = m_cameraTarget.transform.position.y;
        }

        
        m_cameraPos.z = -10;
        
        Camera.main.transform.position = m_cameraPos;
    }

}
