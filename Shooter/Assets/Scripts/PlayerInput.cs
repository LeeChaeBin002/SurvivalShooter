using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string AxisVertical = "Vertical";
    public static readonly string AxisHorizontal = "Horizontal";
    public static readonly string AxisFire1 = "Fire1";

    public float Move { get; private set; }
    public float Rotate { get; private set; }
    public bool Fire { get; private set; }
   

    private void Update()
    {
        Move = Input.GetAxis(AxisVertical);
        Rotate = Input.GetAxis(AxisHorizontal);
        Fire = Input.GetButton(AxisFire1);
    }
}
