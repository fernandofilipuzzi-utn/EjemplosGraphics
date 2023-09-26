using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DibujarFuncMatematica
{
   
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DibujarEjes(g, pictureBox1.Width, pictureBox1.Height);
            DibujarFuncion(g, pictureBox1.Width, pictureBox1.Height);
        }

        private void DibujarEjes(Graphics g, int ancho, int alto)
        {
            #region pinto el fondo
            Brush brush = new SolidBrush(Color.White);
            g.FillRectangle(brush, new RectangleF(0, 0, ancho, alto));
            #endregion

            #region dibujar eje x e y 
            Pen pen = new Pen(Color.Gray);
            int posXEje = ancho / 2;
            int posYEje = alto / 2;

            g.DrawLine(pen, new Point(0, posYEje), new Point(ancho - 1, posYEje));
            g.DrawLine(pen, new Point(posXEje, 0), new Point(posXEje, alto));
            #endregion
        }

        private void DibujarFuncion(Graphics g, int ancho, int alto)
        {
            Pen pen = new Pen(Color.Blue);

            int posXEje = ancho / 2;
            int posYEje = alto / 2;
            int Amplitud = 5;

            //necesito el punto evaluado en el borde como posición inicial
            int x = 0;
            int xp0 = x - posXEje; 
            int yp0 = (int)(Amplitud * Math.Sin(2 * Math.PI / 3 * xp0));

            for (x = 2; x < ancho; x+=3) 
            {
                int xp1 = x - posXEje;

                //calculo el siguiente punto, 
                int yp1=(int)(5*Amplitud*Math.Sin(2*Math.PI/20*xp1));
                
                #region dibujo 
                g.DrawLine(pen, new Point(xp0+ posXEje, -yp0+ posYEje), new Point(xp1+ posXEje, -yp1 + posYEje));
                #endregion

                //guardo la posición anterior
                xp0 = xp1;
                yp0 = yp1;
            }
        }

        private void btnGuardarImagen_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "jpg|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g = Graphics.FromImage(bitmap);
                DibujarEjes(g, pictureBox1.Width, pictureBox1.Height);
                DibujarFuncion(g, pictureBox1.Width, pictureBox1.Height);
                bitmap.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                g.Dispose();
                bitmap.Dispose();
            }
        }
    }
}
