using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    // Movement Variables
    private float speed = 5f;
    private float rotation_speed = 10f;
    private float traction = 0.25f;
    private float rotation_traction = 0.5f;

    // Current Target Position And Rotation
    [SerializeField] private Vector3 target_pos;
    [SerializeField] private Quaternion target_rot;

    // Move/Rotate Towards Target Position/Rotation

    void Update()
    {
        MoveTowards(target_pos);
        RotateTowards(target_rot);
    }

    // Movement And Rotation Formulas
    // Applies ease-out to movement and rotation (delta slows as target arrives towards destination).

    protected void MoveTowards(Vector3 target)
    {
        var move_speed = speed * (Vector3.Distance(target, transform.position) + traction) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, move_speed);
    }

    protected void RotateTowards(Quaternion target)
    {
        var move_speed = rotation_speed * (Quaternion.Angle(target, transform.rotation) + rotation_traction) * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, move_speed);
    }

    // Setters

    public void SetTargetPos(Vector3 vec)
    {
        target_pos = vec;
    }

    public void SetTargetRot(Quaternion quat)
    {
        target_rot = quat;
    }

    public void SetSettings(float speed, float rotation_speed, float traction, float rotation_traction)
    {
        this.speed = speed;
        this.rotation_speed = rotation_speed;
        this.traction = traction;
        this.rotation_traction = rotation_traction;
    }
}
