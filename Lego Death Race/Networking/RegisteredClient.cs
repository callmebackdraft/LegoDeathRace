using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lego_Death_Race.Networking
{
    public class RegisteredClient
    {
        private EndPoint mEndPoint;
        private DateTime mLastAccessed;
        public EndPoint EndPoint
        {
            get
            {
                return mEndPoint;
            }
        }

        public bool Connected
        {
            get
            {
                return DateTime.Now.Subtract(mLastAccessed).Seconds >= 1 ? false : true;
            }
        }

        public int PlayerId
        {
            get
            {
                string ip = ((IPEndPoint)EndPoint).Address.ToString();
                switch(ip.Substring(ip.Length - 3, 3))
                {
                    case "118":
                        return 0;
                    case "121":
                        return 1;
                    default:
                        return -1;
                }
            }
        }

        public RegisteredClient(EndPoint endPoint)
        {
            mEndPoint = endPoint;
            ResetLastTimeAccessed();
        }

        public void ResetLastTimeAccessed()
        {
            mLastAccessed = DateTime.Now;
        }
    }
}
