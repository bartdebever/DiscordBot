using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DiscordBot.Image_generator
{
    public static class SmashPlayerCard
    {
        public static string Test()
        {
            Bitmap background = new Bitmap(@"C:\Users\Bort\Pictures\600px-Finaldestination.jpg");
            Bitmap foxStock = new Bitmap(@"C:\Users\Bort\Pictures\Fox.png");
            using (Graphics g = Graphics.FromImage(background))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawImage(foxStock, new RectangleF(30,30,320,450));
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                TextRenderer.DrawText(g, "Hello World", new Font("Arial", 20), new Point(),  Color.Red);
                g.DrawString("Fox.", new Font(new FontFamily("Arial"), 20, FontStyle.Bold), Brushes.Red, new Point(300,300));
                g.Flush();
            }
            Image card = background;
            card.Save(@"C:\Users\Bort\Pictures\newImage.jpg");
            return @"C:\Users\Bort\Pictures\newImage.jpg";
        }
    }
}
