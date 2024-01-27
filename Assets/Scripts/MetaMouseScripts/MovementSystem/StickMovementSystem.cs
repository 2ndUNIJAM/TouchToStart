using UnityEngine;

public class StickMovementSystem : MonoBehaviour
{
    public Vector2 mouseOrigin;
    public float mouseSpeed;
    //public float activeAreaRadius;          //active zone 부분은 다른 스크립트에서 구현하는 게 더 나을지도
    private Vector2 mousePosition;
    private MetaMouse metaMouse;
    
    Vector2 StickMovement(Vector2 mousePosition, Vector2 mouseOrigin, float mouseSpeed)
    {
        Vector2 mouseDisplacement;
        Vector2 mouseVelocity;
        mouseDisplacement = mousePosition - mouseOrigin;
        mouseVelocity = mouseDisplacement * mouseSpeed;

        return mouseVelocity;
    }
}