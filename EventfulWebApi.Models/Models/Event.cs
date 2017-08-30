using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EventfulWebApi.Models
{
    public class Small
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class Medium
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class Thumb
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class Image
    {
        public Small small { get; set; }
        public string width { get; set; }
        public string caption { get; set; }
        public Medium medium { get; set; }
        public string url { get; set; }
        public Thumb thumb { get; set; }
        public string height { get; set; }
    }

    public class Event
    {
        public string watching_count { get; set; }
        public string olson_path { get; set; }
        public int calendar_count { get; set; }
        public int comment_count { get; set; }
        public string region_abbr { get; set; }
        public string postal_code { get; set; }
        public int going_count { get; set; }
        public int all_day { get; set; }
        public float latitude { get; set; }
        public string groups { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public string privacy { get; set; }
        public string city_name { get; set; }
        public int link_count { get; set; }
        public float longitude { get; set; }
        public string country_name { get; set; }
        public string country_abbr { get; set; }
        public string region_name { get; set; }
        public string start_time { get; set; }
        public Int16 tz_id { get; set; }
        public string description { get; set; }
        public string modified { get; set; }
        public string venue_display { get; set; }
        public string tz_country { get; set; }
       
        public string title { get; set; }
        public string venue_address { get; set; }
        public string geocode_type { get; set; }
        public string tz_olson_path { get; set; }
        public string recur_string { get; set; }
        public string calendars { get; set; }
        public string owner { get; set; }
        public string going { get; set; }
        public string country_abbr2 { get; set; }
        public Image image { get; set; }
        public string created { get; set; }
        public string venue_id { get; set; }
        public string tz_city { get; set; }
        public string stop_time { get; set; }
        public string venue_name { get; set; }
        public string venue_url { get; set; }
    }
    public class Events
    {
        public List<Event> Event { get; set; }
    }
    public class EventSearchResults
    {
        public int? last_item { get; set; }
        public int total_items { get; set; }
        public int? first_item { get; set; }
        public int? page_number { get; set; }
        public int page_size { get; set; }
        public int? page_items { get; set; }
        public float search_time { get; set; }
        public int page_count { get; set; }       
        public Events events { get; set; }
    }
}
