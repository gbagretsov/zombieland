using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{

    /*
    This camera smoothes out rotation around the y-axis and height.
    Horizontal Distance to the target is always fixed.

    There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

    For every of those smoothed values we calculate the wanted value and the current value.
    Then we smooth it using the Lerp function.
    Then we apply the smoothed values to the transform's position.
    */

    public Transform target; // Объект, к которому привязана камера
    public float distance = 10.0f; // Дальность
    public float height = 5.0f; // Высота камеры относительно машины

    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    float wantedRotationAngle;
    float wantedHeight; // Желаемая высота относительно машины
    float currentRotationAngle;
    float currentHeight; // Текущая высота относительно машины
    Quaternion currentRotation;

    private MouseInputController mouse;
    private float mouseXOffset;
    private float mouseYOffset;
    public float maxYPosOffset;
    public float minYNegOffset;

    public void Start()
    {
        mouseXOffset = 0;
        mouseYOffset = 0;
        currentHeight = height;
        mouse = GetComponent<MouseInputController>();
    }

    public void LateUpdate()
    {
        // Случай, когда не назначена цель
        if (!target)
            return;

        mouseXOffset = (mouseXOffset + mouse.MouseXInput * mouse.mouseXSensitivity ) % 360;
        mouseYOffset += mouse.MouseYInput;
        if (mouseYOffset > maxYPosOffset)
            mouseYOffset = maxYPosOffset;
        else if (mouseYOffset < minYNegOffset)
            mouseYOffset = minYNegOffset;

        // Calculate the current rotation angles
        wantedRotationAngle = target.eulerAngles.y + mouseXOffset;
        wantedHeight = height + mouseYOffset;

        currentRotationAngle = transform.eulerAngles.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = target.position - currentRotation * Vector3.forward * distance + currentHeight * Vector3.up;
        
        // Always look at the target
        transform.LookAt(target);
    }
}
