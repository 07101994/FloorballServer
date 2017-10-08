using System;

namespace DAL
{
    public enum CountriesEnum
    {
        HU, SE, FL, SW, CZ
    }

    public enum StateEnum
    {
        Confirmed, Playing, Ended
    }


    public enum ApplicationType
    {
        Manager = 0, Consumer = 1
    }

    public static class EnumExtensions
    {
        public static string ToCountryString(this CountriesEnum country)
        {
            switch (country)
            {
                case CountriesEnum.HU:
                    return "HU";
                case CountriesEnum.SE:
                    return "SE";
                case CountriesEnum.FL:
                    return "FL";
                case CountriesEnum.SW:
                    return "SW";
                case CountriesEnum.CZ:
                    return "CZ";
                default:
                    return "";
            }
        }

        public static string ToUpdateString(this UpdateEnum country)
        {
            switch (country)
            {
                case UpdateEnum.League:
                    return "League";
                case UpdateEnum.Team:
                    return "Team";
                case UpdateEnum.Match:
                    return "Match";
                case UpdateEnum.Player:
                    return "Player";
                case UpdateEnum.Stadium:
                    return "Stadium";
                case UpdateEnum.Referee:
                    return "Referee";
                case UpdateEnum.Event:
                    return "Event";
                case UpdateEnum.EventMessage:
                    return "EventMessage";
                case UpdateEnum.PlayerTeam:
                    return "PlayerTeam";
                case UpdateEnum.PlayerMatch:
                    return "PlayerMatch";
                case UpdateEnum.RefereeMatch:
                    return "RefereeMatch";
                default:
                    return "League";
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this int value)
        {
            return (T)Enum.ToObject(typeof(T),value);
        }

    }

    public enum UpdateEnum
    {
        League, Team, Match, Player, Stadium, Referee, Event, EventMessage,
        PlayerTeam, PlayerMatch, RefereeMatch
    }

    public enum UpdateType
    {
        Create, Update, Delete
    }

    public enum LeagueTypeEnum
    {
        League, Cup, PlayOff
    }

    public enum ClassEnum
    {
        FirstClass, SecondClass, ThirdClass, U21, U19, U17, U15, U13, U11, U9, All
    }

    public enum GenderEnum
    {
        Men, Women
    }  
    
    public enum EventType
    {
        G,A,P2,P5,P10,PV,T,S
    }

    public enum StatType
    {
        G,A,P2,P5,P10,PV,M
    }
}
