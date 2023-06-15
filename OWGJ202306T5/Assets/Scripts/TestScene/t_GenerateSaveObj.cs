using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_GenerateSaveObj : MonoBehaviour
{
    Vector2 mousePos;
    [SerializeField] private GameObject saveObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(saveObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
        }
    }
}
