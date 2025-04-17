namespace Project.World
{
    public static class GenerationSettings
    {
        public const int chunksToGenerate = 16;

        //Chunk size is the size of the chunk in world units
        public const float chunkSize = 32;

        //Quad based
        //Chunk size in quads x by x
        public const int quads = 6;

        //Vert based
        public const int verts = 7;
    }
}
