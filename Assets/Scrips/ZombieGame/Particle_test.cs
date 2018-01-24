using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_test : MonoBehaviour
{
    public Transform player;
    //private 
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
        WeaponType(2,1,1,0.1f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) { ps.Play(); }
        transform.rotation = player.rotation;
        transform.position = player.position + Vector3.Scale(player.forward,new Vector3(0.5f,0f,0.5f)) + new Vector3(0f,0.5f,0f);
    }

    private void WeaponType(int cycles,short minShots,short maxShots,float interval)
    {
        ps.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, minShots, maxShots, cycles ,interval)});
    }

    private void OnParticleCollision(GameObject other)
    {
        // initialize an array the size of our current particle count
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        // *pass* this array to GetParticles...
        int num = ps.GetParticles(particles);
        Debug.Log("Found " + num + " active particles.");
        for (int i = 0; i < num; i++)
        {
            if(!other.CompareTag("Player"))  // negative x: make it die
                particles[i].remainingLifetime = 0;
        }
        // re-assign modified array
        ps.SetParticles(particles, num);
    }
}
