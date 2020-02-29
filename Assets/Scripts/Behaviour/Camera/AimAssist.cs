using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
    public Locomotor locomotor;
    public Directional directional;
    public Transform target;
    public Transform aimObject;
    public Vector3 orbOffset;

    void Update()
    {
        Vector3 mousePosition = Camera.main.WorldToScreenPoint(target.position) - Input.mousePosition;
        Vector3 orientation = -Camera.main.transform.up;
        float cameraDirToPlayerDeg = AngleHelper.GetAngleFromDirection(AngleHelper.GetDirection(Camera.main.transform.position, transform.position));

        directional.angle =
            AngleHelper.ClampAngleDegree(AngleHelper.ClampAngleDegree(
                cameraDirToPlayerDeg +
                FindDegree(orientation.x - mousePosition.x, orientation.y - mousePosition.y)
            ) - 90);

        directional.direction.Set(
            Mathf.Cos(directional.angle * Mathf.Deg2Rad),
            0,
            Mathf.Sin(directional.angle * Mathf.Deg2Rad)
        );

        aimObject.transform.localPosition = new Vector3(
            directional.direction.x * directional.radius,
            0F,
            directional.direction.z * directional.radius
        ) + orbOffset;
    }

    public static float FindDegree(float x, float y)
    {
        float value = ((Mathf.Atan2(y, x) / Mathf.PI) * 180f);

        if (value < 0)
            value += 360f;

        return value;
    }
}
