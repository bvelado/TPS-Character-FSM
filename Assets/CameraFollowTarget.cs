using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFollowTarget : MonoBehaviour {

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform target;
    public Transform Target { get { return target; } }

    void Update () {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
	}
}
