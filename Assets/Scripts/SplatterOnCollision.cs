using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterOnCollision : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public Gradient particleColorGradient;
    public particleDecalPool dropletDecalPool;

    List<ParticleCollisionEvent> collisionEvents;

	// Use this for initialization
	void Start () {
        collisionEvents = new List<ParticleCollisionEvent>();
	}

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {
            dropletDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            i++;
        }
    }
}
