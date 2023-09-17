using System;
using OpenTK;
using OpenTK.Graphics;
//using OpenTK.Graphics.ES10;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace progGrafica
{
    public class Game : GameWindow
    {
        private float angle = 0.0f;
        private float rotationSpeed= 25.0f;
        private Vector3 rotationAngles = Vector3.Zero;
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
           
        }

        public Game(int width, int height) : base(width, height)
        {
        }

        //private float[] vertices = {
       // -0.5f, -0.5f, 0.0f, // Bottom-left vertex
         //0.5f, -0.5f, 0.0f, // Bottom-right vertex
         //0.0f,  0.5f, 0.0f  // Top vertex
         //};

        int VertexBufferObject;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.Beige);

            //Code goes here
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Key.Left))
            {
                rotationAngles.Y -= rotationSpeed * (float)e.Time;
            }
            if (keyboardState.IsKeyDown(Key.Right))
            {
                rotationAngles.Y += rotationSpeed * (float)e.Time;
            }
            if (keyboardState.IsKeyDown(Key.Up))
            {
                rotationAngles.X -= rotationSpeed * (float)e.Time;
            }
            if (keyboardState.IsKeyDown(Key.Down))
            {
                rotationAngles.X += rotationSpeed * (float)e.Time;
            }
            rotationAngles.X = MathHelper.Clamp(rotationAngles.X, -90.0f, 90.0f);
            rotationAngles.Y = rotationAngles.Y % 360.0f;

            // Convertir ángulos negativos a su equivalente positivo
            if (rotationAngles.Y < 0)
            {
                rotationAngles.Y += 360.0f;
            }

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Rotate(rotationAngles.X, 1.0f, 0.0f, 0.0f); // Rotación alrededor del eje X
            GL.Rotate(rotationAngles.Y, 0.0f, 1.0f, 0.0f); // Rotación alrededor del eje Y
            GL.Rotate(rotationAngles.Z, 0.0f, 0.0f, 1.0f); // Rotación alrededor del eje Z


            // Dibuja la pared
            GL.Begin(PrimitiveType.Quads);
            GL.Color3((System.Drawing.Color)Color4.Gray);
            GL.Vertex3(-1.0f, -1.0f, -5.0f);
            GL.Vertex3(1.0f, -1.0f, -5.0f);
            GL.Vertex3(1.0f, 1.0f, -5.0f);
            GL.Vertex3(-1.0f, 1.0f, -5.0f);
            GL.End();

            // Dibuja la repisa
            GL.Begin(PrimitiveType.Quads);
            GL.Color3((System.Drawing.Color)Color4.Brown);
            GL.Vertex3(-1.0f, -0.5f, -4.0f);
            GL.Vertex3(1.0f, -0.5f, -4.0f);
            GL.Vertex3(1.0f, -0.4f, -4.0f);
            GL.Vertex3(-1.0f, -0.4f, -4.0f);
            GL.End();

            // Dibuja el auto (un simple rectángulo)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3((System.Drawing.Color)Color4.Red);
            GL.Vertex3(-0.2f, -0.8f, -3.0f);
            GL.Vertex3(0.2f, -0.8f, -3.0f);
            GL.Vertex3(0.2f, -0.6f, -3.0f);
            GL.Vertex3(-0.2f, -0.6f, -3.0f);
            GL.End();
            Context.SwapBuffers();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
            float aspectRatio = (float)Width / Height;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
    }
}
