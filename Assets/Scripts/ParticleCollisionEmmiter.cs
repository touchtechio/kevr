using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionEmmiter : MonoBehaviour
{

    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public particleDecalPool splatDecalPool;

    List<ParticleCollisionEvent> collisionEvents;


    // Use this for initialization
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        //particleLauncher = new ParticleSystem[particleLauncher.Length];
    }

    void OnParticleCollision(GameObject other)

    {


        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents); // getting the data from the particle system -> collision location
        for (int j = 0; j < collisionEvents.Count; j++)
        {
            // splatDecalPool.ParticleHit(collisionEvents[i], particleColorGradient); // to pass the data into the system we made

            EmitAtLocation(collisionEvents[j]);


        }
    }

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection; // position system at intersection of system and rotate
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal); // one of the variables of the collision events is the normal so need to calculate the rotation as well
        ParticleSystem.MainModule psMain = splatterParticles.main; // calling variables from the main module
        psMain.startColor = particleColorGradient.Evaluate(UnityEngine.Random.Range(0f, 1f));
        splatterParticles.Emit(50);
        //Debug.Log("particleshower");
    }
}
