using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stegonagraph
{
    //обработка контейнера BMP таке PNG

    static class BMP
    {
        //кодирования
        static public void bmpEncode(List<Byte> hideInfo, String savePath, Bitmap myBitmap, byte[] key)
        {
            int bmpPos = 0;
            int stegPos = 0;
            String infoStr = "";

            for (int j = 0; j < hideInfo.Count; j++)
            {
                //канвертация байта в 2 мерную систему вычисления
                infoStr += HelpTools.AutoAddByte(Convert.ToString(hideInfo[j], 2), 8);

                while (infoStr.Length >= 3 * key[stegPos % key.Length])
                {
                    //кодировка с помощью LSB метода
                    infoStr = WriteToBitmap(bmpPos++, key[stegPos % key.Length], infoStr, myBitmap);
                    stegPos++;
                }
            }

    
            while (infoStr.Length % (3 * key[stegPos % key.Length]) != 0)
                infoStr += "0"; 

            while (infoStr.Length !=0)
            {
                //кодировка с помощью LSB метода
                infoStr = WriteToBitmap(bmpPos++, key[stegPos % key.Length], infoStr, myBitmap);
                stegPos++;
            }

            if (infoStr.Length!=0)
                MessageBox.Show("Ahavor zzveli bug!");

            //сахранение BMP картинки с секретной информацией
            myBitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);

            return;

        }

        //декодирование
        static public List<Byte> bmpDecode(Bitmap myBitmap, int len, byte[] Key)
        {
            List<byte> findInfo = new List<byte>();

            int bmpPos = 0;
            String strInfo = "";
            int stegPos = 0;

            while (findInfo.Count != len)
            {

                while (strInfo.Length < 8)
                {
                    //декодирование с помощью LSB метода
                    strInfo += ReadFromBitmap(bmpPos++, Key[stegPos % Key.Length], myBitmap);
                    stegPos++;
                }

                while (strInfo.Length >= 8)
                {
                    findInfo.Add(Convert.ToByte(strInfo.Substring(0, 8), 2));
                    strInfo = strInfo.Substring(8);
                }
            }

            return findInfo;
        }

        static private String ReadFromBitmap(int pos, int arrColor, Bitmap myBitmap)
        {
            String retStr = "";

            int posY = pos / myBitmap.Width,
                posX = pos - posY * myBitmap.Width;

            //RGB цветная палитра конкретного пиксела
            Color pixel = myBitmap.GetPixel(posX, posY);

            int[] pixelColor = new int[3];
            pixelColor[0] = pixel.R;
            pixelColor[1] = pixel.G;
            pixelColor[2] = pixel.B;

            for (int j = 0; j < 3; j++)
            {
                //декодирование секретного файла из последних битов (метод LSB)
                String str = HelpTools.AutoAddByte(Convert.ToString(pixelColor[j], 2), 8);
                str = str.Substring(8 - arrColor, arrColor);

                retStr += str;
            }


            return retStr;
        }

        static private String WriteToBitmap(int pos, int arrColor, String hideStr, Bitmap myBitmap)
        {
            int posY = pos / myBitmap.Width,
                posX = pos - posY * myBitmap.Width;

            //RGB цветная палитра конкретного пиксела
            Color pixel = myBitmap.GetPixel(posX, posY);

            int[] pixelColor = new int[3];
            pixelColor[0] = pixel.R;
            pixelColor[1] = pixel.G;
            pixelColor[2] = pixel.B;

            for (int j = 0; j < 3; j++)
            {
                //замена последних битов на биты секретного файла(метод LSB)
                String str = HelpTools.AutoAddByte(Convert.ToString(pixelColor[j], 2), 8);
                str = str.Substring(0, 8 - arrColor);
                str += hideStr.Substring(0, arrColor);
                pixelColor[j] = Convert.ToInt32(str, 2);
                hideStr = hideStr.Substring(arrColor);
            }

            //изменить цвет пикселя на кодированную
            myBitmap.SetPixel(posX, posY, Color.FromArgb(pixelColor[0], pixelColor[1], pixelColor[2]));
            return hideStr;
        }

    }
}
