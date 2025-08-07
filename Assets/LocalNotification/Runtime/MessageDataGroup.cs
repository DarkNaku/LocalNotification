using System;
using UnityEngine;

namespace DarkNaku.LocalNotification {
    [Serializable]
    public struct MessageDataGroup {
        [SerializeField] private MessageData[] _messages;

        public MessageData[] Messages => _messages;
    }
}
