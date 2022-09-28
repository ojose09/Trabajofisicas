using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friccion : MonoBehaviour
{
    private MyVector position;
    private MyVector velocity;
    private MyVector acceleration;
    [SerializeField] bool FluidFriction = false;
    [SerializeField] float mass = 1;
    [SerializeField] MyVector wind;
    [SerializeField] [Range(0, 1)] float frictionCoefficient;
    [SerializeField] [Range(0, 1)] float damping = 1;
    [SerializeField] [Range(0, 1)] float gravity = -9.8f;

    private void Start()
    {
        position = transform.position;
    }
    public void Move()
    {
        velocity += acceleration * Time.fixedDeltaTime;
        position += velocity * Time.fixedDeltaTime;
        if (Mathf.Abs(position.x) >= 5)
        {
            position.x = Mathf.Sign(position.x) * 5;
            velocity.x *= -1;
            velocity *= damping;
        }
        if (Mathf.Abs(position.y) >= 5)
        {
            position.y = Mathf.Sign(position.y) * 5;
            velocity.y *= -1;
            velocity *= damping;
        }
        transform.position = position;
    }
    private void FixedUpdate()
    {
        float weightScalar = mass * gravity;
        MyVector weight = new MyVector(0, weightScalar);
        MyVector friction = velocity.normalized * frictionCoefficient * -weightScalar * -1;
        acceleration *= 0f;
        ApplyForce(wind);
        ApplyForce(weight);
        ApplyForce(friction);
        if (FluidFriction && position.y <= 0f)
        {
            float velocityMagnitude = velocity.magnitude;
            float frontalArea = transform.localScale.x;
            MyVector fluidFriction = velocity.normalized * frontalArea * velocityMagnitude * velocityMagnitude * -0.5f;
            ApplyForce(fluidFriction);
            fluidFriction.Draw2(position, Color.red);
        }
        friction.Draw2(position, Color.green);
        Move();
    }
    private void Update()
    {
        velocity.Draw2(position, Color.blue);
    }
    void ApplyForce(MyVector force)
    {
        acceleration += force * (1f / mass);
    }
}