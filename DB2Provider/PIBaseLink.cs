using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PiLink;

namespace DB2Provider
{
    public class PIBaseLink
    {
        /// <summary>
        /// 服务器名
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// PI连接对象
        /// </summary>
        public PiLink.declare PIDeclare { get; set; }
        /// <summary>
        /// 连接PI服务器
        /// </summary>
        /// <returns></returns>
        private int LinkPI()
        {
            if (PIDeclare == null)
                PIDeclare = new declare();
            string serverName = ServerName;
            string userName = UserName;
            string passWord = PassWord;
            return PIDeclare.ConnectToServer(ref serverName, ref userName, ref passWord);
        }
        /// <summary>
        /// 获取PI服务连接对象
        /// </summary>
        public PiLink.declare GetPI()
        {
            int linkState = LinkPI();
            if (linkState == 0)
                return PIDeclare;
            else
                return null;
        }
        /// <summary>
        /// 连接PI服务器
        /// </summary>
        /// <param name="serverName">服务名</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        private int LinkPI(string serverName, string userName, string passWord)
        {
            if (PIDeclare == null)
                PIDeclare = new declare();
            return PIDeclare.ConnectToServer(ref serverName, ref userName, ref passWord);
        }
        /// <summary>
        /// 获取PI服务连接对象
        /// </summary>
        public PiLink.declare GetPI(string serverName, string userName, string passWord)
        {
            int linkState = LinkPI(serverName, userName, passWord);
            if (linkState == 0)
                return PIDeclare;
            else
                return null;
        }
        /// <summary>
        /// 关闭PI服务器连接
        /// </summary>
        public void CloseConnection()
        {
            if (PIDeclare != null)
                PIDeclare.DisConnect();
        }
    }
}
