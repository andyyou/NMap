using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WRPlugIn;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using NMap.Model;

namespace NMap.Helper
{
    public class DataHelper
    {
        public IList<IImageInfo> GetFlawImageFromDb(DataRow flaw)
        {
            IList<IImageInfo> imgList = null;

            using (SqlConnection cn = new SqlConnection(JobHelper.DbConnectString))
            {
                cn.Open();
                string queryString = @"Select lFlawId, dCD, dMD,
                                              dArea, dtTime, sName,
                                              lID, dLength, dWidth
                                       From dbo.Jobs T1,
                                            dbo.Flaw T2,
                                            dbo.FlawClass T3
                                       Where T1.klKey = T2.klJobKey AND
                                             T2.klJobKey = T3.fkJobKey AND
                                             T2.lFlawClassType = T3.lID AND
                                             T1.JobID = @JobID AND
                                             T1.klKey = @JobKey AND
                                             T2.dMD = @MD AND
                                             T2.dCD = @CD
                                       Order by lFlawId";

                SqlCommand cmd = new SqlCommand(queryString, cn);
                cmd.Parameters.AddWithValue("@JobKey", JobHelper.JobKey);
                cmd.Parameters.AddWithValue("@JobID", JobHelper.JobInfo.JobID);
                cmd.Parameters.AddWithValue("@MD", flaw["MD"]);
                cmd.Parameters.AddWithValue("@CD", flaw["CD"]);
                SqlDataReader sd = cmd.ExecuteReader();

                while (sd.Read())
                {
                    // Get flaw images
                    using (SqlConnection cn2 = new SqlConnection(JobHelper.DbConnectString))
                    {
                        cn2.Open();
                        string queryString2 = @"Select iImage, lStation 
                                    From dbo.Jobs T1,
                                         dbo.Flaw T2,
                                         dbo.Image T3
                                    Where T1.klKey = T2.klJobKey AND
                                          T2.pklFlawKey = T3.klFlawKey AND
                                          T1.JobID = @JobID AND
                                          T1.klKey = @JobKey AND
                                          T2.lFlawId = @FlawID";
                        SqlCommand cmd2 = new SqlCommand(queryString2, cn2);
                        cmd2.Parameters.AddWithValue("@JobKey", JobHelper.JobKey);
                        cmd2.Parameters.AddWithValue("@JobID", JobHelper.JobInfo.JobID);
                        cmd2.Parameters.AddWithValue("@FlawID", flaw["FlawID"]);
                        SqlDataReader sd2 = cmd2.ExecuteReader();

                        bool blnShowImg = false;
                        int intW = 0;
                        int intH = 0;
                        imgList = new List<IImageInfo>();
                        while (sd2.Read())
                        {
                            byte[] images = (Byte[])sd2["iImage"];
                            int station = (int)sd2["lStation"];

                            intW = images[0] + images[1] * 256;
                            intH = images[4] + images[5] * 256;

                            if (intW == 0 & intH == 0)
                            {
                                intW = 1;
                                intH = 1;
                                blnShowImg = false;
                            }
                            else
                            {
                                blnShowImg = true;
                            }
                            Bitmap bmpShowImg = new Bitmap(intW, intH);

                            if (blnShowImg)
                            {
                                bmpShowImg = ToGrayBitmap(images, intW, intH);
                            }

                            IImageInfo tmpImg = new ImageInfo(bmpShowImg, station);
                            imgList.Add(tmpImg);
                        }
                    }
                }
            }
            return imgList;
        }

        // Function of Image
        public static Bitmap ToGrayBitmap(byte[] rawValues, int width, int height)
        {
            // Declare bitmap variable and lock memory
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // Get image parameter
            int stride = bmpData.Stride;  // Width of scan line
            int offset = stride - width;  // Display width and the scan line width of the gap
            IntPtr iptr = bmpData.Scan0;  // Get bmpData start position in memory
            int scanBytes = stride * height;  // Size of the memory area

            // Convert the original display size of the byte array into an array of bytes actually stored in the memory
            int posScan = 0, posReal = 0;  // Declare two pointer, point to source and destination arrays
            byte[] pixelValues = new byte[scanBytes];  // Declare array size
            for (int x = 0; x < height; x++)
            {
                // Emulate line scanning
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }
                posScan += offset;  //Line scan finished
            }

            // Using Marshal.Copy function copy pixelValues to BitmapData
            Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);  // Unlock memory

            // Change 8 bit bitmap index table to Grayscale
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;
            return bmp;
        }
    }
}
