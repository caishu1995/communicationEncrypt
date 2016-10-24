using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace connectSocket
{
    public partial class Form1 : Form
    {
        Socket socketSend;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 开始连接点击事件
        /// </summary>
        private void listenButton_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ipText.Text);//ip
                int port = Convert.ToInt32(portText.Text);//端口号

                ///连接目标网端
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建socket对象
                IPEndPoint ipEnd = new IPEndPoint(ip, port);//设置目标网端
                socketSend.Connect(ipEnd); //连接

                ///显示
                showRich.Text += "连接成功\r\n";
                MessageBox.Show("连接成功");

                ///后台接收
                Thread th = new Thread(Recive);
                th.IsBackground = true;
                th.Start(socketSend);
            }
            catch (Exception ee)
            {
                showRich.Text += "连接失败\r\n";
                MessageBox.Show(ee.ToString());
            }
        }

        /// <summary>
        /// 一个线程后台运行程序，用来接收客户端传送数据
        /// </summary>
        /// <param name="socket">应该为Socket类型</param>
        private void Recive(object socket)
        {
            Socket socketSend = socket as Socket;
            while (true)
            {
                try
                {
                    byte[] typeByte = new byte[2];
                    int r = socketSend.Receive(typeByte, 2, 0);//接收类型
                    string typeStr = Encoding.UTF8.GetString(typeByte);

                    ///FL表示文件，NP表示需返回公钥，SK表示为震动
                    ///PK表示为公钥，ND表示需解密
                    if (typeStr == "FL")
                    {
                        ReciveFile(socketSend);

                        ///显示
                        showRich.Text += "保存文件成功\r\n";
                        MessageBox.Show("成功");
                    }
                    else if (typeStr == "NP")
                    {
                        ReciveNeedPublicKey(socketSend);
                    }
                    else if (typeStr == "SK")
                    {
                        ReciveShock();

                        ///显示
                        showRich.Text += socketSend.RemoteEndPoint + "发送震动\r\n";
                        MessageBox.Show("震动完成");
                    }
                    else if (typeStr == "PK")
                    {
                        byte[] buffer = RecivePublicKey(socketSend, messageRich.Text.Trim());

                        ///显示
                        if (buffer == null)
                        {
                            showRich.Text += "无法获得公钥";
                            MessageBox.Show("发送失败");
                        }
                        else
                        {
                            showRich.Text += messageRich.Text + "\r\n";
                            MessageBox.Show(Encoding.UTF8.GetString(buffer) + "\r\n发送成功");
                        }
                    }
                    else if (typeStr == "ND")
                    {
                        string decryptStr = ReciveNeedDecrypt(socketSend);

                        ///显示
                        if (decryptStr == null)
                        {
                            showRich.Text += "解密失败";
                            MessageBox.Show("解密失败");
                        }
                        else
                        {
                            showRich.Text += socketSend.RemoteEndPoint.ToString() + ":" + decryptStr + "\r\n";
                            MessageBox.Show("接收成功");
                        }
                    }
                }
                catch (Exception ee) { MessageBox.Show(ee.ToString()); }
            }
        }

        /// <summary>
        /// 接收为文件的处理方式,接收文件最大长度为2M
        /// </summary>
        private void ReciveFile(Socket socketSend)
        {
            byte[] num = new byte[11];         //存储大小
            byte[] name = new byte[51];        //存储文件名
            byte[] buffer = new byte[1024 * 5];//一次最多为5k
            byte[] newBuffer = new byte[1024 * 1024 * 2 + 1];//总共可接受2M

            ///第二次接收大小
            int rLeng = socketSend.Receive(num, 10, 0);//第二次接收，接收大小
            int endLength = 0;                         //最终大小
            for (int i = 9; i >= 0; i--)
                endLength = endLength * 10 + num[i];

            ///第三次接收文件名
            int rFileName = socketSend.Receive(name, 50, 0);
            string nameStr = Encoding.UTF8.GetString(name);

            ///第四次接收组装
            int length = 0;                          //已经存放的总长度
            while (length < endLength)
            {
                int rr = socketSend.Receive(buffer, buffer.Length, 0);//接收，接收内容

                ///如果超出当初约定的大小，则只保留约定内容
                if (rr + length >= endLength)
                    rr = endLength - length;

                for (int i = 0; i < rr; i++)
                {
                    newBuffer[length + i] = buffer[i];
                }

                length += rr;
            }

            ///选择保存位置
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\Users\Administrator\Desktop";
            sfd.Title = "选择保存位置";
            sfd.Filter = "所有文件|*.*";
            sfd.FileName = nameStr;
            sfd.ShowDialog(this);

            ///保存文件
            string path = sfd.FileName;//获取路径
            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))//创建只写流
            {
                fsWrite.Write(newBuffer, 0, length);
            }
        }

        /// <summary>
        /// 接收到需要返回公钥的处理方式
        /// </summary>
        /// <param name="socketSend">需回复的ip</param>
        private void ReciveNeedPublicKey(Socket socketSend)
        {
            RSAKey rk = new RSAKey();
            rk.setRSAKey(@"privateKey.xml", @"publicKey.xml");//创建公钥私钥，并写入指定文件夹内

            ///序号
            string typeStr = "PK";
            byte[] numByte = Encoding.UTF8.GetBytes(typeStr);

            ///公钥
            string keyStr = rk.getRSAKey(@"publicKey.xml");
            byte[] keyByte = Encoding.UTF8.GetBytes(keyStr);

            ///发送
            socketSend.Send(numByte, 2, 0);//发送序号
            socketSend.Send(keyByte, keyByte.Length, 0);//发送公钥
        }

        /// <summary>
        /// 接收为震动的处理方式
        /// </summary>
        private void ReciveShock()
        {
            for (int i = 0; i < 500; i++)
            {
                this.Location = new Point(200, 200);
                this.Location = new Point(280, 280);
            }
        }

        /// <summary>
        /// 接收为公钥的处理方式，接收公钥后加密，将结果返回
        /// </summary>
        /// <param name="content">需加密的内容</param>
        /// <returns>加密后的内容</returns>
        private byte[] RecivePublicKey(Socket socketSend, string content)
        {
            byte[] keyByte = new byte[1024 * 5];            //接收5K
            int rKey = socketSend.Receive(keyByte, keyByte.Length, 0); //接收公钥
            string RSAPublicKey = Encoding.UTF8.GetString(keyByte);

            ///用获得的公钥加密信息内容,如果已获得公钥则发送加密后的值
            if (RSAPublicKey != null)
            {
                byte[] buffer = Encrypt(socketSend, RSAPublicKey, content);
                return buffer;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 加密并发送信息
        /// </summary>
        /// <param name="RSAPublicKey">获得的公钥</param>
        /// <param name="content">需加密的内容</param>
        /// <returns>加密后的内容</returns>
        private byte[] Encrypt(Socket socketSend, string RSAPublicKey, string content)
        {
            ///加密
            RSAChange rsa = new RSAChange();
            string encryptStr = rsa.RSAEncrypt(RSAPublicKey,content);//加密
            byte[] buffer = Encoding.UTF8.GetBytes(encryptStr);//转换成可以传输的类型

            ///发送类型
            string type = "ND";
            byte[] typeB = Encoding.UTF8.GetBytes(type);

            ///发送
            socketSend.Send(typeB);//发送类型
            socketSend.Send(buffer);//发送加密后结果

            return buffer;
        }

        /// <summary>
        /// 接收到需要返回解密的处理方式
        /// </summary>
        /// <returns>解密后的内容</returns>
        private string ReciveNeedDecrypt(Socket socketSend)
        {
            RSAKey rk = new RSAKey();
            RSAChange rDecrypt = new RSAChange();

            ///接收
            byte[] buffer = new byte[1024 * 5];//一次为5k
            int r = socketSend.Receive(buffer);//接收
            string str = Encoding.UTF8.GetString(buffer, 0, r);//转成字符串

            ///解密
            string strDecrypt = rDecrypt.RSADecrypt(rk.getRSAKey(@"privateKey.xml"), str);//解密

            return strDecrypt;
        }

        /// <summary>
        /// 发送信息单击事件
        /// </summary>
        private void sendButton_Click(object sender, EventArgs e)
        {
            ///发送消息，提示目标地址回发公钥
            socketSend.Send(Encoding.UTF8.GetBytes("NP"));
        }
    }
}
