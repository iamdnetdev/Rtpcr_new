using System;
using System.Collections.Generic;
using System.Linq;

namespace RtpcrCustomerApp.BusinessModels.Common
{
    public static class VaccinationSlots
    {
        private const string Slot8_9 = "8 am - 9 am";
        private const string Slot9_10 = "9 am - 10 am";
        private const string Slot10_11 = "10 am - 11 am";
        private const string Slot11_12 = "11 am - 12 noon";
        private const string Slot12_13 = "12 noon - 1 pm";
        private const string Slot13_14 = "1 pm - 2 pm";
        private const string Slot14_15 = "2 pm - 3 pm";
        private const string Slot15_16 = "3 pm - 4 pm";
        private const string Slot16_17 = "4 pm - 5 pm";
        private const string Slot17_18 = "5 pm - 6 pm";
        private const string Slot18_19 = "6 pm - 7 pm";
        private const string Slot19_20 = "7 pm - 8 pm";

        /*
        8 am - 9 am
        9 am - 10 am
        10 am - 11 am
        11 am - 12 noon
        12 noon - 1 pm
        1 pm - 2 pm
        2 pm - 3 pm
        3 pm - 4 pm
        4 pm - 5 pm
        5 pm - 6 pm
        6 pm - 7 pm
        7 pm - 8 pm
        */

        public static readonly Dictionary<string, int> Slots = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
        {
            { Slot8_9, 8 },
            { Slot9_10, 9 },
            { Slot10_11, 10 },
            { Slot11_12, 11 },
            { Slot12_13, 12 },
            { Slot13_14, 13 },
            { Slot14_15, 14 },
            { Slot15_16, 15 },
            { Slot16_17, 16 },
            { Slot17_18, 17 },
            { Slot18_19, 18 },
            { Slot19_20, 19 }
        };

        public static string FindSlot(DateTime? scheduleDate)
        {
            if (!scheduleDate.HasValue) return string.Empty;
            return Slots.FirstOrDefault(s => scheduleDate.Value.Hour == s.Value).Key ?? string.Empty;
        }
    }
}
