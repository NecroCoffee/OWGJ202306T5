using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]private GameObject Door;
    [Header("�h�A���㉺�ɊJ������l")]
    [SerializeField] private float DoorMaxOpen=0;

    private bool Is_activeSwitch=false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void Update()
    {
        
    }
}
