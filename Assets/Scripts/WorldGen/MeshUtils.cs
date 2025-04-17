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

            // Primitives
            Vector3[] vertices = new Vector3[density * density];
            Vector2[] uvs = new Vector2[density * density];
            int[] triangles = new int[(density - 1) * (density - 1) * 6];
            int i = 0;

            //Generate the mesh
            for (int y = 0; y < density; y++)
            {
                for (int x = 0; x < density; x++)
                {
                    // Vert
                    vertices[i] = new Vector3(x, 0, y);
                    uvs[i] = new Vector3(1 / density * x, 0, 1 / density * y);
                    i++;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
        }
    }
}
