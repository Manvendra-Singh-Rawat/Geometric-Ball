using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    public float smoothspeed = 0.125f;
    [SerializeField]
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredposition = target.position + offset;
        Vector3 smoothposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
        transform.position = smoothposition; 
    }
}