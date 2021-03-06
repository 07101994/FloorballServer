﻿using DAL;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessagingService
{
    public class NotificationHelper
    {

        public static List<string> GetEventBodyArgs(Event e)
        {
            List<string> args = new List<string>();

            switch (e.Type)
            {
                case EventType.G:
                    args.Add(e.Match.HomeTeam.Name.ToString());
                    args.Add(e.Match.ScoreH.ToString());
                    args.Add(e.Match.ScoreA.ToString());
                    args.Add(e.Match.AwayTeam.Name.ToString());
                    args.Add(e.Time.ToString());
                    args.Add(e.Player.FirstName + " " + e.Player.LastName);
                    args.Add(e.EventMessage.Message);
                    break;
                case EventType.P2:
                    break;
                case EventType.P5:
                    break;
                case EventType.P10:
                    break;
                case EventType.PV:
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
                case EventType.G:
                    
                    break;
                case EventType.P2:
                    break;
                case EventType.P5:
                    break;
                case EventType.P10:
                    break;
                case EventType.PV:
                    break;
                default:
                    break;
            }

            return args;
        }

        public static List<string> GetMatchWillStartTitleArgs(Match m)
        {
            List<string> args = new List<string>();



            return args;
        }

        public static List<string> GetMatchWillStartBodyArgs(Match m)
        {
            List<string> args = new List<string>();

            //args.Add()

            return args;
        }

    }
}