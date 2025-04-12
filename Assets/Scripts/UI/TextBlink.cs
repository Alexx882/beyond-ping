using System;
using UnityEngine;

namespace UI
{
    public class TextBlink : MonoBehaviour
    {

        public Material textMaterial;
        public float speed;

        public float minSoftness;
        public float maxSoftness = 0.2f;
        private static int _softnessPropertyID = Shader.PropertyToID("_OutlineSoftness");
        private float _currentSoftness = 0;
        private float _minSoftness;
        private float _maxSoftness;

        public float minDilate;
        public float maxDilate = 0.3f;
        private static int _dilatePropertyID = Shader.PropertyToID("_FaceDilate");
        private float _currentDilate = 0;
        private float _minDilate;
        private float _maxDilate;

        private float _t = 0;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _t = 0;
            _minSoftness = minSoftness;
            _maxSoftness = maxSoftness;
            _minDilate = minDilate;
            _maxDilate = maxDilate;
        }

        // Update is called once per frame
        void Update()
        {
            _currentSoftness = Mathf.Lerp(_minSoftness, _maxSoftness, _t);
            _currentDilate = Mathf.Lerp(_minDilate, _maxDilate, _t);
        
            _t += speed * Time.deltaTime;
        
            textMaterial.SetFloat(_softnessPropertyID, _currentSoftness);
            textMaterial.SetFloat(_dilatePropertyID, _currentDilate);
        
            Console.Out.WriteLine(_t);
            Console.Out.WriteLine(_currentSoftness);

            if (_t > 1)
            {
                (_minSoftness, _maxSoftness) = (_maxSoftness, _minSoftness);
                (_minDilate, _maxDilate) = (_maxDilate, _minDilate);
                _t = 0;
            }
        }
    }
}
