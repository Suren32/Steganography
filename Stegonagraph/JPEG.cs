using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Stegonagraph
{
    //обработка контейнера JPEG
    class JPEG
    {
        //сколко byte информации может хранит кантейнер
        public UInt64 info { set; get; }
        public List<byte> secretFiles { set; get; }

        //jpeg AC DC кайфиценти
        private int[] compCount = new int[3];
        private List<HuffTree[][]> DHT_DC = new List<HuffTree[][]>();
        private List<HuffTree[][]> DHT_AC = new List<HuffTree[][]>();

        //битовый поток JPEG файл
        private byte[] arrayJpeg;

        public JPEG(String path, Boolean tp)
        {
            if (tp)
                JpegCompressor(path);//конвертация JPEG Progressive в JPEG Baseline
            else
                arrayJpeg = File.ReadAllBytes(path);//получить jpeg в байтах

            UInt64 picturePos = 0;

            while (true)
            {

                //0xFFC0 инициализация начальников кайфицентов 
                if ((arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 192)
                   || (arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 193)
                   || (arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 194))
                {
                    int[] compH = new int[3];
                    int[] compV = new int[3];
                    picturePos += 11;

                    for (int i = 0; i < 3; i++)
                    {
                        compH[i] = arrayJpeg[picturePos]>>4;
                        compV[i] = arrayJpeg[picturePos]&15;
                        picturePos += 3;

                        compCount[i] = (int)(compH[i] * compV[i]);

                    }

                    picturePos -= 2;
                }

                //DHT нахождение кодов из дерева хафмана
                if (arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 196)
                {
                    picturePos += 4;
                    int kaefType = Convert.ToInt32(AddByte(Convert.ToString(arrayJpeg[picturePos], 2)).Substring(0, 4), 2);

                    HuffTree[][] shablon = new HuffTree[16][];
                    List<int> kaefCode = new List<int>();
                    kaefCode.Add(0);
                    kaefCode.Add(1);

                    for (int i = 0; i < 16; i++)
                    {
                        picturePos++;

                        shablon[i] = new HuffTree[arrayJpeg[picturePos]];
                        for (int j = 0; j < shablon[i].Length; j++)
                        {
                            shablon[i][j] = new HuffTree(kaefCode[0], 0);
                            kaefCode.RemoveRange(0, 1);
                        }

                        int ln = kaefCode.Count;
                        for (int j = 0; j < ln; j++)
                        {
                            kaefCode.Add(kaefCode[j]<<1);
                            kaefCode.Add((kaefCode[j] << 1)+1);
                        }
                        kaefCode.RemoveRange(0, ln);

                    }

                    for (int j = 0; j < 16; j++)
                        for (int i = 0; i < shablon[j].Length; i++)
                            shablon[j][i].Val = arrayJpeg[++picturePos];

                    //инициализация DC AC кайфицентов
                    if (kaefType == 0)
                        DHT_DC.Add(shablon);
                    else
                        DHT_AC.Add(shablon);

                }

                //конек JPEG файл
                if (arrayJpeg[picturePos] == 255 && (arrayJpeg[picturePos + 1] == 217 || arrayJpeg[picturePos + 1] == 218))
                    break;

                picturePos++;
            }


        }

        //сколько байт информации может хранить наш JPEG файл
        public void GetInfo(byte[] key)
        {
            jpegProcessing(key, 0);
        }
        public void jpegEncode(byte[] arrHide, String savePath, byte[] key)
        {
            jpegProcessing(key, 1, arrHide, savePath);
        }
        public void jpegDecode(byte[] key)
        {
            jpegProcessing(key, 2);
        }


        // type
        // 0 - Info
        // 1 - Encode
        // 2 - Decode
        private void jpegProcessing(byte[] key, byte typeProcess, byte[] arrHide = null, String savePath = null)
        {
            UInt64 picturePos = 0;
            UInt32 stegPos = 0;
            info = 0;

            //Encode
            int hidePos = 0;
            String hideChunk = "";

            List<byte> processedImg = new List<byte>();
            String processedImgChunk = "";

            //Decode
            secretFiles = new List<byte>();
            String secretFilesChunk = "";
            //

            while (true)
            {
                //начало чтения кодов
                if (arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 218)
                {
                    List<int> compDC = new List<int>();
                    List<int> compAC = new List<int>();

                    picturePos++;
                    ulong nom = picturePos;
                    int len = (arrayJpeg[++picturePos] << 8) +arrayJpeg[++picturePos];
                    int cmpCount = arrayJpeg[++picturePos];
                    picturePos++;

                    for (int i = 0; i < cmpCount; i++)
                    {
                        picturePos++;

                        compDC.Add(arrayJpeg[picturePos]>>4);
                        compAC.Add(arrayJpeg[picturePos]&15);

                        picturePos++;
                    }

                    picturePos = nom + (ulong)len +1;

                    if (typeProcess == 1)
                    {
                        for (UInt64 i = 0; i < picturePos; i++)
                            processedImg.Add(arrayJpeg[i]);
                    }

                    String processedChunk = "";
                    int cycleCount = 0;
                    int index = 0;

                    while (true)
                    {
                        if (cycleCount < compCount[index % compCount.Length])
                            cycleCount++;
                        else
                        { cycleCount = 1; index++; }


                        String hufCode = "";
                        int quantumTableCount = 0;
                        int processedChunkIndex = 0;

                        HuffTree[][] DC = DHT_DC[compDC[index % compCount.Length]];
                        HuffTree[][] AC = DHT_AC[compAC[index % compCount.Length]];

                        while (quantumTableCount < 64)
                        {
                            switch (typeProcess) {
                                case 1:
                                    while (processedImgChunk.Length > 8)
                                    {
                                        processedImg.Add(Convert.ToByte(processedImgChunk.Substring(0, 8), 2));

                                        if (Convert.ToByte(processedImgChunk.Substring(0, 8), 2) == 255)
                                            processedImg.Add(0);

                                        processedImgChunk = processedImgChunk.Substring(8);

                                    }
                                    break;
                                case 2:
                                    while (secretFilesChunk.Length > 8)
                                    {
                                        secretFiles.Add(Convert.ToByte(secretFilesChunk.Substring(0, 8), 2));
                                        secretFilesChunk = secretFilesChunk.Substring(8);
                                    }
                                    break;
                            }


                            while (processedChunk.Length < 32)
                            {
                                if (arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 217)
                                    break;

                                if (arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 0)
                                {
                                    processedChunk += AddByte(Convert.ToString(arrayJpeg[picturePos], 2));
                                    picturePos++;
                                }
                                else
                                {
                                    processedChunk += AddByte(Convert.ToString(arrayJpeg[picturePos], 2));
                                }

                                picturePos++;
                            }

                            hufCode += processedChunk[processedChunkIndex++];

                            if (quantumTableCount == 0)
                            {
                                for (int j = 0; j < DC[hufCode.Length - 1].Length; j++)
                                {
                                    if (DC[hufCode.Length - 1][j].Code == Convert.ToInt32(hufCode,2))
                                    {
                                        if (typeProcess == 1)
                                        {
                                            processedImgChunk += processedChunk.Substring(0, hufCode.Length);
                                        }

                                        processedChunk = processedChunk.Substring(hufCode.Length);

                                        if (DC[hufCode.Length - 1][j].Val != 0)
                                        {
                                            int chap = DC[hufCode.Length - 1][j].Val&15;
                                            int q = DC[hufCode.Length - 1][j].Val >> 4;
                                            for (int k = 0; k < q; k++)
                                                quantumTableCount++;

                                            if (typeProcess == 1)
                                            {
                                                processedImgChunk += processedChunk.Substring(0, chap);
                                            }

                                            processedChunk = processedChunk.Substring(chap);
                                        }

                                        processedChunkIndex = 0;
                                        hufCode = "";
                                        quantumTableCount++;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < AC[hufCode.Length - 1].Length; j++)
                                {
                                    if (AC[hufCode.Length - 1][j].Code == Convert.ToInt32(hufCode, 2))
                                    {
                                        if (typeProcess == 1)
                                        {
                                            processedImgChunk += processedChunk.Substring(0, hufCode.Length);
                                        }

                                        processedChunk = processedChunk.Substring(hufCode.Length);

                                        if (AC[hufCode.Length - 1][j].Val == 0)
                                        {
                                            goto found;
                                        }
                                        else
                                        {
                                            int chap = AC[hufCode.Length - 1][j].Val & 15;
                                            int q = AC[hufCode.Length - 1][j].Val >> 4;

                                            for (int k = 0; k < q; k++)
                                                quantumTableCount++;

                                            switch (typeProcess)
                                            {
                                                case 0:
                                                    if (chap > key[stegPos % key.Length])
                                                        info += (ulong)key[stegPos % key.Length];
                                                    else
                                                        info += (ulong)chap;
                                                    break;
                                                case 1:
                                                    if (!(hidePos == arrHide.Length && hideChunk == ""))
                                                    {
                                                        if (chap > key[stegPos % key.Length])
                                                        {
                                                            hideChunk = WriteCode(ref hidePos, hideChunk, arrHide, key[stegPos % key.Length]);

                                                            processedImgChunk += processedChunk.Substring(0, chap - key[stegPos % key.Length]) + hideChunk.Substring(0, key[stegPos % key.Length]);
                                                            hideChunk = hideChunk.Substring(key[stegPos % key.Length]);
                                                            info += (ulong)key[stegPos % key.Length];
                                                        }
                                                        else
                                                        {
                                                            hideChunk = WriteCode(ref hidePos, hideChunk, arrHide, key[stegPos % key.Length]);

                                                            processedImgChunk += hideChunk.Substring(0, chap);
                                                            hideChunk = hideChunk.Substring(chap);
                                                            info += (ulong)chap;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        processedImgChunk += processedChunk.Substring(0, chap);
                                                    }
                                                    break;
                                                case 2:
                                                    if (chap > key[stegPos % key.Length])
                                                    {
                                                        secretFilesChunk += processedChunk.Substring(chap - key[stegPos % key.Length], key[stegPos % key.Length]);
                                                        info += (ulong)key[stegPos % key.Length];
                                                    }
                                                    else
                                                    {
                                                        secretFilesChunk += processedChunk.Substring(0, chap);
                                                        info += (ulong)chap;
                                                    }
                                                    break;
                                            }


                                            stegPos++;
                                            processedChunk = processedChunk.Substring(chap);
                                        }
                                        processedChunkIndex = 0;
                                        hufCode = "";
                                        quantumTableCount++;
                                        break;
                                    }
                                }
                            }


                        }

                    found:;

                        if ((arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 254) || (arrayJpeg[picturePos] == 255 && arrayJpeg[picturePos + 1] == 217))
                        {
                            switch (typeProcess)
                            {
                                case 0:
                                    info = info / 8;
                                    break;
                                case 1:
                                    processedImgChunk += processedChunk;

                                    while (processedImgChunk.Length >= 8)
                                    {
                                        processedImg.Add(Convert.ToByte(processedImgChunk.Substring(0, 8), 2));

                                        if (Convert.ToByte(processedImgChunk.Substring(0, 8), 2) == 255)
                                            processedImg.Add(0);

                                        processedImgChunk = processedImgChunk.Substring(8);
                                    }

                                    processedImg.Add(255);
                                    processedImg.Add(217);

                                    File.WriteAllBytes(savePath, processedImg.ToArray());
                                    break;
                                case 2:
                                    while (secretFilesChunk.Length >= 8)
                                    {
                                        secretFiles.Add(Convert.ToByte(secretFilesChunk.Substring(0, 8), 2));

                                        if (Convert.ToByte(secretFilesChunk.Substring(0, 8), 2) == 255)
                                            secretFiles.Add(0);

                                        secretFilesChunk = secretFilesChunk.Substring(8);

                                    }
                                    break;
                            }
                            return;

                        }

                    }

                }
                picturePos++;
            }
        }

        private String AddByte(String btStr)
        {
            while (btStr.Length < 8)
                btStr = "0" + btStr;
            return btStr;
        }
        private String AddHexByte(String btStr)
        {
            return btStr.Length < 2 ? "0" + btStr : btStr;
        }
        private void JpegCompressor(String path)
        {
            byte[] byteArray = File.ReadAllBytes(path);

            List<byte> listJpeg = byteArray.OfType<byte>().ToList();

            int count = 0;

            for (int i = 0; i < listJpeg.Count - 1; i++)
                if (listJpeg[i] == 255 && listJpeg[i + 1] == 219)
                    count++;

            int pos = 0;

            for (int i = 0; i < listJpeg.Count - 1; i++)
                if (listJpeg[i] == 255 && listJpeg[i + 1] == 219)
                {
                    pos = i;
                    while (listJpeg[i] == 255 && listJpeg[i + 1] == 219)
                    {
                        i += 2;
                        int len = Convert.ToInt32(AddByte(Convert.ToString(listJpeg[i], 2)) + AddByte(Convert.ToString(listJpeg[i + 1], 2)), 2);
                        i += len;
                    }

                }

            listJpeg.RemoveRange(0, pos);
            listJpeg.InsertRange(0, new List<byte>() { 255, 216, 255, 254, 0, 4, 58, 41 });

            File.WriteAllBytes(System.IO.Directory.GetCurrentDirectory() + "\\" + "shablon.jpg", listJpeg.ToArray());

            FileInfo fi = new FileInfo(path);

            using (Image source = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + "\\" + "shablon.jpg"))
            {
                ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(c => c.MimeType == "image/jpeg");

                EncoderParameters parameters = new EncoderParameters(3);
                parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                parameters.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ScanMethod, (int)EncoderValue.ScanMethodInterlaced);
                parameters.Param[2] = new EncoderParameter(System.Drawing.Imaging.Encoder.RenderMethod, (int)EncoderValue.RenderProgressive);

                source.Save(fi.Name, codec, parameters);
            }

            arrayJpeg = File.ReadAllBytes(fi.Name);

            File.Delete(System.IO.Directory.GetCurrentDirectory() + "\\" + fi.Name);
            File.Delete(System.IO.Directory.GetCurrentDirectory() + "\\" + "shablon.jpg");

        }

        private String WriteCode(ref int pos, String strhideChunk, byte[] arrInfo, int stegType)
        {

            if (strhideChunk.Length < stegType)
            {
                if (pos != arrInfo.Length)
                    strhideChunk += AddByte(Convert.ToString(arrInfo[pos++], 2));
                else
                {
                    while (strhideChunk.Length % stegType != 0)
                        strhideChunk += "0";
                }
            }

            return strhideChunk;
        }

    }
}
