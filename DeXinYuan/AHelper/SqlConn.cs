using Common;
using System.Web;

namespace DeXinYuan
{
    public class SqlConn
    {
        public static string SqlContention;

        /// <summary>
        /// SQL连接对象
        /// </summary>
        static SqlConn()
        {
            SqlContention = ConfigContext.ConnctionString/*+HttpContext.Current.Server.MapPath(ConfigContext.Db)*/;
        }
    }
}