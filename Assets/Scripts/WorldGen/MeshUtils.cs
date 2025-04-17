using UnityEngine;

namespace Project.World
{
    public static class MeshUtils
    {
        public static void QuadBasedMesh(Mesh mesh)
        {
            mesh.Clear();
            int quads = GenerationSettings.quads;

            //Generate the mesh
            Vector3[] vertices = new Vector3[quads * quads * 4];
            Vector2[] uvs = new Vector2[quads * quads * 4];
            int vertexIndex = 0;
            int triangleIndex = 0;
            int uvIndex = 0;
            int quadIndex = 0;

            int[] triangles = new int[quads * quads * 6];

            for (int y = 0; y < quads; y++)
            {
                for (int x = 0; x < quads; x++)
                {
                    Vector3 baseVert = new Vector3(GenerationSettings.chunkSize / quads * x, 0, GenerationSettings.chunkSize / quads * y);

                    vertices[vertexIndex++] = new Vector3(0, 0, 0) + baseVert;
                    vertices[vertexIndex++] = new Vector3(0, 0, GenerationSettings.chunkSize / quads) + baseVert;
                    vertices[vertexIndex++] = new Vector3(GenerationSettings.chunkSize / quads, 0, 0) + baseVert;
                    vertices[vertexIndex++] = new Vector3(GenerationSettings.chunkSize / quads, 0, GenerationSettings.chunkSize / quads) + baseVert;


                    Vector2 baseUv = new Vector2((float)x / quads, (float)y / quads);

                    uvs[uvIndex++] = new Vector2(0, 0) + baseUv;
                    uvs[uvIndex++] = new Vector2(0, quads / quads) + baseUv;
                    uvs[uvIndex++] = new Vector2(quads / quads, 0) + baseUv;
                    uvs[uvIndex++] = new Vector2(quads / quads, quads / quads) + baseUv;

                    triangles[triangleIndex++] = 0 + 4 * quadIndex;
                    triangles[triangleIndex++] = 1 + 4 * quadIndex;
                    triangles[triangleIndex++] = 2 + 4 * quadIndex;
                    triangles[triangleIndex++] = 2 + 4 * quadIndex;
                    triangles[triangleIndex++] = 1 + 4 * quadIndex;
                    triangles[triangleIndex++] = 3 + 4 * quadIndex;

                    quadIndex++;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
        }

        public static void VertBasedMesh(Mesh mesh)
        {
            mesh.Clear();
            int quads = GenerationSettings.quads;

            //Generate the mesh
            Vector3[] vertices = new Vector3[quads * quads * 4];
            Vector2[] uvs = new Vector2[quads * quads * 4];
            int vertexIndex = 0;
            int triangleIndex = 0;
            int uvIndex = 0;
            int quadIndex = 0;

            int[] triangles = new int[quads * quads * 6];

            for (int y = 0; y < quads; y++)
            {
                for (int x = 0; x < quads; x++)
                {
                    Vector3 baseVert = new Vector3(GenerationSettings.chunkSize / quads * x, 0, GenerationSettings.chunkSize / quads * y);

                    vertices[vertexIndex++] = new Vector3(0, 0, 0) + baseVert;
                    vertices[vertexIndex++] = new Vector3(0, 0, GenerationSettings.chunkSize / quads) + baseVert;
                    vertices[vertexIndex++] = new Vector3(GenerationSettings.chunkSize / quads, 0, 0) + baseVert;
                    vertices[vertexIndex++] = new Vector3(GenerationSettings.chunkSize / quads, 0, GenerationSettings.chunkSize / quads) + baseVert;


                    Vector2 baseUv = new Vector2((float)x / quads, (float)y / quads);

                    uvs[uvIndex++] = new Vector2(0, 0) + baseUv;
                    uvs[uvIndex++] = new Vector2(0, quads / quads) + baseUv;
                    uvs[uvIndex++] = new Vector2(quads / quads, 0) + baseUv;
                    uvs[uvIndex++] = new Vector2(quads / quads, quads / quads) + baseUv;

                    triangles[triangleIndex++] = 0 + 4 * quadIndex;
                    triangles[triangleIndex++] = 1 + 4 * quadIndex;
                    triangles[triangleIndex++] = 2 + 4 * quadIndex;
                    triangles[triangleIndex++] = 2 + 4 * quadIndex;
                    triangles[triangleIndex++] = 1 + 4 * quadIndex;
                    triangles[triangleIndex++] = 3 + 4 * quadIndex;

                    quadIndex++;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
        }
    }
}
