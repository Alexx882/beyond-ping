using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        var dir = target.position - transform.position;
        
        var angle = (Mathf.Atan2(dir.y, dir.x) + 135) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
