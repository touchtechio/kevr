using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDecalPool : MonoBehaviour {

    private int particleDecalDataIndex;
    public int maxDecals = 100;
    public float decalSizeMin = 0.5f;
    public float decalSizeMax = 1.5f;

    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem decalParticleSystem;

	// Use this for initialization
	void Start () {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particleData = new ParticleDecalData[maxDecals]; // collision data
        particles = new ParticleSystem.Particle[maxDecals]; // particles to be launched

        for(int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }

    }

    // lets function get called outside of here
    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        displayParticles();
    }
	
    // pass in particle data from the collision event
    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        if (particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }
        // record collision position, rotation, size and color
        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360); // rotation on the wall randomly
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f, 1f));
        particleDecalDataIndex++;
    }

    void displayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation; // 3D rotation
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;



        }
        // pass in all into the actual particle system
        decalParticleSystem.SetParticles(particles, particles.Length);
      
    }
}
