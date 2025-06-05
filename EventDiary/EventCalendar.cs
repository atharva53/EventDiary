using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace EventDiary
{
    [Serializable]
    public class Hour
    {
        public int Value { get; set; }
        public List<EventInfo> Events { get; set; } = new List<EventInfo>();

        public Hour() { }

        public Hour(int value)
        {
            Value = value;
        }
    }

    [Serializable]
    public class Day
    {
        public int Value { get; set; }
        public List<Hour> Hours { get; set; } = new List<Hour>();

        public Day() 
        {
            // Initialize all 24 hours
            for (int i = 0; i < 24; i++)
            {
                Hours.Add(new Hour(i));
            }
        }

        public Day(int value) : this()
        {
            Value = value;
        }
    }

    [Serializable]
    public class Month
    {
        public int Value { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();

        public Month() { }

        public Month(int value, int daysInMonth)
        {
            Value = value;
            
            // Initialize all days in this month
            for (int i = 1; i <= daysInMonth; i++)
            {
                Days.Add(new Day(i));
            }
        }
    }

    [Serializable]
    public class Year
    {
        public int Value { get; set; }
        public List<Month> Months { get; set; } = new List<Month>();

        public Year() { }

        public Year(int value)
        {
            Value = value;
            InitializeMonths();
        }

        private void InitializeMonths()
        {
            // Initialize all 12 months with the correct number of days
            for (int i = 1; i <= 12; i++)
            {
                int daysInMonth = DateTime.DaysInMonth(Value, i);
                Months.Add(new Month(i, daysInMonth));
            }
        }
    }

    [Serializable]
    public class Calendar
    {
        public List<Year> Years { get; set; } = new List<Year>();

        public Calendar() 
        {
            // Initialize with the current year
            AddYear(DateTime.Now.Year);
        }

        public void AddYear(int year)
        {
            if (!Years.Exists(y => y.Value == year))
            {
                Years.Add(new Year(year));
            }
        }
    }
    
    [Serializable]
    public class EventInfo
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }

        public EventInfo() { }

        public EventInfo(string eventName, DateTime eventDate)
        {
            EventName = eventName;
            EventDate = eventDate;
        }
    }
}