using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegonagraph
{
    class WAV
    {
        public int NumberOfChannels { set; get; }
        public int BlockAlignBytes { set; get; }
        public int BitsPerSample { set; get; }
        public UInt64 StartPos { set; get; }
        public UInt64 AudioInfoCount { set; get; }
        private byte[] wavFile;

        public WAV(String path)
        {

            this.wavFile = File.ReadAllBytes(path);

            String binaryTxt = "";
            UInt64 cPos = 0;

            cPos += 22;

            for (var i = cPos; i < cPos + 2; i++)
            {
                String str = HelpTools.AutoAddByte(Convert.ToString(wavFile[i], 2), 8);
                binaryTxt = str + binaryTxt;
            }
            //chanel counts
            this.NumberOfChannels = Convert.ToInt32(binaryTxt, 2);
            binaryTxt = "";
            cPos += 10;

            for (var i = cPos; i < cPos + 2; i++)
            {
                String str = HelpTools.AutoAddByte(Convert.ToString(wavFile[i], 2), 8);
                binaryTxt = str + binaryTxt;
            }

            //freymi erkarutyun byterov
            this.BlockAlignBytes = Convert.ToInt32(binaryTxt, 2);
            binaryTxt = "";
            cPos += 2;

            for (var i = cPos; i < cPos + 2; i++)
            {
                String str = HelpTools.AutoAddByte(Convert.ToString(wavFile[i], 2), 8);
                binaryTxt = str + binaryTxt;
            }

            //xorutyun
            this.BitsPerSample = Convert.ToInt32(binaryTxt, 2);

            while (!(binaryTxt == "data" || cPos == (UInt64)wavFile.Length))
            {
                binaryTxt = ((char)wavFile[cPos]).ToString() + ((char)wavFile[cPos + 1]).ToString() + ((char)wavFile[cPos + 2]).ToString() + ((char)wavFile[cPos + 3]).ToString();
                cPos++;
            }

            binaryTxt = "";

            cPos += 3;
            for (var i = cPos; i < cPos + 4; i++)
            {
                String str = HelpTools.AutoAddByte(Convert.ToString(wavFile[i], 2), 8);
                binaryTxt = str + binaryTxt;
            }


            cPos += 4;
            this.AudioInfoCount = Convert.ToUInt64(binaryTxt, 2);
            this.StartPos = cPos;

        }

        public void WavEncode(List<Byte> hideInfo, String savePath, byte[] Key)
        {             
            UInt64 sPos = StartPos;
            UInt32 stegPos = 0;
            String infoStr = "";
            int smCount = NumberOfChannels == 1 ? 1 : 2;

            for (int j = 0; j < hideInfo.Count; j++)
            {

                infoStr += HelpTools.AutoAddByte(Convert.ToString(hideInfo[j], 2), 8);

                while (infoStr.Length > (smCount*Key[stegPos%Key.Length]))
                {
                    UInt64 bfPos = sPos;
                    String strSample = "";

                    for (int i = 0; i < BlockAlignBytes; i++)
                    {
                        String str = HelpTools.AutoAddByte(Convert.ToString(wavFile[sPos++], 2), 8);
                        strSample = str + strSample;
                    }

                    sPos = bfPos;

                    for (int i = 0; i < BlockAlignBytes * 8 / BitsPerSample - 1; i++)
                    {
                        strSample = strSample.Substring(BitsPerSample) + strSample.Substring(0, BitsPerSample);
                    }

                    for (int i = 0; i < smCount; i++)
                    {
                        String str = strSample.Substring(0, BitsPerSample);
                        str = str.Substring(0, str.Length - Key[stegPos % Key.Length]);
                        str += infoStr.Substring(0, Key[stegPos % Key.Length]);
                        infoStr = infoStr.Substring(Key[stegPos % Key.Length]);

                        for (int k = str.Length; k > 0; k-=8)
                        {
                            wavFile[sPos++] = Convert.ToByte(str.Substring(k-8, 8), 2);
                        }

                        strSample = strSample.Substring(BitsPerSample);
                    }

                    sPos = bfPos + (UInt64)BlockAlignBytes;
                    stegPos++;
                }
            }

            while (infoStr.Length % (smCount* Key[stegPos % Key.Length]) != 0)
                infoStr += "0";


            while (infoStr.Length != 0)
            {
                UInt64 bfPos = sPos;
                String strSample = "";

                for (int i = 0; i < BlockAlignBytes; i++)
                {
                    String str = HelpTools.AutoAddByte(Convert.ToString(wavFile[sPos++], 2), 8);
                    strSample = str + strSample;
                }

                sPos = bfPos;

                for (int i = 0; i < BlockAlignBytes * 8 / BitsPerSample - 1; i++)
                {
                    strSample = strSample.Substring(BitsPerSample) + strSample.Substring(0, BitsPerSample);
                }

                for (int i = 0; i < smCount; i++)
                {
                    String str = strSample.Substring(0, BitsPerSample);
                    str = str.Substring(0, str.Length - Key[stegPos % Key.Length]);
                    str += infoStr.Substring(0, Key[stegPos % Key.Length]);
                    infoStr = infoStr.Substring(Key[stegPos % Key.Length]);

                    for (int k = str.Length; k > 0; k -= 8)
                    {
                        wavFile[sPos++] = Convert.ToByte(str.Substring(k - 8, 8), 2);
                    }
                    strSample = strSample.Substring(BitsPerSample);
                }

                sPos = bfPos + (UInt64)BlockAlignBytes;
                stegPos++;
            }

            File.WriteAllBytes(savePath, this.wavFile);
        }

        public List<byte> WavDecode(int len, byte[] Key)
        {
            UInt64 sPos = StartPos;
            UInt32 stegPos = 0;
            List<byte> findInfo = new List<byte>();
            String strInfo = "";
            int smCount = NumberOfChannels == 1 ? 1 : 2;

            while (findInfo.Count != len)
            {
                UInt64 bfPos = sPos;
                String strSample = "";

                for (int i = 0; i < BlockAlignBytes; i++)
                {
                    String str = HelpTools.AutoAddByte(Convert.ToString(wavFile[sPos++], 2), 8);
                    strSample = str + strSample;
                }

                sPos = bfPos;

                for (int i = 0; i < BlockAlignBytes * 8 / BitsPerSample - 1; i++)
                    strSample = strSample.Substring(BitsPerSample) + strSample.Substring(0, BitsPerSample);
                

                for (int i = 0; i < smCount; i++)
                {
                    String str = strSample.Substring(0, BitsPerSample);
                    strInfo += str.Substring(str.Length - Key[stegPos % Key.Length], Key[stegPos % Key.Length]);
                    strSample = strSample.Substring(BitsPerSample);
                }

                sPos = bfPos + (UInt64)BlockAlignBytes;
                stegPos++;

                while (strInfo.Length >= 8)
                {
                    findInfo.Add(Convert.ToByte(strInfo.Substring(0, 8), 2));
                    strInfo = strInfo.Substring(8);
                }
            }

            return findInfo;
        }

    }
}
