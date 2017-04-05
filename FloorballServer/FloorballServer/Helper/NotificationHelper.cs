using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloorballServer.Helper
{
    public class NotificationHelper
    {

        public static List<string> GetEventBodyArgs(Event e)
        {
            List<string> args = new List<string>();

            switch (e.Type)
            {
                case "G":
                    args.Add(e.Match.HomeTeam.Name.ToString());
                    args.Add(e.Match.GoalsH.ToString());
                    args.Add(e.Match.GoalsA.ToString());
                    args.Add(e.Match.AwayTeam.Name.ToString());
                    args.Add(e.Time.ToString());
                    args.Add(e.Player.FirstName + " " + e.Player.SecondName);
                    args.Add(e.EventMessage.Message);
                    break;
                case "P2":
                    break;
                case "P5":
                    break;
                case "P10":
                    break;
                case "PV":
                    break;
                default:
                    break;
            }

            return args;
        }

        public static List<string> GetEventTitleArgs(Event e)
        {
            List<string> args = new List<string>();

            switch (e.Type)
            {
                case "G":
                    
                    break;
                case "P2":
                    break;
                case "P5":
                    break;
                case "P10":
                    break;
                case "PV":
                    break;
                default:
                    break;
            }

            return args;
        }


    }
}