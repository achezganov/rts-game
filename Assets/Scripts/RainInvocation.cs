using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainInvocation : MonoBehaviour
{
    public Light dirLight; // сслыка на directionLight
    private ParticleSystem _ps; // объявление переменной типа part. sys.
    private bool _isRain = true; 

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>(); // инициализация
        StartCoroutine(Weather()); // запуск куротины
        
    }

    private void Update()
    {
        if (_isRain && dirLight.intensity > 0.3f)
            LightIntensity(-1);
        else if (!_isRain && dirLight.intensity < 1.0f)
            LightIntensity(1);

    }

    private void LightIntensity(int mult)
    {
        dirLight.intensity += 0.1f * Time.deltaTime * mult;
    }

    IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));
            if (_isRain) _ps.Stop();
            else _ps.Play();
            _isRain = !_isRain;
        }
    }
}
