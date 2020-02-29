using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Locomotor : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public bool canMove = true;
    public bool isMoving;
    public bool isMovingToItem;
    private float distanceOffset;
    public float speed;
    private Vector3 lastPosition;
    public bool isDestinationLock;

    [HideInInspector]
    public float speedDefault;
    /* returns a vector3 direction between -1, 1 */
    public Vector3 Direction
    {
        private set;
        get;
    }
    public Vector3 Destination
    {
        get;
        private set;
    }
    public float DestinationDistance
    {
        get;
        private set;
    }
    public float FacingAngleDeg
    {
        get;
        private set;
    }

    private void Start()
    {
        speedDefault = speed;
        lastPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        ForceStop();
    }

    private void SetDestination(Vector3 destination)
    {
        if (isDestinationLock)
            return;

        this.Destination = destination;
        this.distanceOffset = 0;
    }

    private void SetDestination(Vector3 destination, float distanceOffset)
    {
        if (isDestinationLock)
            return;

        this.Destination = destination;
        this.distanceOffset = distanceOffset;
        this.distanceOffset = 0;
    }

    private void SetDestionationLock(Vector3 destination)
    {
        this.Destination = destination;
        this.isDestinationLock = true;
        this.distanceOffset = 0;
    }

    private void SetDestionationLock(Vector3 destination, float distanceOffset)
    {
        this.Destination = destination;
        this.isDestinationLock = true;
        this.distanceOffset = distanceOffset;
    }

    public void MoveTo(Vector3 position, bool forceMove)
    {
        if (forceMove)
            SetDestionationLock(position);
        else
            SetDestination(position);

        Move();
    }

    public void MoveTo(Vector3 position, bool forceMove, float offset)
    {
        if (forceMove)
            SetDestionationLock(position, offset);
        else
            SetDestination(position, offset);

        Move();
    }

    public void Move()
    {
        canMove = true;
    }

    public void Stop()
    {
        Destination = transform.position;

        if (!isDestinationLock)
        {
            canMove = false;
            isMoving = false;
        }
    }

    public void ForceStop()
    {
        Destination = transform.position;
        canMove = false;
        isMoving = false;
    }

    private void FixedUpdate()
    {
        if (Destination != null)
        {
            // properties update
            Direction = AngleHelper.GetDirection(transform.position, Destination);
            DestinationDistance = Vector3.Distance(transform.position, Destination);
            FacingAngleDeg = AngleHelper.GetFacingAngleInDegree(Camera.main.transform.position, this);
        }
    }

    void Update()
    {
        if (Destination != null)
        {
            if (isDestinationLock && Vector3.Distance(transform.position, Destination) < distanceOffset + 0.1f)
            {
                isDestinationLock = false;
                Destination = transform.position;
                return;
            }

            if (Vector3.Distance(transform.position, Destination) < 0.01f)
            {
                Destination = transform.position;
                ForceStop();
            }
            else
            {
                if (canMove)
                {
                    isMoving = true;
                    _rigidbody.MovePosition(transform.position + (Direction * speed * Time.deltaTime));
                }

                lastPosition = transform.position;
            }
        }
    }
}
