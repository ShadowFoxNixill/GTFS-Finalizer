using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Nixill.Collections;

namespace Nixill.GTFS {
  public class Config {
    private static ConfigObject Cfg;

    public static string TimeZone { get => Cfg?.TimeZone; }
    public static string AgencyIDTag { get => Cfg?.IDs?.Agency?.Tag; }
    public static string RouteIDTag { get => Cfg?.IDs?.Route?.Tag; }
    public static string StopIDTag { get => Cfg?.IDs?.Stop?.Tag; }
    public static string AgencyIDPattern { get => Cfg?.IDs?.Agency?.Pattern; }
    public static string RouteIDPattern { get => Cfg?.IDs?.Route?.Pattern; }
    public static string StopIDPattern { get => Cfg?.IDs?.Stop?.Pattern; }
    public static string AgencyNameTag { get => Cfg?.Names?.Agency?.Tag ?? "name"; }
    public static string RouteNameTag { get => Cfg?.Names?.Route?.Tag ?? "name"; }
    public static string StopNameTag { get => Cfg?.Names?.Stop?.Tag ?? "name"; }
    public static string AgencyNamePattern { get => Cfg?.Names?.Agency?.Pattern; }
    public static string RouteNamePattern { get => Cfg?.Names?.Route?.Pattern; }
    public static string StopNamePattern { get => Cfg?.Names?.Stop?.Pattern; }
    public static List<Calendar> Calendars { get => Cfg?.Calendars; }
    public static List<string> BlankCalendars { get => Cfg?.BlankCalendars; }
    public static string Today = DateTime.Today.ToString("yyyyMMdd");

    public static void Load() {
      Cfg = JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText("settings.json"));
    }

    public static string PatternExtract(string value, string pattern) {
      try {
        Regex ptn = new Regex(pattern);
        Match match = ptn.Match(value);
        return match.Groups[1].Value;
      }
      catch (Exception) {
        return value;
      }
    }
  }

  public class ConfigObject {
    public string TimeZone;
    public IDTagList IDs;
    public NameTagList Names;
    public List<Calendar> Calendars;
    public List<String> BlankCalendars;
  }

  public class IDTagList {
    public TagInfo Agency;
    public TagInfo Route;
    public TagInfo Stop;
  }

  public class NameTagList {
    public TagInfo Agency;
    public TagInfo Route;
    public TagInfo Stop;
  }

  public class TagInfo {
    public string Tag;
    public string Pattern;

    public static implicit operator TagInfo(string input) => new TagInfo { Tag = input };
  }

  public class Calendar {
    public DateTime Start;
    public DateTime End;
    public string Monday;
    public string Tuesday;
    public string Wednesday;
    public string Thursday;
    public string Friday;
    public string Saturday;
    public string Sunday;
    public Dictionary<DateTime, string> Exceptions;
  }
}