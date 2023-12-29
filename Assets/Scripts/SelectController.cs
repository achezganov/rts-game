using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SelectController : MonoBehaviour
{
    public GameObject mainCube;
    private Camera camera;
    public LayerMask layer;
    public LayerMask _layerPlayer;
    private GameObject _cubeSelect;
    private RaycastHit _hit;
    public List<GameObject> players;
    
    private float defaultY = 0.2f;

    private void Awake()
    {
        camera = GetComponent<Camera>(); // инициализация камеры 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // очищаем массив players
            players.Clear();
            
            // инициализация луча 
            Ray _ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 1000.0f, layer));
                // создание куба в первоначальной координате 
                _cubeSelect = Instantiate(mainCube, new Vector3(_hit.point.x, defaultY, _hit.point.z), Quaternion.identity);
                
        }

        if (_cubeSelect)
        {
            Ray _ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit _hitDrag, 1000.0f, layer))
            {
                float xScale = (_hit.point.x - _hitDrag.point.x), 
                      zScale = _hit.point.z - _hitDrag.point.z;
                
                // разворачиваем куб если координаты отрицательные 
                if (xScale < 0.0f && zScale < 0.0f)
                    _cubeSelect.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else if (xScale < 0.0f)
                    _cubeSelect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                else if (zScale < 0.0f)
                    _cubeSelect.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                else
                    _cubeSelect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                // растягивание куба на дельту (конечная точка - начальная)
                _cubeSelect.transform.localScale = new Vector3(Mathf.Abs(xScale), defaultY, Mathf.Abs(zScale));
            }
            

        }
        
        if (Input.GetMouseButtonUp(0) && _cubeSelect)
        {
            // выборка объектов внутри периметра cubeSelection
            // собираем объекты внутри куба в массив
            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelect.transform.position,
                _cubeSelect.transform.localScale,
                Vector3.up,
                Quaternion.identity,
                0,// говорит о том, что мы выбираем только те объекты, которые внутри куба 
                _layerPlayer);
            
            //перебираем объекты в массиве
            foreach (var el in hits)
            {
                players.Add(el.transform.gameObject); // обращаемся как к игровому объекту
            }
            
            Destroy(_cubeSelect);
        }
    }
}
