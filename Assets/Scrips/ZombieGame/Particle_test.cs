using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_test : MonoBehaviour
{
    public Transform player;
    public Transform pos;
    public const string path = "items";

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
        GetWeaponValues("Pistol");
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) { ps.Play(); }
        transform.rotation = player.rotation;
        //old chef
        //transform.position = player.position + Vector3.Scale(player.forward,new Vector3(0.5f,0f,0.5f)) + new Vector3(0f,0.5f,0f);

        //new chef
        transform.position = pos.position;
    }

    void GetWeaponValues(string weapon)
    {
        ItemContainer ic = ItemContainer.Load(path);

        foreach (Item item in ic.items)
        {
            if (item.name == weapon) WeaponType(item.cycle, item.minShots, item.maxShots, item.interval, item.angle,item.range); ;
        }
        
    }


    private void WeaponType(int cycles,short minShots,short maxShots,float interval,float angle,float range)
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
