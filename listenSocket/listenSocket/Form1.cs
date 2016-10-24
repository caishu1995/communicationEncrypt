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

namespace listenSocket
{
    public partial class Form1 : Form
    {
        //将远程连接的客户端的IP地址和Socket存入集合中
        Dictionary<string, Socket> socketDic = new Dictionary<string, Socket>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;//取消跨线程检查
        }

        /// <summary>
        /// 开始监听 按钮单击事件，执行链接
        /// </summary>
        private void listenButton_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ipLabel.Text); //ip地址
                int port = Convert.ToInt32(portText.Text);//端口号

                ///监控目标网端
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个负责监IP地址跟端口号的Socket
                IPEndPoint ipEnd = new IPEndPoint(ip, port);//创建网络端点
                socketWatch.Bind(ipEnd);//将端点和socket链接
                socketWatch.Listen(2);//监听

                ///显示
                showRich.Text += "监听成功\r\n";
                MessageBox.Show("监听成功");

                ///后台监听
                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(socketWatch);
            }
            catch (Exception ee) 
            {
                showRich.Text += "监听失败\r\n"; 
                MessageBox.Show(ee.ToString());
            }
        }

        /// <summary>
        /// 一个线程后台运行程序
        /// </summary>
        /// <param name="socket">应该为Socket类型</param>
        private void Listen(object socket)
        {
            Socket socketWatch = socket as Socket;
            Socket socketSend;

            while (true)
            {
                try
                {
                    socketSend = socketWatch.Accept();//获取跟客户端通信的Socket
                    socketDic.Add(socketSend.RemoteEndPoint.ToString(), socketSend);//将信息存入<ip,socket>集合
                    clientListCombo.Items.Add(socketSend.RemoteEndPoint.ToString());//将IP地址存入下拉框
                    
                    ///显示信息
                    showRich.Text += socketSend.RemoteEndPoint.ToString() + "连接成功\r\n";
                    MessageBox.Show("连接成功");

                    ///创建一个线程来获取该客户端传送的信息
                    Thread th = new Thread(Recive);
                    th.IsBackground = true;
                    th.Start(socketSend);
                }
                catch (Exception ee) { MessageBox.Show(ee.ToString()); }
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
                    int tfr = socketSend.Receive(typeByte, 2, 0);//接收
                    string typeStr = Encoding.UTF8.GetString(typeByte);//转换

                    ///NP表示需返回公钥，ND表示需解密，PK表示为公钥
                    if (typeStr == "NP")
                    {
                        ReciveNeedPublicKey(socketSend);
                    }
                    else if (typeStr == "ND")
                    {
                        string decryptStr = ReciveNeedDecrypt(socketSend);

                        ///显示
                        if (decryptStr != null)
                        {
                            showRich.Text += socketSend.RemoteEndPoint.ToString() + ":" + decryptStr + "\r\n";
                            MessageBox.Show("接收成功");
                        }
                        else
                        {
                            showRich.Text += "接收失败\r\n";
                            MessageBox.Show("接收失败");
                        }
                    }
                    else if (typeStr == "PK")
                    {
                        byte[] buffer = RecivePublicKey(socketSend, txtText.Text);

                        ///显示
                        if (buffer == null)
                        {
                            showRich.Text += "无法获得公钥";
                            MessageBox.Show("发送失败");
                        }
                        else
                        {
                            showRich.Text += txtText.Text + "\r\n";
                            MessageBox.Show(Encoding.UTF8.GetString(buffer) + "\r\n发送成功");
                        }
                    }
                    
                }
                catch (Exception ee) { MessageBox.Show(ee.ToString()); }
            }
        }

        /// <summary>
        /// 接收到需要返回公钥的处理方式
        /// </summary>
        private void ReciveNeedPublicKey(Socket socketSend)
        {
            RSAKey rk = new RSAKey();
            rk.setRSAKey(@"privateKey.xml", @"publicKey.xml");//创建公钥私钥，并写入指定文件夹内

            ///序号
            string type = "PK";
            byte[] num = Encoding.UTF8.GetBytes(type);
            
            ///公钥
            string sKey = rk.getRSAKey(@"publicKey.xml");
            byte[] keyBu = Encoding.UTF8.GetBytes(sKey);

            ///发送
            socketSend.Send(num, 2, 0);//发送序号
            socketSend.Send(keyBu, keyBu.Length, 0);//发送公钥
        }

        /// <summary>
        /// 接收到需要返回解密的处理方式
        /// </summary>
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
        /// 接收为公钥的处理方式，用公钥加密
        /// </summary>
        /// <param name="content">需加密的内容</param>
        /// <returns>加密后的结果</returns>
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
        /// <returns>加密后的结果</returns>
        private byte[] Encrypt(Socket socketSend, string RSAPublicKey, string content)
        {
            ///加密
            RSAChange rsa = new RSAChange();
            string encryptStr = rsa.RSAEncrypt(RSAPublicKey, content);//加密
            byte[] buffer = Encoding.UTF8.GetBytes(encryptStr);//转换成可以传输的类型

            ///发送类型
            string typeStr = "ND";
            byte[] typeByte = Encoding.UTF8.GetBytes(typeStr);

            ///发送
            socketSend.Send(typeByte);//发送类型
            socketSend.Send(buffer);//发送加密后结果

            return buffer;
        }

        /// <summary>
        /// 选择文件 按钮单击事件，执行查找文件功能
        /// </summary>
        private void fileChooseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\Administrator\Desktop";
            ofd.Title = "传送文件";
            ofd.Filter = "所有文件|*.*";
            ofd.ShowDialog();

            txtText.Text = ofd.FileName;
        }

        /// <summary>
        /// 发送文件点击事件
        /// </summary>
        private void fileSendButton_Click(object sender, EventArgs e)
        {
            string path = txtText.Text;                   //获取路径
            byte[] typeByte = new byte[2];                //传输类型
            byte[] lengByte = new byte[10];               //传输大小
            byte[] nameByte = new byte[50];               //传输名称
            byte[] buffer = new byte[1024 * 1024 * 2];    //只能传送2M

            try
            {
                ///类型
                string typeStr = "FL";
                typeByte = Encoding.UTF8.GetBytes(typeStr);

                ///读取文件并获得实际大小
                int r;
                using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))//创建只读文件流
                {
                    r = fsRead.Read(buffer, 0, buffer.Length);
                }

                ///转换长度
                char[] num = new char[10];
                int i = 0;
                while(r != 0)
                {
                    num[i] = Convert.ToChar(r%10);
                    i++;
                    r = r / 10;
                }
                lengByte = Encoding.Default.GetBytes(num);

                ///转换名称
                string nameStr = Path.GetFileName(path);              //获取名称
                byte[] nameByte1 = Encoding.UTF8.GetBytes(nameStr);
                for (int j = 0; j < 50; j++)
                {
                    if(j < nameByte1.Length)
                        nameByte[j] = nameByte1[j];
                    else
                        nameByte[j] = 0;
                }

                ///发送
                socketDic[clientListCombo.SelectedItem.ToString()].Send(typeByte, 2, 0); //发送类型
                socketDic[clientListCombo.SelectedItem.ToString()].Send(lengByte, 10, 0);//发送长度
                socketDic[clientListCombo.SelectedItem.ToString()].Send(nameByte, 50, 0);//发送名称
                socketDic[clientListCombo.SelectedItem.ToString()].Send(buffer, buffer.Length, 0);//发送内容

                ///显示
                showRich.Text += socketDic[clientListCombo.SelectedItem.ToString()].RemoteEndPoint + ":发送文件成功\r\n";
                MessageBox.Show("发送成功");
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        /// <summary>
        /// 发送消息点击事件
        /// </summary>
        private void msgSendButton_Click(object sender, EventArgs e)
        {
            try
            {
                ///发送消息，提示目标地址回发公钥
                Socket socketSend = socketDic[clientListCombo.SelectedItem.ToString()];
                socketSend.Send(Encoding.UTF8.GetBytes("NP"));
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }

        /// <summary>
        /// 发送震动点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shockSendButton_Click(object sender, EventArgs e)
        {
            string typeStr = "SK";
            byte[] typeByte = Encoding.UTF8.GetBytes(typeStr);  //传输类型

            try
            {
                ///发送
                socketDic[clientListCombo.SelectedItem.ToString()].Send(typeByte, 2, 0);

                ///显示
                showRich.Text += clientListCombo.SelectedText + ":发送震动成功\r\n";
                MessageBox.Show("震动成功");
            }
            catch (Exception ee) { MessageBox.Show(ee.ToString()); }
        }
    }
}
