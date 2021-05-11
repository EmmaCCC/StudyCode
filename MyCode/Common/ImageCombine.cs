using System.Drawing;

namespace Common
{
    class ImageCombine
    {

        private const int LOGO_SIZE = 196;

        /// <summary>
        /// 图片处理为圆形
        /// </summary>
        /// <param name="img">原图片</param>
        /// <param name="size">圆形的直径大小</param>
        /// <returns></returns>
        public static Image CutEllipse(Image img, Size size)
        {
            Rectangle rec = new Rectangle(0, 0, img.Width, img.Height);

            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }

        /// <summary>
        /// 调用此函数后使此两种图片合并，类似相册，有个
        /// 背景图，中间贴自己的目标图片
        /// </summary>
        /// <param name="imgBack">粘贴的源图片</param>
        /// <param name="img">粘贴的目标图片</param>
        public static Image CombineImage(Image imgBack, Image img)
        {
            //设置纯色logo地图覆盖原来的logo
            Bitmap bitmap = MakePureBitmap(img.Width, img.Height);
            var blankEllipse = CutEllipse(bitmap,new Size(LOGO_SIZE, LOGO_SIZE));
            using (Graphics g = Graphics.FromImage(imgBack))
            {
                //画背景图
                g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
                //画空白logo
                g.DrawImage(blankEllipse, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
                //画新logo
                g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            }
            return imgBack;
        }

        /// <summary>
        /// 创建纯色位图
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static Bitmap MakePureBitmap(int x, int y)
        {
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics g = Graphics.FromImage(bmp)) { g.Clear(Color.White); }
            return bmp;
        }


        /// <summary>
        /// 合并二维码
        /// </summary>
        /// <param name="imgBack"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Image CombineQrCodeImage(Image imgBack, Image img)
        {
            var image2 = CutEllipse(img,new Size(LOGO_SIZE, LOGO_SIZE));
            return CombineImage(imgBack, image2);
        }
    }
}
