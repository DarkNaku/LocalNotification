using System;
using UnityEngine;

namespace DarkNaku.LocalNotification {
    [Serializable]
    public struct ScheduleData {
        [SerializeField, Min(0)] private int _messageGroupIndex;
        [SerializeField, Min(1)] private int _afterDays;
        [SerializeField, Range(0, 23)] private int _hours;
        [SerializeField, Range(0, 59)] private int _minutes;
        [SerializeField, Range(0, 59)] private int _seconds;

        public int MessageGroupIndex => _messageGroupIndex;
        public int AfterDays => _afterDays;
        public int Hours => _hours;
        public int Minutes => _minutes;
        public int Seconds => _seconds;
    }
}