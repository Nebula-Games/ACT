// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="Code39.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace ACT.Core.Helper.Barcodes
{
    /// <summary>
    /// Create Barcodes using Code39
    /// </summary>
    public class Code39
	{
        /// <summary>
        /// Generates the PNG.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="barSize">Size of the bar.</param>
        /// <param name="showCodeString">if set to <c>true</c> [show code string].</param>
        /// <param name="title">The title.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] GeneratePNG(string code, int barSize, bool showCodeString, string title)
        {

            Barcodes.Code39 c39 = new Barcodes.Code39();

            // Create stream....
            MemoryStream ms = new MemoryStream();         
            c39.FontSize = barSize;
            c39.ShowCodeString = showCodeString;
            if (title + "" != "")
                c39.Title = title;
            Bitmap objBitmap = c39.GenerateBarcode(code);
            objBitmap.Save(ms, ImageFormat.Png);
            var _TmpReturn = ms.GetBuffer();
            ms.Dispose();
            return _TmpReturn;
        }

        /// <summary>
        /// The item sep height
        /// </summary>
        private const int _itemSepHeight=3;

        /// <summary>
        /// The title size
        /// </summary>
        SizeF _titleSize =SizeF.Empty;
        /// <summary>
        /// The bar code size
        /// </summary>
        SizeF _barCodeSize =SizeF.Empty;
        /// <summary>
        /// The code string size
        /// </summary>
        SizeF _codeStringSize =SizeF.Empty;

        #region Barcode Title 

        /// <summary>
        /// The title string
        /// </summary>
        private string _titleString=null;
        /// <summary>
        /// The title font
        /// </summary>
        private Font _titleFont=null;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
		{
			get { return _titleString; }
			set { _titleString=value; }
		}

        /// <summary>
        /// Gets or sets the title font.
        /// </summary>
        /// <value>The title font.</value>
        public Font TitleFont
		{
			get { return _titleFont; }
			set { _titleFont=value; }
		}
        #endregion

        #region Barcode code string

        /// <summary>
        /// The show code string
        /// </summary>
        private bool _showCodeString=false;
        /// <summary>
        /// The code string font
        /// </summary>
        private Font _codeStringFont=null;

        /// <summary>
        /// Gets or sets a value indicating whether [show code string].
        /// </summary>
        /// <value><c>true</c> if [show code string]; otherwise, <c>false</c>.</value>
        public bool ShowCodeString
		{
			get { return _showCodeString; }
			set { _showCodeString=value; }
		}

        /// <summary>
        /// Gets or sets the code string font.
        /// </summary>
        /// <value>The code string font.</value>
        public Font CodeStringFont
		{
			get { return _codeStringFont; }
			set { _codeStringFont=value; }
		}
        #endregion

        #region Barcode Font

        /// <summary>
        /// The C39 font
        /// </summary>
        private Font _c39Font=null;
        /// <summary>
        /// The C39 font size
        /// </summary>
        private float _c39FontSize=12;
        /// <summary>
        /// The C39 font file name
        /// </summary>
        private string _c39FontFileName=AppDomain.CurrentDomain.BaseDirectory + "Fonts\\FREE3OF9.TTF";
        /// <summary>
        /// The C39 font family name
        /// </summary>
        private string _c39FontFamilyName="Free 3 of 9";

        /// <summary>
        /// Gets or sets the name of the font file.
        /// </summary>
        /// <value>The name of the font file.</value>
        public string FontFileName
		{
			get { return _c39FontFileName; }
			set { _c39FontFileName=value; }
		}

        /// <summary>
        /// Gets or sets the name of the font family.
        /// </summary>
        /// <value>The name of the font family.</value>
        public string FontFamilyName
		{
			get { return _c39FontFamilyName; }
			set { _c39FontFamilyName=value; }
		}

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public float FontSize
		{
			get { return _c39FontSize; }
			set { _c39FontSize=value; }
		}

        /// <summary>
        /// Gets the code39 font.
        /// </summary>
        /// <value>The code39 font.</value>
        private Font Code39Font
		{
			get
			{
				if (_c39Font==null)
				{
					// Load the barcode font			
					PrivateFontCollection pfc=new PrivateFontCollection();
					pfc.AddFontFile(_c39FontFileName);
					FontFamily family=new FontFamily(_c39FontFamilyName,pfc);			
					_c39Font=new Font(family,_c39FontSize);
				}
				return _c39Font;
			}
		}

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Code39"/> class.
        /// </summary>
        public Code39()
		{           
			_titleFont=new Font("Arial",10);
			_codeStringFont=new Font("Arial",10);
		}

        #region Barcode Generation

        /// <summary>
        /// Generates the barcode.
        /// </summary>
        /// <param name="barCode">The bar code.</param>
        /// <returns>Bitmap.</returns>
        public Bitmap GenerateBarcode(string barCode)
		{
			
			int bcodeWidth=0;
			int bcodeHeight=0;

			// Get the image container...
			Bitmap  bcodeBitmap =CreateImageContainer(barCode, ref bcodeWidth, ref bcodeHeight);
            System.Drawing.Graphics objGraphics = System.Drawing.Graphics.FromImage(bcodeBitmap);

			// Fill the background			
			objGraphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0,0,bcodeWidth,bcodeHeight));

			int vpos=0;

			// Draw the title string
			if (_titleString!=null)			
			{
				objGraphics.DrawString(_titleString, _titleFont, new SolidBrush(Color.Black),XCentered((int)_titleSize.Width,bcodeWidth),vpos);
				vpos+=(((int)_titleSize.Height)+_itemSepHeight);
			}
			// Draw the barcode
			objGraphics.DrawString(barCode, Code39Font, new SolidBrush(Color.Black),XCentered((int)_barCodeSize.Width,bcodeWidth),vpos);

			// Draw the barcode string
			if (_showCodeString)
			{
				vpos+=(((int)_barCodeSize.Height));
				objGraphics.DrawString(barCode, _codeStringFont, new SolidBrush(Color.Black),XCentered((int)_codeStringSize.Width,bcodeWidth),vpos);
			}

			// return the image...									
			return bcodeBitmap;			
		}

        /// <summary>
        /// Creates the image container.
        /// </summary>
        /// <param name="barCode">The bar code.</param>
        /// <param name="bcodeWidth">Width of the bcode.</param>
        /// <param name="bcodeHeight">Height of the bcode.</param>
        /// <returns>Bitmap.</returns>
        private Bitmap CreateImageContainer(string barCode, ref int bcodeWidth, ref int bcodeHeight)
		{

            System.Drawing.Graphics objGraphics;	

			// Create a temporary bitmap...
			Bitmap tmpBitmap = new Bitmap(1,1,PixelFormat.Format32bppArgb); 
			objGraphics = System.Drawing.Graphics.FromImage(tmpBitmap); 

			// calculate size of the barcode items...
			if (_titleString!=null)			
			{
				_titleSize=objGraphics.MeasureString(_titleString,_titleFont);				
				bcodeWidth=(int)_titleSize.Width;
				bcodeHeight=(int)_titleSize.Height+_itemSepHeight;
			}

			_barCodeSize=objGraphics.MeasureString(barCode,Code39Font);								
			bcodeWidth=Max(bcodeWidth,(int)_barCodeSize.Width);
			bcodeHeight+=(int)_barCodeSize.Height;
			
			if (_showCodeString)
			{
				_codeStringSize=objGraphics.MeasureString(barCode,_codeStringFont);
				bcodeWidth=Max(bcodeWidth,(int)_codeStringSize.Width);
				bcodeHeight+=(_itemSepHeight+(int)_codeStringSize.Height);
			}
			
			// dispose temporary objects...
			objGraphics.Dispose();
			tmpBitmap.Dispose();

			return (new Bitmap(bcodeWidth,bcodeHeight,PixelFormat.Format32bppArgb));
		}

        #endregion


        #region Auxiliary Methods

        /// <summary>
        /// Determines the maximum of the parameters.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>System.Int32.</returns>
        private int Max(int v1, int v2)
		{
			return (v1>v2 ? v1 : v2 );
		}

        /// <summary>
        /// xes the centered.
        /// </summary>
        /// <param name="localWidth">Width of the local.</param>
        /// <param name="globalWidth">Width of the global.</param>
        /// <returns>System.Int32.</returns>
        private int XCentered(int localWidth, int globalWidth)
		{
			return ((globalWidth-localWidth)/2);
		}

		#endregion

	}
}
