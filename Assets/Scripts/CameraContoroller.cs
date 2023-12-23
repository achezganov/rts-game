using System;
using UnityEngine;

public class CameraContoroller : MonoBehaviour
{
    public float _speed = 20.0f;
    public float _rotateSpeed = 20.0f;                  // скорость поворота камеры
    private float _mult = 1.0f;                         // коэф ускорения камеры
    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");        // отслеживаем "a" "d" + arrows
        float ver = Input.GetAxis("Vertical");          // отслеживаем значения "w" "s" + arrows
        
        float _rotate = 0.0f;
        if (Input.GetKey(KeyCode.Q))
            _rotate = -1;
        else if (Input.GetKey(KeyCode.E))
            _rotate = 1;
        
        // тернарное условие, если shift нажат
        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f; 
        
        // разворот камеры
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime * _rotate * _mult, Space.World); 
        // передвижение камеры на wasd || arrows
        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * _speed, Space.World );
    }
}
