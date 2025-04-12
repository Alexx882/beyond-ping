using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject warningSignal;
        public ConnectivitySignal connectivitySignal;
        public DistanceUI connectivityDistanceUI;
        public float currentDistance;
        public float connectivityDistanceWarning;
        public float maxDistance;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            connectivitySignal.maxDistance = maxDistance;
            warningSignal.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            connectivitySignal.currentDistance = currentDistance;
            connectivityDistanceUI.distance = currentDistance;
            warningSignal.SetActive(currentDistance > connectivityDistanceWarning);
        }
    }
}
