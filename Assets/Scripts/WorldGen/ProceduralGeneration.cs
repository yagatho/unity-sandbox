using UnityEngine;

namespace Project.World
{
    /// <summary>
    /// ProceduralGeneration class is holding all the functions for generating the world.
    /// </summary>
    public static class ProceduralGeneration
    {
        public static Chunk[] GenerateWorld(int chunksToGenerate, Transform parent)
        {
            // PERF: Thread it with unity job system 

            //Create an array of chunks
            int chunkIndex = 0;
            Chunk[] chunks = new Chunk[chunksToGenerate * chunksToGenerate];

            //Loop through the array and create a chunk for each index
            for (int y = 0; y < chunksToGenerate; y++)
            {
                for (int x = 0; x < chunksToGenerate; x++)
                {
                    chunks[chunkIndex++] = GenerateChunk(new Vector2Int(x, y), parent);
                }
            }

            return chunks;

        }

        public static Chunk GenerateChunk(Vector2Int pos, Transform parent)
        {
            //Create a new mesh
            Mesh mesh = new Mesh();
            mesh.name = "Chunk_" + pos.x + "_" + pos.y;

            //Set the parent of the chunk
            // PERF: Switch to using a single mesh for all chunks 
            GameObject chunkObject = new GameObject("Chunk_" + pos.x + "_" + pos.y);
            chunkObject.transform.SetParent(parent);
            chunkObject.transform.position = new Vector3(pos.x * GenerationSettings.chunkSize, 0, pos.y * GenerationSettings.chunkSize);

            //Create a new chunk
            Chunk chunk = new Chunk(pos, mesh);
            chunk.GenerateMesh();

            //Add a mesh filter and a mesh renderer to the chunk
            chunkObject.AddComponent<MeshFilter>().mesh = mesh;
            chunkObject.AddComponent<MeshRenderer>().material = World.GetInstance().worldMaterial;

            return chunk;
        }

        public static void TerraformChunks(Chunk[] chunks, FastNoiseLite noise)
        {
            //Loop through the chunks and terraform them
            for (int i = 0; i < chunks.Length; i++)
            {
                TerraformChunk(chunks[i], noise);
            }
        }

        public static void TerraformChunk(Chunk chunk, FastNoiseLite noise)
        {
            //Loop through the vertices and change their height
            Vector3[] vertices = chunk.myMesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                // Get the global position of the vertex
                Vector3 vertGlobalPos = vertices[i] + new Vector3(chunk.position.x, 0, chunk.position.y) * GenerationSettings.chunkSize;

                // Set the height of the vertex based on the noise
                float h = noise.GetNoise(vertGlobalPos.x, vertGlobalPos.z) * 5;
                vertices[i].y += Mathf.Pow(h, 4);
            }

            chunk.myMesh.vertices = vertices;
        }
    }

    [System.Serializable]
    public struct Chunk
    {
        public Vector2Int position;
        public Mesh myMesh;

        //Initializes the chunk with a position and a mesh
        public Chunk(Vector2Int _position, Mesh _myMesh)
        {
            this.position = _position;
            this.myMesh = _myMesh;
        }

        public void GenerateMesh()
        {
            if (myMesh == null)
            {
                Debug.LogError("Mesh is null");
                return;
            }

            //Generate the mesh
            MeshUtils.MakePlane(myMesh);

            myMesh.RecalculateNormals();
            myMesh.RecalculateBounds();
            myMesh.RecalculateTangents();
            myMesh.Optimize();
        }
    }

}
