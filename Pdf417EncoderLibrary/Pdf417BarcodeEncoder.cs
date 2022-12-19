/////////////////////////////////////////////////////////////////////
//
//	PDF417 Barcode Encoder
//
//	Pdf417Encoder class
//
//	Author: Uzi Granot
//	Version: 2.0
//	Date: May 7, 2019
//	Copyright (C) 2019 Uzi Granot. All Rights Reserved
//
//	PDF417 barcode encoder class and the attached test/demo
//  applications are free software.
//	Software developed by this author is licensed under CPOL 1.02.
//
//	The main points of CPOL 1.02 subject to the terms of the License are:
//
//	Source Code and Executable Files can be used in commercial applications;
//	Source Code and Executable Files can be redistributed; and
//	Source Code can be modified to create derivative works.
//	No claim of suitability, guarantee, or any warranty whatsoever is
//	provided. The software is provided "as-is".
//	The Article accompanying the Work may not be distributed or republished
//	without the Author's consent
//
//	Version History
//	---------------
//
//	Version 1.0 2019/04/01
//		Original version
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
//#if NET462
using System.Drawing;
using System.Drawing.Imaging;
//#endif

namespace Pdf417EncoderLibrary
{
    public class Pdf417BarcodeEncoder : Pdf417Encoder
    {
        private static readonly byte[] PngFileSignature = new byte[] { 137, (byte)'P', (byte)'N', (byte)'G', (byte)'\r', (byte)'\n', 26, (byte)'\n' };

        private static readonly byte[] PngIendChunk = new byte[] { 0, 0, 0, 0, (byte)'I', (byte)'E', (byte)'N', (byte)'D', 0xae, 0x42, 0x60, 0x82 };

        /// <summary>
        /// Save barcode image to PNG file
        /// </summary>
        /// <param name="FileName">PNG file name</param>
        public void SaveBarcodeToPngFile
                (
                string FileName
                )
        {
            // exceptions
            if (FileName == null)
                throw new ArgumentNullException("SaveBarcodeToPngFile: FileName is null");
            if (!FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("SaveBarcodeToPngFile: FileName extension must be .png");

            // file name to stream
            using (Stream OutputStream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // save file
                SaveBarcodeToPngFile(OutputStream);
            }
            return;
        }

        /// <summary>
        /// Save barcode image to PNG stream
        /// </summary>
        /// <param name="OutputStream">PNG output stream</param>
        public void SaveBarcodeToPngFile
                (
                Stream OutputStream
                )
        {
            // header
            byte[] Header = BuildPngHeader(ImageWidth, ImageHeight);

            // barcode data
            byte[] InputBuf = BarcodeMatrixToPng();

            // compress barcode data
            byte[] OutputBuf = PngImageData(InputBuf);

            // stream to binary writer
            BinaryWriter BW = new BinaryWriter(OutputStream);

            // write signature
            BW.Write(PngFileSignature, 0, PngFileSignature.Length);

            // write header
            BW.Write(Header, 0, Header.Length);

            // write image data
            BW.Write(OutputBuf, 0, OutputBuf.Length);

            // write end of file
            BW.Write(PngIendChunk, 0, PngIendChunk.Length);

            // flush all buffers
            BW.Flush();
            return;
        }

//#if NET462
		/// <summary>
		/// Save barcode Bitmap to file
		/// </summary>
		/// <param name="FileName">File name</param>
		/// <param name="Format">Image file format (i.e. PNG, BMP, JPEG)</param>
		public void SaveBarcodeToFile
				(
				string FileName,
				ImageFormat Format
				)
			{
			// exceptions
			if(FileName == null)
				throw new ArgumentNullException("SaveBarcodeToFile: FileName is null");

			// create Bitmap image of barcode
			Bitmap BarcodeImage = CreateBarcodeBitmap();

			// save image to file
			using(FileStream FS = new FileStream(FileName, FileMode.Create))
				{
				BarcodeImage.Save(FS, Format);
				}
			return;
			}

		/// <summary>
		/// Save barcode Bitmap to stream
		/// </summary>
		/// <param name="OutputStream">Output stream</param>
		/// <param name="Format">Image file format (i.e. PNG, BMP, JPEG)</param>
		public void SaveBarcodeToBitmap
				(
				Stream OutputStream,
				ImageFormat Format
				)
			{
			// create Bitmap image of barcode
			Bitmap BarcodeImage = CreateBarcodeBitmap();

			// save image
			BarcodeImage.Save(OutputStream, Format);

			// flush stream
			OutputStream.Flush();
			return;
			}

		/// <summary>
		/// Create Bitmap image of the Pdf417 barcode
		/// </summary>
		/// <returns>Barcode Bitmap</returns>
		public Bitmap CreateBarcodeBitmap()
			{
			return CreateBarcodeBitmap(Brushes.White, Brushes.Black);
			}

		/// <summary>
		/// Create Pdf417 barcode Bitmap image from boolean black and white matrix
		/// </summary>
		/// <param name="WhiteBrush">Background color (White brush)</param>
		/// <param name="BlackBrush">Bar color (Black brush)</param>
		/// <returns>Pdf417 barcode image</returns>
		public Bitmap CreateBarcodeBitmap
				(
				Brush WhiteBrush,
				Brush BlackBrush
				)
			{
			// create barcode matrix
			if(Pdf417BarcodeMatrix == null) Pdf417BarcodeMatrix = CreateBarcodeMatrix();

			// Pdf417Matrix width and height
			int MatrixWidth = BarColumns;
			int MatrixHeight = DataRows;
 
			// create picture object and make it white
			Bitmap Image = new Bitmap(ImageWidth, ImageHeight);
			Graphics Graphics = Graphics.FromImage(Image);
			Graphics.FillRectangle(WhiteBrush, 0, 0, ImageWidth, ImageHeight);

			// x and y image pointers
			int XOffset = QuietZone;
			int YOffset = QuietZone;

			// convert result matrix to output matrix
			for(int Row = 0; Row < MatrixHeight; Row++)
				{
				for(int Col = 0; Col < MatrixWidth; Col++)
					{
					// bar is black
					if(Pdf417BarcodeMatrix[Row, Col]) Graphics.FillRectangle(BlackBrush, XOffset, YOffset, NarrowBarWidth, RowHeight);
					XOffset += NarrowBarWidth;
					}
				XOffset = QuietZone;
				YOffset += RowHeight;
				}

			// return image
			return Image;
			}
//#endif
        private static byte[] BuildPngHeader
                (
                int Width,
                int Height
                )
        {
            // header
            byte[] Header = new byte[25];

            // header length
            Header[0] = 0;
            Header[1] = 0;
            Header[2] = 0;
            Header[3] = 13;

            // header label
            Header[4] = (byte)'I';
            Header[5] = (byte)'H';
            Header[6] = (byte)'D';
            Header[7] = (byte)'R';

            // image width
            Header[8] = (byte)(Width >> 24);
            Header[9] = (byte)(Width >> 16);
            Header[10] = (byte)(Width >> 8);
            Header[11] = (byte)Width;

            // image height
            Header[12] = (byte)(Height >> 24);
            Header[13] = (byte)(Height >> 16);
            Header[14] = (byte)(Height >> 8);
            Header[15] = (byte)Height;

            // bit depth (1)
            Header[16] = 1;

            // color type (grey)
            Header[17] = 0;

            // Compression (deflate)
            Header[18] = 0;

            // filtering (up)
            Header[19] = 0; // 2;

            // interlace (none)
            Header[20] = 0;

            // crc
            uint Crc = CRC32.Checksum(Header, 4, 17);
            Header[21] = (byte)(Crc >> 24);
            Header[22] = (byte)(Crc >> 16);
            Header[23] = (byte)(Crc >> 8);
            Header[24] = (byte)Crc;

            // return header
            return Header;
        }

        internal static byte[] PngImageData
                (
                byte[] InputBuf
                )
        {
            // output buffer is:
            // Png IDAT length 4 bytes
            // Png chunk type IDAT 4 bytes
            // Png chunk data made of:
            //		header 2 bytes
            //		compressed data DataLen bytes
            //		adler32 input buffer checksum 4 bytes
            // Png CRC 4 bytes
            // Total output buffer length is 18 + DataLen

            // compress image
            byte[] OutputBuf = ZLibCompression.Compress(InputBuf);

            // png chunk data length
            int PngDataLen = OutputBuf.Length - 12;
            OutputBuf[0] = (byte)(PngDataLen >> 24);
            OutputBuf[1] = (byte)(PngDataLen >> 16);
            OutputBuf[2] = (byte)(PngDataLen >> 8);
            OutputBuf[3] = (byte)PngDataLen;

            // add IDAT
            OutputBuf[4] = (byte)'I';
            OutputBuf[5] = (byte)'D';
            OutputBuf[6] = (byte)'A';
            OutputBuf[7] = (byte)'T';

            // adler32 checksum
            uint ReadAdler32 = Adler32.Checksum(InputBuf, 0, InputBuf.Length);

            // ZLib checksum is Adler32 write it big endian order, high byte first
            int AdlerPtr = OutputBuf.Length - 8;
            OutputBuf[AdlerPtr++] = (byte)(ReadAdler32 >> 24);
            OutputBuf[AdlerPtr++] = (byte)(ReadAdler32 >> 16);
            OutputBuf[AdlerPtr++] = (byte)(ReadAdler32 >> 8);
            OutputBuf[AdlerPtr] = (byte)ReadAdler32;

            // crc
            uint Crc = CRC32.Checksum(OutputBuf, 4, OutputBuf.Length - 8);
            int CrcPtr = OutputBuf.Length - 4;
            OutputBuf[CrcPtr++] = (byte)(Crc >> 24);
            OutputBuf[CrcPtr++] = (byte)(Crc >> 16);
            OutputBuf[CrcPtr++] = (byte)(Crc >> 8);
            OutputBuf[CrcPtr++] = (byte)Crc;

            // successful exit
            return OutputBuf;
        }

        // convert barcode matrix to PNG image format
        private byte[] BarcodeMatrixToPng()
        {
            // create barcode matrix
            if (Pdf417BarcodeMatrix == null) Pdf417BarcodeMatrix = CreateBarcodeMatrix();

            // BWMatrix width and height
            int MatrixWidth = Pdf417BarcodeMatrix.GetUpperBound(1) + 1;
            int MatrixHeight = Pdf417BarcodeMatrix.GetUpperBound(0) + 1;

            // image width and height
            int ImageWidth = this.ImageWidth;
            int ImageHeight = this.ImageHeight;

            // width in bytes including filter leading byte
            int PngWidth = (ImageWidth + 7) / 8 + 1;

            // PNG image array
            // array is all zeros in other words it is black image
            int PngLength = PngWidth * ImageHeight;
            byte[] PngImage = new byte[PngLength];

            // first row is a quiet zone and it is all white (filter is 0 none)
            int PngPtr;
            for (PngPtr = 1; PngPtr < PngWidth; PngPtr++) PngImage[PngPtr] = 255;

            // additional quiet zone rows are the same as first line (filter is 2 up)
            int PngEnd = QuietZone * PngWidth;
            for (; PngPtr < PngEnd; PngPtr += PngWidth) PngImage[PngPtr] = 2;

            // convert result matrix to output matrix
            for (int MatrixRow = 0; MatrixRow < MatrixHeight; MatrixRow++)
            {
                // make next row all white (filter is 0 none)
                PngEnd = PngPtr + PngWidth;
                for (int PngCol = PngPtr + 1; PngCol < PngEnd; PngCol++) PngImage[PngCol] = 255;

                // add black to next row
                for (int MatrixCol = 0; MatrixCol < MatrixWidth; MatrixCol++)
                {
                    // bar is white
                    if (!Pdf417BarcodeMatrix[MatrixRow, MatrixCol]) continue;

                    int PixelCol = NarrowBarWidth * MatrixCol + QuietZone;
                    int PixelEnd = PixelCol + NarrowBarWidth;
                    for (; PixelCol < PixelEnd; PixelCol++)
                    {
                        PngImage[PngPtr + (1 + PixelCol / 8)] &= (byte)~(1 << (7 - (PixelCol & 7)));
                    }
                }

                // additional rows are the same as the one above (filter is 2 up)
                PngEnd = PngPtr + RowHeight * PngWidth;
                for (PngPtr += PngWidth; PngPtr < PngEnd; PngPtr += PngWidth) PngImage[PngPtr] = 2;
            }

            // bottom quiet zone and it is all white (filter is 0 none)
            PngEnd = PngPtr + PngWidth;
            for (PngPtr++; PngPtr < PngEnd; PngPtr++) PngImage[PngPtr] = 255;

            // additional quiet zone rows are the same as first line (filter is 2 up)
            for (; PngPtr < PngLength; PngPtr += PngWidth) PngImage[PngPtr] = 2;

            return PngImage;
        }
    }
}
