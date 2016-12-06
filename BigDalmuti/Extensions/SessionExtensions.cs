using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigDalmuti.Extensions
{
    public static class SessionExtensions
    {
        /// <summary> 
        /// Get value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        public static int GetPlayerNumberFromSession(this HttpSessionStateBase session)
        {
            return (int)session["playerNumber"];
        }

        /// <summary> 
        /// Set value. 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="session"></param> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        public static void SetPlayerNumberToSession(this HttpSessionStateBase session, int playerNumber)
        {
            session["playerNumber"] = playerNumber;
        }
    }
}
