using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string Vertical = "Vertical";
    public static readonly string Horizontal = "Horizontal";
    public static readonly string Fire1 = "Fire1";
    public static readonly string ReloadStr = "Reload";

    public float Move { get; set; }
    public float Rot { get; set; }
    public bool Fire { get; set; }
    public bool Reload { get; set; }

    private void Update()
    {
        Move = Input.GetAxis(Vertical);
        Rot = Input.GetAxis(Horizontal);
        Fire = Input.GetButton(Fire1);
        Reload = Input.GetButton(ReloadStr);
    }
}
