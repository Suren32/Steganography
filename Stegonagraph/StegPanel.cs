using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stegonagraph
{
    public partial class StegPanel : Form
    {
        List<Point> contCords = new List<Point>();
        Boolean tpHide;
        Process loadingPeocess;
        byte[] stegKey;

        public StegPanel(Boolean hideUnhide,byte[] stegKey)
        {
            InitializeComponent();
            containerGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.stegKey = new byte[stegKey.Length];

            for (int i = 0; i < stegKey.Length; i++)
                this.stegKey[i] = stegKey[i];
            

            dataGridView.Font = new Font("Sylfaen", 12);
            containerGridView.Font = new Font("Sylfaen", 12);

            if (!hideUnhide)
            {
                dataGridView.Enabled = false;
                SelectItemBtn.Enabled = false;
                btnRemove.Enabled = false;
            }

            tpHide = hideUnhide;
            contCords.Add(new Point(20, 20 - new TrackBar().Height));
            
        }

        private void SelectContainerBtn_Click(object sender, EventArgs e)
        {
            UInt64 info = 0;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                try {
                    loadingPeocess = Process.Start(System.IO.Directory.GetCurrentDirectory() + "//Resources//WaitForm.exe");
                }
                catch (Exception err) {}

                foreach (String file in openFileDialog1.FileNames)
                {

                    FileInfo fileInfo = new FileInfo(file);
                    Bitmap bp;

                    String Name = fileInfo.Name;
                    Name = Name.Substring(0, Name.LastIndexOf('.'));
                    Name = Name.Length > 255 ? Name.Substring(0, 255) + fileInfo.Extension : Name + fileInfo.Extension;

                    Boolean bl = false;

                    for (int i = 0; i < containerGridView.Rows.Count; i++)
                    {
                        if (Name == containerGridView.Rows[i].Cells[0].Value.ToString())
                        {
                            bl = true;
                            MessageBox.Show("File has already exist!");
                        }
                    }

                    if (bl)
                        continue;

                    switch (fileInfo.Extension.ToLower())
                    {
                        case ".bmp":
                        case ".png":
                            bp = new Bitmap(fileInfo.FullName);
                            pbPicture.Image = Image.FromFile("Image/true.png");

                            info = 0;
                            for (int i = 0; i < bp.Width * bp.Height; i++)
                                info += (ulong)(3 * stegKey[i % stegKey.Length]);
                            
                            containerGridView.Rows.Add(fileInfo.Name, info / 8, fileInfo.FullName);
                            break;
                        case ".wav":
                            WAV wavInfo = new WAV(fileInfo.FullName);
                            pbPicture.Image = Image.FromFile("Image/true.png");

                            info = 0;
                            for (UInt64 i = 0; i < wavInfo.AudioInfoCount/(ulong)wavInfo.BlockAlignBytes; i++)
                                info += (ulong)stegKey[i % (ulong)stegKey.Length];

                            if (wavInfo.NumberOfChannels == 1)
                                containerGridView.Rows.Add(fileInfo.Name, info / 8, fileInfo.FullName);
                            else
                                containerGridView.Rows.Add(fileInfo.Name, (info * 2) / 8, fileInfo.FullName);
                            break;
                        case ".jpg":
                        case ".jpeg":
                            JPEG jpeg = new JPEG(fileInfo.FullName, dataGridView.Enabled ? true : false);
                            jpeg.GetInfo(stegKey);
                            pbPicture.Image = Image.FromFile("Image/true.png");
                            containerGridView.Rows.Add(fileInfo.Name, jpeg.info, fileInfo.FullName);
                            break;
                        default:
                            pbPicture.Image = Image.FromFile("Image/false.png");
                            break;
                    }
                }

                try{loadingPeocess.Kill();}
                catch (Exception err) { }

                containerGridView.ClearSelection();
                containerGridView.CurrentCell = null;
            }
            labelContainer.Text = "Can Hide: " + GetPoints(GetSize(containerGridView)) + " byte";
        }
        private void SelectDataBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    byte[] btArray = File.ReadAllBytes(file);

                    String Name = fileInfo.Name;
                    Name = Name.Substring(0, Name.LastIndexOf('.'));
                    Name = Name.Length > 255 ? Name.Substring(0, 255) + fileInfo.Extension : Name + fileInfo.Extension;


                    Boolean bl = false;

                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (Name == dataGridView.Rows[i].Cells[0].Value.ToString())
                        {
                            bl = true;
                            MessageBox.Show("File has already exist!");
                        }
                    }

                    if (bl)
                        continue;

                    //5b 1b,anuni lenq*2,5b,5b
                    String name = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));

                    dataGridView.Rows.Add(fileInfo.Name, (dataGridView.Rows.Count == 0 ? 10 : 0) +  1 + (name.Length>255?510: name.Length*2) + 1 + fileInfo.Extension.Substring(1).Length + 5 + btArray.Length   , fileInfo.FullName);
                }
                dataGridView.ClearSelection();
                dataGridView.CurrentCell = null;
            }

            labelHide.Text = "File Size: " + GetPoints(GetSize(dataGridView)) + " byte";
        }

        //hide find functions
        private UInt64 GetSize(DataGridView dgv)
        {
            UInt64 byteCount = 0;

            for (int i = 0; i < dgv.Rows.Count; i++)
                byteCount += Convert.ToUInt64(dgv.Rows[i].Cells[1].Value.ToString());

            return byteCount;
        }
        private String GetPoints(UInt64 num)
        {
            String str = num.ToString();
            String endStr = "";

            while (str.Length > 3)
            {
                endStr = "." + str.Substring(str.Length - 3, 3) + endStr;
                str = str.Substring(0, str.Length - 3);
            }

            return str + endStr;
        }

        //start encoding or decoding
        private void btnStart_Click(object sender, EventArgs e)
        {
            List<Byte> hideInfo = new List<Byte>();

            if (containerGridView.Rows.Count == 0)
            {
                MessageBox.Show("Insert container!");
                return;
            }

            if (dataGridView.Rows.Count == 0 && dataGridView.Enabled)
            {
                MessageBox.Show("Insert hide information!");
                return;
            }

            if (checkBox.Checked && encryptTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insert password of encription!");
                return;
            }

            if(GetSize(containerGridView) < GetSize(dataGridView))
            {
                MessageBox.Show("Secret filse size more then container can hide!");
                return;
            }

            DialogResult result = folderBrowserDialog1.ShowDialog();
            String savePath = "";
            if (result != DialogResult.OK) {
                return;
            }

            savePath = folderBrowserDialog1.SelectedPath + "\\";
            try
            {
                loadingPeocess = Process.Start(System.IO.Directory.GetCurrentDirectory() + "//Resources//WaitForm.exe");
            }
            catch (Exception err) { }

            if (tpHide)
            {
                String[] pathArray = new String[dataGridView.Rows.Count];
                UInt64 hideFileCount = (UInt64)GetSize(dataGridView);

                //вся информация в байтах(5byte)
                for (int i = 0; i < 5; i++) {
                    hideInfo.Add((byte)(hideFileCount >> ((4 - i) * 8)));
                }

                hideFileCount = (UInt64)pathArray.Length;
                //каличество файлах(5byte)
                for (int i = 0; i < 5; i++)
                    hideInfo.Add((byte)(hideFileCount >> ((4 - i) * 8)));

                //размер текущего секретного файла
                for (int i = 0; i < pathArray.Length; i++)
                    pathArray[i] = dataGridView.Rows[i].Cells[2].Value.ToString();

                for (int i = 0; i < pathArray.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(pathArray[i]);

                    String fileName = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));

                    if (fileName.Length > 255)
                        fileName = fileName.Substring(0, 255);

                    String fileExt = fileInfo.Extension.Substring(1);

                    //размер файла(1byte) имя файла(255byte)
                    hideInfo.Add((byte)fileName.Length);

                    for (int j = 0; j < fileName.Length; j++)
                    {
                        Encoding.ASCII.GetBytes(fileName[j].ToString());
                        hideInfo.Add((Encoding.Unicode.GetBytes(fileName[j].ToString()))[0]);
                        hideInfo.Add((Encoding.Unicode.GetBytes(fileName[j].ToString()))[1]);
                    }

                    //размер расширение(1byte) расширение файла(xbyte)
                    hideInfo.Add((byte)fileExt.Length);
                    for (int j = 0; j < fileExt.Length; j++)
                        hideInfo.Add((byte)fileExt[j]);

                    //чтение файла в байтах
                    byte[] btHideArray = File.ReadAllBytes(pathArray[i]);

                    UInt64 secretLength = (UInt64)btHideArray.Length;
                    for (int j = 0; j < 5; j++)
                        hideInfo.Add((byte)(secretLength >> ((4 - j) * 8)));

                    for (long j = 0; j < btHideArray.Length; j++)
                        hideInfo.Add(btHideArray[j]);

                }

                if (checkBox.Checked)
                {
                    AES cryptPattern = new AES(encryptTextBox.Text);
                    hideInfo = cryptPattern.Encrypt(hideInfo.ToArray()).OfType<byte>().ToList();
                }

                //String savePath = "\\HideHistory\\" + DateTime.Now.ToString().Replace('/', '.').Replace(' ', '_').Replace(':', '.');
                //Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + savePath);
                //savePath = System.IO.Directory.GetCurrentDirectory() + savePath + "\\";

                pathArray = new String[containerGridView.Rows.Count];
                for (int i = 0; i < pathArray.Length; i++)
                    pathArray[i] = containerGridView.Rows[i].Cells[2].Value.ToString();

                //начало скрыте секретных файлов
                for (int i = 0; i < containerGridView.Rows.Count; i++)
                {
                    if (hideInfo.Count == 0)
                        break;

                    int hideCount = hideInfo.Count > Convert.ToInt32(containerGridView.Rows[i].Cells[1].Value) ? Convert.ToInt32(containerGridView.Rows[i].Cells[1].Value) : hideInfo.Count;
                    FileInfo fileInfo = new FileInfo(pathArray[i]);

                    switch (fileInfo.Extension.ToLower())
                    {
                        case ".png":
                        case ".bmp":
                            BMP.bmpEncode(hideInfo.GetRange(0, hideCount), savePath + fileInfo.Name, new Bitmap(pathArray[i]), stegKey);
                            break;
                        case ".wav":
                            WAV shablon = new WAV(containerGridView.Rows[i].Cells[2].Value.ToString());

                            shablon.WavEncode(hideInfo.GetRange(0, hideCount), savePath + fileInfo.Name, stegKey);
                            break;
                        case ".jpg":
                        case ".jpeg":
                            JPEG jpeg = new JPEG(pathArray[i],true);
                            jpeg.jpegEncode(hideInfo.GetRange(0, hideCount).ToArray(), savePath + fileInfo.Name, stegKey);
                            break;
                    }
                    hideInfo.RemoveRange(0, hideCount);
                }

                try { loadingPeocess.Kill(); }
                catch (Exception err) { }
                MessageBox.Show("Information Hided!");

            }
            else
            {
                String[] pathArray = new String[containerGridView.Rows.Count];
                List<byte> findInfo = new List<byte>();

                for (int i = 0; i < pathArray.Length; i++)
                    pathArray[i] = containerGridView.Rows[i].Cells[2].Value.ToString();

                for (int i = 0; i < containerGridView.Rows.Count; i++)
                {
                    FileInfo fileInfo = new FileInfo(pathArray[i]);

                    switch (fileInfo.Extension.ToLower())
                    {
                        case ".png":
                        case ".bmp":
                            findInfo.AddRange(BMP.bmpDecode(new Bitmap(pathArray[i]), Convert.ToInt32(containerGridView.Rows[i].Cells[1].Value),stegKey));
                            break;
                        case ".wav":
                            WAV shablon = new WAV(containerGridView.Rows[i].Cells[2].Value.ToString());
                            findInfo.AddRange(shablon.WavDecode(Convert.ToInt32(containerGridView.Rows[i].Cells[1].Value), stegKey));
                            break;
                        case ".jpg":
                        case ".jpeg":
                            JPEG jpeg = new JPEG(pathArray[i], false);
                            jpeg.jpegDecode(stegKey);
                            findInfo.AddRange(jpeg.secretFiles);
                            break;
                    }
                }

                try
                {
                    UInt64 fileQanakByte, fileCount;
                    List<byte> qanak = new List<byte>();
                    qanak = findInfo.GetRange(0, 16);


                    if (checkBox.Checked)
                    {
                        AES cryptPattern = new AES(encryptTextBox.Text);
                        qanak = cryptPattern.DeCrypt(qanak.ToArray()).OfType<byte>().ToList();
                    }

                    String str = "";

                    for (int i = 0; i < 5; i++)
                        str += HelpTools.AutoAddByte(Convert.ToString(qanak[i], 2), 8);

                    fileQanakByte = Convert.ToUInt64(str, 2);
                    str = "";

                    findInfo.RemoveRange((int)fileQanakByte, (int)((UInt64)findInfo.Count - (UInt64)fileQanakByte));

                    for (int i = 5; i < 10; i++)
                        str += HelpTools.AutoAddByte(Convert.ToString(qanak[i], 2), 8);

                    fileCount = Convert.ToUInt64(str, 2);

                    if (checkBox.Checked)
                    {
                        findInfo = new AES(encryptTextBox.Text).DeCrypt(findInfo.ToArray()).OfType<byte>().ToList();
                    }

                    //String savePath = "\\UnHideHistory\\" + DateTime.Now.ToString().Replace('/', '.').Replace(' ', '_').Replace(':', '.');
                    //Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + savePath);
                    //savePath = System.IO.Directory.GetCurrentDirectory() + savePath + "\\";

                    //SKSVAV


                    findInfo.RemoveRange(0, 10);

                    for (int i = 0; i < (int)fileCount; i++)
                    {
                        int nom = 0;
                        int nameLen = findInfo[nom++];

                        str = "";
                        String fileName = "";
                        for (int j = 0; j < nameLen; j++)
                            fileName += Encoding.Unicode.GetString(new byte[2]{
                        Convert.ToByte(HelpTools.AutoAddByte(Convert.ToString(findInfo[nom++], 2), 8),2),
                        Convert.ToByte(HelpTools.AutoAddByte(Convert.ToString(findInfo[nom++], 2), 8),2) });

                        //cevachap
                        String fileExt = "";
                        int len = findInfo[nom++];
                        for (int j = 0; j < len; j++)
                            fileExt += Encoding.ASCII.GetString(new byte[1] { findInfo[nom++] });

                        fileExt = "." + fileExt;

                        String componentCount = "";
                        for (int j = 0; j < 5; j++)
                            componentCount += HelpTools.AutoAddByte(Convert.ToString(findInfo[nom++], 2), 8);

                        int compCount = Convert.ToInt32(componentCount, 2);

                        byte[] btArray = findInfo.GetRange(nom, compCount).ToArray();
                        findInfo.RemoveRange(0, nom + compCount);
                        //nom += compCount;
                        File.WriteAllBytes(savePath + fileName + fileExt, btArray);

                    }
                }
                catch
                {
                    try{loadingPeocess.Kill();}
                    catch (Exception err) { }

                    MessageBox.Show("Hidden information not found!");
                    return;
                }

                try { loadingPeocess.Kill(); }
                catch (Exception err) { }
                MessageBox.Show("Information Finded!");
            }

        }

        //remove buttons
        private void BtnContainerRmove_Click(object sender, EventArgs e)
        {
            if (containerGridView.SelectedRows.Count < 1)
                return;

            foreach (DataGridViewRow r in containerGridView.SelectedRows)
                if (!r.IsNewRow)
                    containerGridView.Rows.RemoveAt(r.Index);

            labelContainer.Text = "Can Hide: " + GetPoints(GetSize(containerGridView)) + " byte";
        }
        private void BtnDataRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            foreach (DataGridViewRow r in dataGridView.SelectedRows)
                if (!r.IsNewRow)
                    dataGridView.Rows.RemoveAt(r.Index);

            labelHide.Text = "File Size: " + GetPoints(GetSize(dataGridView)) + " byte";
        }

        //password enavle check box
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
                encryptTextBox.Enabled = true;
            else
                encryptTextBox.Enabled = false;

        }

        //form close
        private void StegPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                form.Activate();
                form.Show();
                return;
            }
        }

    }
}
