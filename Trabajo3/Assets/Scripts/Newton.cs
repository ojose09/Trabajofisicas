using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]

public class Newton : MonoBehaviour
{
    private MyVector position;
    private MyVector acceleration;
    public float mass = 1;
    [SerializeField] private MyVector velocity;
    [SerializeField] Newton Target;
    [SerializeField] [Range(0, 1)] float gravity = -9.8f;
    [SerializeField] [Range(0, 1)] float damping = 1;

    private void Start()
    {
        position = transform.position;
    }
    public void Move()
    {
        velocity += acceleration * Time.fixedDeltaTime;
        position += velocity * Time.fixedDeltaTime;

        if (velocity.magnitude >= 3f)
        {
            velocity.Normalize();
            velocity *= 3;
        }
        transform.position = position;
    }
    private void FixedUpdate()
    {
        MyVector r = Target.transform.position - transform.position;
        float Magnituder = r.magnitude;
        MyVector F = r.normalized * (1 / Target.mass * mass / Magnituder * Magnituder);

        float weightScalar = mass * gravity;
        MyVector weight = new MyVector(0, weightScalar);
        acceleration *= 0f;
        ApplyForce(F);
        Move();
        F.Draw2(position, Color.red);
    }
    private void Update()
    {
        velocity.Draw2(position, Color.green);
    }
    void ApplyForce(MyVector force)
    {
        acceleration += force * (1f / mass);
    }
}