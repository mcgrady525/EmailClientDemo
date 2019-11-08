using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSharing.Frameworks.Common;
using SSharing.Frameworks.Email;

namespace ConsoleApplication65
{
    public static class EmailHelper
    {
        /// <summary>
        /// 获取EmailWrapper的唯一实例
        /// </summary>
        public static EmailWrapper Instace
        {
            get
            {
                return SingletonProvider<EmailWrapper>.Instance;
            }
        }

    }
}
