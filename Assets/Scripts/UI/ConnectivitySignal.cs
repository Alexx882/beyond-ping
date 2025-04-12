using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ConnectivitySignal : MonoBehaviour
    {
        public RawImage signalImage;
        public Texture[] signalTextures;
        public float currentDistance;
        public float maxDistance;

        private float _factor;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _factor = signalTextures.Length / maxDistance;
        }

        // Update is called once per frame
        void Update()
        {
            var index = Mathf.Min(Mathf.FloorToInt(_factor * currentDistance), signalTextures.Length - 1);
            signalImage.texture = signalTextures[index];
        }
    }
}
