﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.App_Start
{
    public class ComprimirImagen
    {
        public static Image VaryQualityLevel(Image image, long value)
        {
            using (Bitmap bmp1 = new Bitmap(image))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(GetImageFormat(image));
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, value);
                myEncoderParameters.Param[0] = myEncoderParameter;
                MemoryStream ms = new MemoryStream();
                bmp1.Save(ms, jpgEncoder, myEncoderParameters);
                return Image.FromStream(ms);
            }
        }

        public static Image VaryQualityLevel(Bitmap image, long value)
        {
            try
            {
                using (image)
                {
                    ImageCodecInfo jpgEncoder = GetEncoder(GetImageFormat(image));
                    System.Drawing.Imaging.Encoder myEncoder =
                        System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);

                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, value);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, jpgEncoder, myEncoderParameters);
                    return Image.FromStream(ms);
                }
            }
            catch (Exception EX)
            {

                throw EX; 
            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static ImageFormat GetImageFormat(Image img)
        {
            using (img)
            {
                return img.RawFormat;
            }
        }
        public static ImageFormat GetImageFormat(Bitmap img)
        {
            using (img)
            {
                return img.RawFormat;
            }
        }
    }
}