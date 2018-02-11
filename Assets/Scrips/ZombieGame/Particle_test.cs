using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_test : MonoBehaviour
{
    public Transform player;
    public Transform pos;
    public Vector3 offset;
    public const string path = "items";

    private ParticleSystem ps;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    public float speed = 100;

    //This is method in FixedUpdate NOT made by us. It comes from http://wiki.unity3d.com/index.php?title=LookAtMouse
    void FixedUpdate()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) { ps.Play(); }
        //transform.rotation = player.rotation;


        //old chef
        transform.position = player.position + Vector3.Scale(player.forward*offset.x - player.right * offset.z, new Vector3(0.5f, 0f,0.5f)) + new Vector3(0f,0.5f,0f);

        //new chef
        //transform.position = pos.position;
        //transform.rotation *= Quaternion.Euler(0f, -20f, 0f);
    }



    public void WeaponType(int cycles,short minShots,short maxShots,float interval,float angle,float range)
    {
        var shape = ps.shape;
        var main = ps.main;
        ps.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, minShots, maxShots, cycles ,interval)});
        shape.angle = angle;
        main.startLifetime = range;
    }

    private void OnParticleCollision(GameObject other)
    {
        // initialize an array the size of our current particle count
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        // *pass* this array to GetParticles...
        int num = ps.GetParticles(particles);
        //Debug.Log("Found " + num + " active particles.");
        for (int i = 0; i < num; i++)
        {
            if(!other.CompareTag("Player"))  // negative x: make it die
                particles[i].remainingLifetime = 0;
        }
        // re-assign modified array
        ps.SetParticles(particles, num);
    }
}
