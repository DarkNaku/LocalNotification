using System;
using System.Collections;
using Unity.Notifications;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

namespace DarkNaku.LocalNotification {
    public class LocalNotification : MonoBehaviour {
        [SerializeField] private string _androidSmallIcon = "icon_0";
        [SerializeField] private string _androidLargeIcon = "icon_1";
        [SerializeField] private NotificationData _notificationData;

        private const string AndroidChannel = "default";
        private const string AndroidChannelName = "Notifications";
        private const string AndroidChannelDescription = "Main notifications";

        private static LocalNotification _instance;
        private bool _isInitialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void OnSubsystemRegistration() {
            _instance = null;
        }
        
        //public static void ScheduleNotification(int id, string title, string body, DateTime time) => _instance?._ScheduleNotification(id, title, body, time);
        //public static void CancelScheduleNotification(int id) => NotificationCenter.CancelScheduledNotification(id);
        //public static void CancelAllScheduledNotifications() => NotificationCenter.CancelAllScheduledNotifications();

        private void Awake() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        private IEnumerator Start() {
            if (_isInitialized) yield break;

            var args = NotificationCenterArgs.Default;
            args.AndroidChannelId = AndroidChannel;
            args.AndroidChannelName = AndroidChannelName;
            args.AndroidChannelDescription = AndroidChannelDescription;
            NotificationCenter.Initialize(args);

            _isInitialized = true;

            NotificationCenter.CancelAllDeliveredNotifications();

            var request = NotificationCenter.RequestPermission();

            if (request.Status == NotificationsPermissionStatus.RequestPending) {
                yield return request;
            }

            Debug.Log($"[LocalNotifications] Permission Result : {request.Status}");
        }

        private void OnApplicationPause(bool pauseStatus) {
            if (pauseStatus) {
                NotificationCenter.CancelAllScheduledNotifications();

                var now = DateTime.Now;

                var messageGroupIndices = DictionaryPool<int, int>.Get();

                for (int i = 0; i < _notificationData.MessageGroups.Length; i++) {
                    messageGroupIndices.Add(i, Random.Range(0, _notificationData.MessageGroups[i].Messages.Length));
                }

                for (int i = 0; i < _notificationData.Schedules.Length; i++) {
                    var schedule = _notificationData.Schedules[i];
                    var messageGroup = _notificationData.MessageGroups[schedule.MessageGroupIndex];

                    if (messageGroup.Messages.Length == 0) continue;

                    var messageIndex = messageGroupIndices[schedule.MessageGroupIndex];
                    var time = new DateTime(now.Year, now.Month, now.Day, schedule.Hours, schedule.Minutes, schedule.Seconds);
                    var message = messageGroup.Messages[messageIndex];

                    if (schedule.AfterDays > 0) {
                        ScheduleNotification(message, time.AddDays(schedule.AfterDays));
                    }

                    messageGroupIndices[schedule.MessageGroupIndex] = (messageIndex + 1) % messageGroup.Messages.Length;
                }
                
                DictionaryPool<int, int>.Release(messageGroupIndices);
            }
        }
        
        private void _ScheduleNotification(int id, string title, string body, DateTime time) {
            var schedule = new NotificationDateTimeSchedule(time);
            var notification = new Notification() { Identifier = id, Title = title, Text = body };
            
            ScheduleNotification(notification, schedule);
        }

        private void ScheduleNotification(MessageData message, DateTime time) {
            var schedule = new NotificationDateTimeSchedule(time);
            var notification = new Notification() { Title = message.Title, Text = message.Body };
            
            ScheduleNotification(notification, schedule);
        }
        
        private void ScheduleNotification(Notification notification, NotificationDateTimeSchedule schedule) {
#if UNITY_ANDROID
            var androidNotification = (AndroidNotification)notification;
            androidNotification.SmallIcon = _androidSmallIcon;
            androidNotification.LargeIcon = _androidLargeIcon;
            androidNotification.FireTime = schedule.FireTime;
            AndroidNotificationCenter.SendNotification(androidNotification, AndroidChannel);
#else
            NotificationCenter.ScheduleNotification(notification, schedule);
#endif
        }
    }
}