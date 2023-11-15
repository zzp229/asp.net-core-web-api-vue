using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Other
{
    public class LoginModel
    {
		private string userName;
		/// <summary>
		/// 账号
		/// </summary>
		public string Uid
        {
			get { return userName; }
			set { userName = value; }
		}


		private string password;
		/// <summary>
		/// 密码
		/// </summary>
		public string Pwd
        {
			get { return password; }
			set { password = value; }
		}


	}
}
