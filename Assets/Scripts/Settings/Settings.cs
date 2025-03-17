public static class Settings
{
    //Global
    //-- Basic Movement
    public const float maxWalkingVelocity = 2f;
    public const float maxRuningVelocity = 5f;
    public const float acceleration = 3f;
    public const float runAcceleration = .2f;

    //-- Camera Movement
    public const float mouseSensitivity = .05f;


    //FPS related
    public const float peekSpeed = 0.05f;
    public const float jumpForce = 1f;
    public const float armLerpSpeed = 5f;
    public const float armSwayStrenght = .5f;
    public const float adsSpeed = 1f;


    //Souls-like related
    public const float cameraDistance = 3f;
    public const float cameraDistanceMin = 1f;
    public const float cameraFalloffTime = .1f;
    public const float cameraYOffset = .7f;
    public const float playerYawStrenght = .2f;



    //NON STATIC
    public static bool canMoveCamera = true;
}
