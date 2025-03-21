# Ray Tracing with OpenTK

![OpenTK](https://img.shields.io/badge/OpenTK-4.8-blue)
![.NET](https://img.shields.io/badge/.NET-6.0-purple)

A simple ray tracer with interactive light control. Demonstrates mirror, diffuse, and refractive materials in a 3D scene.

## Features
- Ray tracing with reflection and refraction support.
- 2 spheres with different materials (mirror and refractive).
- 12 triangles forming walls, floor, and ceiling.
- Interactive light movement (WASD, Space/Shift).
- Camera and material configurations via shaders.

## Dependencies
- [.NET 6.0](https://dotnet.microsoft.com/download)
- [OpenTK 4.8](https://opentk.net/)

## Controls

| Key            | Action                       |
|----------------|------------------------------|
| **WASD**       | Move light in XY-plane       |
| **Space**      | Raise light upward           |
| **Shift**      | Lower light downward         |
| **Esc**        | Exit                         |

## Project structure 
```
.
├── Program.cs          - Entry point and window setup
├── Window.cs           - Window logic and rendering
├── Shaders.cs          - Shader program class
├── shader.vert         - Vertex shader
├── shader.frag         - Fragment shader (core ray tracing logic)
└── README.md
```

