using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_GenerateSaveObj : MonoBehaviour
{
    /// <summary>
    /// テスト用にセーブオブジェクトをマウスの地点に生成
    /// ビルド用には組み込まないこと
    /// ただし、「a」とコメントが付いている行はGameManagerのセーブオブジェクト関連のbool変数を弄ってるので
    /// 先輩が作ってくれたスクリプトのいい感じの行に追加する
    /// 
    /// S2G215
    /// </summary>


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
            m_gameManagerScript.Is_saveObjectActive = true;//a
            m_gameManagerScript.Is_canSaveObjectGenerate = false;//a
            Instantiate(saveObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
            
        }
    }
}
