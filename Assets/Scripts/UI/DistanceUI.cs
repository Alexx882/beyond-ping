using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DistanceUI : MonoBehaviour
    {

        public TextMeshProUGUI text;
        public float distance = 0;
        public int[] roundBreakpoints = {10};
        public float scale = 10f;
    
        private string _currentText;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            text.text = "0km";
        }

        // Update is called once per frame
        void Update()
        {
            text.text = $"{GetDistanceForBreakpoint()}km";
        }

        private int GetDistanceForBreakpoint()
        {
            foreach (var breakpoint in roundBreakpoints.OrderBy((x) => x).Reverse())
            {
                if (distance < breakpoint)
                {
                    continue;
                }
                
                return Mathf.RoundToInt(distance / breakpoint * scale) * breakpoint;
            }

            return Mathf.RoundToInt(distance * scale);
        }
    }
}
