using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_GenerateSaveObj : MonoBehaviour
{
    Vector2 mousePos;
    [SerializeField] private GameObject saveObject;

    [SerializeField]private GameObject m_gameManagerObject;
    private GameManager_test m_gameManagerScript;

    private void Start()
    {
        m_gameManagerObject = GameObject.FindWithTag("GameManager");
        m_gameManagerScript = m_gameManagerObject.GetComponent<GameManager_test>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_gameManagerScript.Is_canSaveObjectGenerate == true) 
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_gameManagerScript.Is_saveObjectActive = true;
            m_gameManagerScript.Is_canSaveObjectGenerate = false;
            Instantiate(saveObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
            
        }
    }
}
