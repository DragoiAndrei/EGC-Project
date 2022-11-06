using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace EGC_Project
{
    class window3d : GameWindow
    {
        private Color DEFAULT_BACK_COLOR = Color.FromArgb(96, 96, 96);
        private Triangle3D firstTriangle;
        private Triangle3D secondTriangle;
        private Patrat3D patrat;
        private Randomizer rando;
        private KeyboardState previousKeyboard;
        public window3d() : base(1000, 1000, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            rando = new Randomizer();
            firstTriangle = new Triangle3D(new Vector3(1, 5, 1), new Vector3(6, 1, 1), new Vector3(6, 10, 1));
            secondTriangle = new Triangle3D(new Vector3(-1, 5, 1), new Vector3(-6, 1, 1), new Vector3(-6, 10, 1));
            patrat = new Patrat3D(new Vector3(-1, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 11, 1), new Vector3(-1, 11, 1));
            DisplayHelp();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // setare fundal
            GL.ClearColor(DEFAULT_BACK_COLOR);

            // setare viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // setare proiectie/con vizualizare
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 600);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // setare ochi
            Matrix4 ochi = Matrix4.LookAt(15, 15, 30, 0, 0, 0, 0, 1, 0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref ochi);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // RENDER CODE
            DrawAxes();
            firstTriangle.Draw();
            secondTriangle.Draw();
            patrat.Draw();
            // END render code
            SwapBuffers();


        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // LOGIC CODE
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();
            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }
            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }
            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());
            }
            if (currentKeyboard[Key.X] && !previousKeyboard[Key.X])
            {
                firstTriangle.DiscoMode(rando);
                secondTriangle.DiscoMode(rando);
                patrat.DiscoMode(rando);
            }
            if (currentKeyboard[Key.W] && !previousKeyboard[Key.W])
            {
                firstTriangle.MorphPoz1();
                secondTriangle.MorphPoz1();
                
            }
            if (currentKeyboard[Key.S] && !previousKeyboard[Key.S])
            {
                firstTriangle.MorphPoz2();
                secondTriangle.MorphPoz2();

            }
            

        }
        private void DisplayHelp()
        {
            Console.WriteLine("\n      MENIU");
            Console.WriteLine(" H - meniul");
            Console.WriteLine(" ESC - parasire aplicatie");
            Console.WriteLine(" W - Aripa in fata");
            Console.WriteLine(" S - Aripa in spate");
            Console.WriteLine(" B - schimbare culoare de fundal");
            Console.WriteLine(" X - DISCO MODE (LOL)");
        }
        private void DrawAxes()
        {
            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(100, 0, 0);
            GL.End();

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 100, 0); ;
            GL.End();

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 100);
            GL.End();
        }
    }
    
}
