using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject warningSignal;
        public GameObject lowConnectivityText;
        public GameObject signalLostText;
        public ConnectivitySignal connectivitySignal;
        public DistanceUI connectivityDistanceUI;
        public PlayerMovement playerMovement;
        public float connectivityDistanceWarning;
        public float maxDistance;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            connectivitySignal.maxDistance = maxDistance;
            warningSignal.SetActive(false);
            lowConnectivityText.SetActive(false);
            signalLostText.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            var currentDistance = playerMovement.GetDistanceToCommander();
            connectivitySignal.currentDistance = currentDistance;
            connectivityDistanceUI.ping = Mathf.RoundToInt(playerMovement.GetCurrentDelayInMilliseconds());

            if (playerMovement.isAlive)
            {
                warningSignal.SetActive(currentDistance > connectivityDistanceWarning);
                lowConnectivityText.SetActive(currentDistance > connectivityDistanceWarning &&
                                              currentDistance < maxDistance);
                signalLostText.SetActive(currentDistance >= maxDistance);
                
                if (currentDistance >= maxDistance)
                {
                    SoundManager.PlaySound(SoundType.LOST, 1.0f);
                    playerMovement.isAlive = false;
                    playerMovement.trailRenderer.emitting = false;
                }
            }
            else
            {
                warningSignal.SetActive(true);
                signalLostText.SetActive(true);
                lowConnectivityText.SetActive(false);
            }
        }
    }
}
