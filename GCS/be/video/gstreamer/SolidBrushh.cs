﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using SkiaSharp;

namespace System.Drawing
{
    public class SolidBrushh : Brushh
    {
        public SolidBrushh() : this(Color.Black)
        {
        }

        public SolidBrushh(Color color)
        {
            _color = color;

            try
            {
                nativeBrush = new SKPaint() { Color = color.ToSKColor() };
            }
            catch (Exception)
            {
                //Console.WriteLine(e);
            }
        }

        public SolidBrushh(uint color)
        {
            _color = Color.FromArgb((byte)(color >> 24), (byte)(color >> 16), (byte)(color >> 8), (byte)color);
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                try
                {
                    nativeBrush.Color = value.ToSKColor();
                }
                catch (Exception)
                {
                    //Console.WriteLine(e);
                }
            }
        }

        public void ScaleTransform(float rectangleWidth, float rectangleHeight, MatrixOrder append)
        {
        }

        public void TranslateTransform(float rectangleLeft, float rectangleTop, MatrixOrder append)
        {
        }
    }
}