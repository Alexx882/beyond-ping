using TMPro;
using UnityEngine;

namespace UI
{
    public class DistanceUI : MonoBehaviour
    {
        public int ping;
        public TextMeshProUGUI text;

        private string _currentText;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            text.text = "0ms";
        }

        // Update is called once per frame
        void Update()
        {
            text.text = $"{Mathf.RoundToInt(ping)}ms";
        }
    }
}