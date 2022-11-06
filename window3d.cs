using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
namespace EGC_Project
{
    class window3d : GameWindow
    {
        private Color DEFAULT_BACK_COLOR = Color.FromArgb(96, 96, 96);
        private Triangle3D firstTriangle;
        private Triangle3D secondTriangle;
        private Triangle3D thirdTriangle;
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
            citireFisier();
            
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
            thirdTriangle.Draw();
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
                Color ret =thirdTriangle.DiscoModeR(rando);
                Console.WriteLine(Convert.ToString(ret));
                
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
        public void citireFisier()
        {
            
            string numeFisier = "fisier.txt";
            string locatiefisier = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string cale_completa_fisier = locatiefisier +"\\"+ "EGC-Project"+"\\" + numeFisier;
            /* using (System.IO.StreamReader file = new System.IO.StreamReader(cale_completa_fisier, true))
             {
                 string liniefisier;
                 for (int i = 1; i <= 3; i++)
                 {
                     while ((liniefisier = file.ReadLine()) != null)
                     {

                         var data = liniefisier.Split(' ');
                         float x = Convert.ToInt32(data[0]);
                         float y = Convert.ToInt32(data[1]);
                         float z = Convert.ToInt32(data[2]);
                         Console.WriteLine(Convert.ToString(x), Convert.ToString(y), Convert.ToString(z), "\n");
                     }
                 }

             }*/
            string [] lines = File.ReadAllLines(cale_completa_fisier);
            List<float> numbers = new List<float>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (!string.IsNullOrEmpty(line))
                {
                    string[] stringNumbers = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < stringNumbers.Length; j++)
                    {
                        if (!float.TryParse(stringNumbers[j], out float num))
                        {
                            throw new OperationCanceledException(stringNumbers[j] + " was not a number.");
                        }
                        numbers.Add(num);
                        
                    }
                    
                }
            }
            float[] array = numbers.ToArray();
            
                thirdTriangle = new Triangle3D(new Vector3(array[0], array[1], array[2]), new Vector3(array[3], array[4], array[5]), new Vector3(array[6], array[7], array[8]));

            


        }
    }
    
}
