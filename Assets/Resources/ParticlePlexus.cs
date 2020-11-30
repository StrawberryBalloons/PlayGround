
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePlexus : MonoBehaviour
{
    public float maxDistance = 1.0f;
    public float minDistance = 0.1f;
    public int maxConnections = 5;
    public int maxLineRenderers = 100;

    new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    ParticleSystem.MainModule particleSystemMainModule;

    public LineRenderer lineRendererTemplate;
    List<LineRenderer> lineRenderers = new List<LineRenderer>();

    Transform _transform;
    Vector3 p3_pos;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMainModule = particleSystem.main;
        //removed transform
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int maxParticles = particleSystemMainModule.maxParticles;

        if (particles == null || particles.Length < maxParticles)
        {
            particles = new ParticleSystem.Particle[maxParticles];
        }
        int lrIndex = 0;
        int lineRendererCount = lineRenderers.Count;

        if (lineRendererCount > maxLineRenderers)
        {
            for (int i = maxLineRenderers; i < lineRendererCount; i++)
            {
                Destroy(lineRenderers[i].gameObject);
            }
            int removeCount = lineRendererCount - maxLineRenderers;
            lineRenderers.RemoveRange(maxLineRenderers, lineRendererCount - maxLineRenderers);
            lineRendererCount -= lineRendererCount - maxLineRenderers;
        }

        if (maxConnections > 0 && maxLineRenderers > 0)
        {



            particleSystem.GetParticles(particles);
            int particleCount = particleSystem.particleCount;

            float maxDistanceSqr = maxDistance * maxDistance;
            float minDistanceSqr = minDistance * minDistance; // i think this is how that works

            ParticleSystemSimulationSpace simulationSpace = particleSystemMainModule.simulationSpace;

            switch (simulationSpace)
            {
                case ParticleSystemSimulationSpace.Local:
                    {
                        _transform = transform;
                        break;
                    }
                case ParticleSystemSimulationSpace.Custom:
                    {
                        _transform = particleSystemMainModule.customSimulationSpace;
                        break;
                    }
                case ParticleSystemSimulationSpace.World:
                    {
                        _transform = transform;
                        break;
                    }
                default:
                    {
                        throw new System.NotSupportedException(
                        string.Format("unsupported simulation space '{0}'.",
                        System.Enum.GetName(typeof(ParticleSystemSimulationSpace),
                        particleSystemMainModule.simulationSpace))
                        );
                    }
            }



            for (int i = 0; i < particleCount; i++)
            {
                if (lrIndex == maxLineRenderers)
                {
                    break;
                }
                Vector3 p1_pos = particles[i].position;

                int connections = 0;
                for (int j = i; j < particleCount; j++)
                {
                    Vector3 p2_pos = particles[j].position;
                    float distanceSqr = Vector3.SqrMagnitude(p1_pos - p2_pos);

                    if (distanceSqr <= maxDistanceSqr)
                    {
                        LineRenderer lr;


                        if (lrIndex == lineRendererCount)
                        {
                            lr = Instantiate(lineRendererTemplate, _transform, false);
                            lineRenderers.Add(lr);

                            lineRendererCount++;
                        }
                        lr = lineRenderers[lrIndex];


                        lr.enabled = true;
                        lr.useWorldSpace = simulationSpace == ParticleSystemSimulationSpace.World ? true : false;

                        lr.SetPosition(0, p1_pos);
                        lr.SetPosition(1, p2_pos);

                        lrIndex++;
                        connections++; 
                    }
                    // if (distanceSqr <= minDistanceSqr)
                    // {
                    //     //move away from eachother
                    //     particles[i].position = Vector3.Lerp(p1_pos, getPos(p1_pos, p2_pos), Time.deltaTime / 2.0f);

                    // }
                    if (connections == maxConnections || lrIndex == maxLineRenderers)
                    {
                        break;
                    }
                }
            }

            for (int i = lrIndex; i < lineRendererCount; i++)
            {
                lineRenderers[i].enabled = false;
            }
            particleSystem.SetParticles(particles, particleCount);
        }
    }
    public Vector3 getPos(Vector3 p1_pos, Vector3 p2_pos)
    {
        p3_pos = new Vector3(getPosCalc(p1_pos.x, p2_pos.x), getPosCalc(p1_pos.y, p2_pos.y),
         getPosCalc(p1_pos.z, p2_pos.z));

        return p3_pos;
    }
    public float getPosCalc(float x, float y)
    {
        return x + (x - y);
    }
}
