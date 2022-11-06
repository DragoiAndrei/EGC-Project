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
    class Patrat3D
    {
        private Vector3 PointA;
        private Vector3 PointB;
        private Vector3 PointC;
        private Vector3 PointD;
        private Color color;
        private bool visibility;
        private float linewidth;
        private float pointsize;
        private PolygonMode polMode;
        public Patrat3D(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            PointA = a;
            PointB = b;
            PointC = c;
            PointD = d;
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
                GL.Begin(PrimitiveType.Polygon);
                GL.Color3(color);
                GL.Vertex3(PointA);
                GL.Vertex3(PointB);
                GL.Vertex3(PointC);
                GL.Vertex3(PointD);
                GL.End();
            }
        }
        public void DiscoMode(Randomizer _r)
        {
            color = _r.RandomColor();
        }
    }
}
