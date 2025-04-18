using UnityEngine;

namespace Project.World
{
    public static class MeshUtils
    {

        // Makes a plane mesh
        public static void MakePlane(Mesh mesh)
        {
            mesh.Clear();
            int density = GenerationSettings.chunkDensity;
            float size = GenerationSettings.chunkSize;

            // Primitives
            Vector3[] vertices = new Vector3[density * density];
            Vector2[] uvs = new Vector2[density * density];
            int[] triangles = new int[(density - 1) * (density - 1) * 6];
            int i = 0;

            //Generate the mesh verts and uvs
            for (int y = 0; y < density; y++)
            {
                for (int x = 0; x < density; x++)
                {
                    // Scale the vertices to the chunk size
                    vertices[i] = new Vector3(x, Mathf.PerlinNoise(x, y), y) * (size / (density - 1));
                    uvs[i] = new Vector2(x, y) / density;
                    i++;
                }
            }

            //Generate the triangles
            i = 0;
            for (int y = 0; y < density - 1; y++)
            {
                for (int x = 0; x < density - 1; x++)
                {
                    int start = y * density + x;
                    int nextRow = (y + 1) * density;

                    triangles[i++] = start;
                    triangles[i++] = nextRow + x;
                    triangles[i++] = start + 1;

                    triangles[i++] = nextRow + x;
                    triangles[i++] = nextRow + x + 1;
                    triangles[i++] = start + 1;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
        }
    }
}
