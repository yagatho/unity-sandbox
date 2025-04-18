using UnityEngine;

namespace Project.World
{
    /// <summary>
    /// World class is responsible for generating the world.
    /// It is called at the start of the game.
    /// It uses the ProceduralGeneration class to generate the world.
    /// It is attached to the World GameObject.
    /// </summary>
    public class World : MonoBehaviour
    {
        //Variables
        public Material worldMaterial;

        public Chunk[] chunks;

        //Functions
        void Start()
        {
            chunks = ProceduralGeneration.GenerateWorld(GenerationSettings.chunksToGenerate, transform);

            // Get noise 
            FastNoiseLite noise = new FastNoiseLite();
            noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
            noise.SetFrequency(0.01f);
            noise.SetSeed(1341);

            noise.SetFractalType(FastNoiseLite.FractalType.FBm);
            noise.SetFractalOctaves(3);
            noise.SetFractalLacunarity(2.17f);
            noise.SetFractalGain(0.62f);
            noise.SetFractalWeightedStrength(0);

            noise.SetDomainWarpType(FastNoiseLite.DomainWarpType.BasicGrid);
            noise.SetDomainWarpAmp(2.5f);

            // Terraform the chunks
            ProceduralGeneration.TerraformChunks(chunks, noise);
        }


        // Get the instance of the world
        public static World GetInstance()
        {
            return FindFirstObjectByType<World>();
        }
    }

}

