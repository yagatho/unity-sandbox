# Procedural Generation 
## Contents for now
- `MeshUtils.cs` (Generating mesh primitives)
- `ProceduralGeneration.cs` (Generating the world)
- `World.cs` (main Monobehaviour)
- `GenerationSettings.cs` (Settings for the world generation)

## Description
The world generation begins in the `World` class, which is a `MonoBehaviour` that is attached to the `World` GameObject in the scene. The `World` class is responsible for managing the world generation process (and its whole lifespan) and generates the world using `ProceduralGeneration` class. 

The `ProceduralGeneration` class is responsible for generating the primitive chunks using the `MeshUtils` class, and later on it will be used for terraforming of the terrain. The `MeshUtils` class is responsible for generating the mesh primitives (plane for each chunk) that are used to create the world. The `GenerationSettings` class is used to store the settings for the world generation process.
