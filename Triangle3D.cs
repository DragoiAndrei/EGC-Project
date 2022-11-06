using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace EGC_Project
{
    class Triangle3D
    {
        private Vector3 PointA;
        private Vector3 PointB;
        private Vector3 PointC;
        private Color color;
        private bool visibility;
        private float linewidth;
        private float pointsize;
        private PolygonMode polMode;
        public Triangle3D(Vector3 a, Vector3 b, Vector3 c)
        {
            PointA = a;
            PointB = b;
            PointC = c;

            color = Color.FromArgb(0, 0, 0);

            Inits();
        }
        private void Inits()
        {
            visibility = true;
            linewidth = 3.0f;
            pointsize = 3.0f;
            polMode = PolygonMode.Fill;
        }
        public void Draw()
        {
            if (visibility)
            {
                GL.PointSize(pointsize);
                GL.LineWidth(linewidth);
                GL.PolygonMode(MaterialFace.FrontAndBack, polMode);
                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(color);
                GL.Vertex3(PointA);

                GL.Vertex3(PointB);
                GL.Vertex3(PointC);
                GL.End();
            }
        }
        public void DiscoMode(Randomizer _r)
        {
            color = _r.RandomColor();
        }
        public Color DiscoModeR(Randomizer _r)
        {
            color = _r.RandomColor();
            return color;
        }

        public void MorphPoz1()
        {
            int select = 0;
            if (select == 0)
            {
                PointB.Z += 2;
                PointC.Z += 2;
                select = 1;
            }
            else
            {
                if (select == 1)
                {
                    PointB.Z -= 2;
                    PointC.Z -= 2;
                }
            }
        }
        public void MorphPoz2()
        {
            int select = 0;
            if (select == 0)
            {
                PointB.Z -= 2;
                PointC.Z -= 2;
                select = 1;
            }
            else
            {
                if (select == 1)
                {
                    PointB.Z += 2;
                    PointC.Z += 2;
                }
            }
        }

        public void MorphFront()
        {

            Vector3 initB = PointB;
            Vector3 tempB = PointB;
            Vector3 initC = PointB;
            float zB = 0;
            zB=PointB.Z+5;
            tempB.Z = zB;
            PointB = tempB;



            
        }
        public void MorphBack()
        {

            Vector3 initB = PointB;
            Vector3 tempB = PointB;
            Vector3 initC = PointB;
            float zB = 0;
            zB = PointB.Z - 5;
            tempB.Z = zB;
            PointB = tempB;




        }
    }
}
