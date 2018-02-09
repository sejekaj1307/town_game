using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_test : MonoBehaviour
{
    public Transform player;
    public Transform pos;
    public const string path = "items";

    private ParticleSystem ps;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) { ps.Play(); }
        transform.rotation = player.rotation;
        
        //old chef
        transform.position = player.position + Vector3.Scale(player.forward,new Vector3(0.5f,0f,0.5f)) + new Vector3(0f,0.5f,0f);

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
