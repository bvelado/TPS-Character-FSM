using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public string PlayerHorizontalAxisName = "Horizontal";
    public string PlayerVerticalAxisName = "Vertical";

    public Vector3 GetAxisInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public bool GetJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }
}
