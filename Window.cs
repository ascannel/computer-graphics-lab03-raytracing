using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace computer_graphics_lab03
{
    public class Window : GameWindow
    {
        Vector3 LightPos = new Vector3(3.0f, -1.0f, -4.0f);

        private Vector3 _front = -Vector3.UnitZ;

        private Vector3 _up = Vector3.UnitY;

        private Vector3 _right = Vector3.UnitX;

        private ShaderProgram _shaderProgram;

        private float[] _vertices = {
            -1f, -1f, 0f,
            -1f, 1f, 0f,
            1f, -1f, 0f,
            1f, 1f, 0f
        };
        private int _vertexArrayObject;
        private int _vertexBufferObject;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            //Сборка шейдерной программы (компиляция и линковка двух шейдеров)
            string vertexShaderText = File.ReadAllText("../../../shader.vert");
            string fragmentShaderText = File.ReadAllText("../../../shader.frag");

            _shaderProgram = new ShaderProgram(
                vertexShaderText, fragmentShaderText);

            //Создание Vertex Array Object и его привязка
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            //Создание объекта буфера вершин/нормалей, его привязка и заполнение
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (sizeof(float) * _vertices.Length),
                _vertices, BufferUsageHint.StaticDraw);

            //Указание OpenGL, где искать вершины в буфере вершин/нормалей
            var posLoc = _shaderProgram.GetAttribLocation("vPosition");
            GL.EnableVertexAttribArray(posLoc);
            GL.VertexAttribPointer(posLoc, 3, VertexAttribPointerType.Float, false, 0, 0);

            //Установка фона
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //Включение теста глубины во избежание наложений
            GL.Enable(EnableCap.DepthTest);

            _shaderProgram.SetVector3("uCamera.Position", new Vector3(0.0f, 0.0f, -7.0f));
            _shaderProgram.SetVector3("uCamera.View", new Vector3(0.0f, 0.0f, 1f));
            _shaderProgram.SetVector3("uCamera.Up", new Vector3(0.0f, 1f, 0.0f));
            _shaderProgram.SetVector3("uCamera.Side", new Vector3(1.0f, 0.0f, 0.0f));
            _shaderProgram.SetVector2("uCamera.Scale", new Vector2(1.4f));
            _shaderProgram.SetVector3("uLight.Position", LightPos);
        }

        protected override void OnUnload()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.UseProgram(0);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteBuffer(_vertexBufferObject);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            Title = $"CG Lab03 FPS: {1f / e.Time:0}";
            //Очистка буферов цвета и глубины
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //Привязка буфера вершин
            GL.BindVertexArray(_vertexArrayObject);

            _shaderProgram.Use();
            _shaderProgram.SetVector3("uLight.Position", LightPos);


            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float speed = 1.5f;

            if (input.IsKeyDown(Keys.W))
            {
                LightPos += _front * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                LightPos -= _front * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                LightPos -= _right * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                LightPos += _right * speed * (float)e.Time;
            }


            if (input.IsKeyDown(Keys.Space))
            {
                LightPos += _up * speed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                LightPos -= _up * speed * (float)e.Time; // Down
            }
        }

        //Обновление размеров области видимости при изменении размеров окна
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);

        }
    }
}