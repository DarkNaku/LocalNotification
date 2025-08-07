using System;
using UnityEngine;

namespace DarkNaku.LocalNotification {
    [Serializable]
    public class NotificationData {
        [SerializeField] private MessageDataGroup[] _messageGroups;
        [SerializeField] private ScheduleData[] _schedules;

        public MessageDataGroup[] MessageGroups => _messageGroups;
        public ScheduleData[] Schedules => _schedules;
    }
}