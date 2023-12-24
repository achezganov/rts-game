using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask _layer;
    public float _rotateSpeed = 60f;
    private void Start()
    {
        CursorPositionObject();
    }
    
    private void Update()
    {
        CursorPositionObject();
        // удаляем компонент после размещения объекта
        if (Input.GetMouseButtonDown(0)) 
            Destroy(gameObject.GetComponent<PlaceObjects>());
        // разворачиваем объект
        if(Input.GetKey(KeyCode.R))
            transform.Rotate(Vector3.up * Time.deltaTime * _rotateSpeed);
    }

    private void CursorPositionObject()
    {
        // отслеживаем курсор мыши относительно карты
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

        RaycastHit _hit;
        // кастуем луч (проекцию на граунд)
        if (Physics.Raycast(_ray, out _hit, 1000f, _layer))
            transform.position = _hit.point; // point - vector3
        
    }
}
