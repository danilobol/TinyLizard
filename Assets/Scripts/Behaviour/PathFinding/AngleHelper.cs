using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AngleHelper
{
    /// <summary>
    /// Returns the angle between two vectos
    /// </summary>
    /// 
    public static readonly float rad = Mathf.PI / 180;
    // (-1, 1)
    public static Vector3 GetDirection(Vector3 origin, Vector3 destination)
    {
        Vector3 heading = destination - origin;

        if (heading.magnitude == 0)
            return Vector3.zero;

        Vector3 direction = heading / heading.magnitude;
        direction.z = 0;

        return direction;
    }
    /* 
     * Returns angle in radians.
    */
    public static float GetAngleFromDirection(Vector3 origin, Vector3 destination)
    {
        Vector3 direction = GetDirection(origin, destination);
        return ClampAngleRad(Mathf.Atan2(direction.x * Mathf.PI * 2, direction.z * Mathf.PI * 2));
    }

    /* 
     * Direction should be between -1, 1.
     * Returns angle in radians.
    */
    public static float GetAngleFromDirection(Vector3 direction)
    {
        float t = Mathf.Atan2(direction.z * rad, direction.x * rad) * (180 / Mathf.PI);

        if (t < 0)
            t += 360;

        return t;
    }

    public static float GetAngle(Vector3 A, Vector3 B)
    {
        // |A·B| = |A| |B| COS(θ)
        // |A×B| = |A| |B| SIN(θ)
        return Mathf.Atan2(Cross(A, B), Dot(A, B));
    }

    public static float GetAngle2Deg(Vector3 A, Vector3 B)
    {
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(A.y - B.y, A.x - B.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        return AngleDeg;
    }

    public static float GetFacingAngleInDegree(Vector3 cameraPosition, Locomotor creatureLocomotor)
    {
        float cameraAngle = GetAngleFromDirection(
            GetDirection(
                creatureLocomotor.transform.position,
                cameraPosition
            )
        );

        float characterAngle = GetAngleFromDirection(
            GetDirection(
                creatureLocomotor.transform.position,
                creatureLocomotor.Destination
            )
        );

        return ClampAngleDegree(ClampAngleDegree(characterAngle - cameraAngle) - 90);
    }

    public static float Magnitude(Vector3 A)
    {
        return Mathf.Sqrt(Dot(A, A));
    }

    public static float Dot(Vector3 A, Vector3 B)
    {
        return A.x * B.x + A.z * B.z;
    }

    public static float Cross(Vector3 A, Vector3 B)
    {
        return A.x * B.z - A.z * B.x;
    }

    public static bool Inside(float angle, float min, float max)
    {
        angle = ClampAngleDegree(angle);
        min = ClampAngleDegree(min);
        max = ClampAngleDegree(max);

        return angle >= min && angle < max;
    }

    public static float ClampAngleRad(float angle)
    {
        if (angle > 2 * Mathf.PI)
            angle -= 2 * Mathf.PI;
        else if (angle < 0)
            angle += 2 * Mathf.PI;

        return angle;
    }

    public static float ClampAngleDegree(float angle)
    {
        if (angle > 359)
            angle -= 360;
        else if (angle < 0)
            angle += 360;

        return angle;
    }
}
