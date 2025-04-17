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

        //Functions
        void Start()
        {
            ProceduralGeneration.GenerateWorld(GenerationSettings.chunksToGenerate, transform);
        }


        // Get the instance of the world
        public static World GetInstance()
        {
            return FindFirstObjectByType<World>();
        }
    }

}

