using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    static ObjectPool[] particlesPool;
    string[] arrStrParticleName;

	// Use this for initialization
	void Start () {
        arrStrParticleName = new string[(int)eParticleType.END - 1];
        arrStrParticleName[0] = "BangParticle";
        arrStrParticleName[1] = "CrashParticle";

        particlesPool = new ObjectPool[(int)eParticleType.END - 1];
        for (int i = 0; i < particlesPool.Length; i++)
        {
            GameObject particle = Resources.Load("Prefabs/Particles/" + arrStrParticleName[i]) as GameObject;
            Debug.Log(particle);
            particlesPool[i] = ObjectPool.CreateFor(particle);
        }
	}

    public static void SpawnParticle(eParticleType type, Vector2 pos)
    {
        int n = (int)type - 1;
        particlesPool[n].Retain(pos);
    }
}
