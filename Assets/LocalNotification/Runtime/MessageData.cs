using System;
using UnityEngine;

namespace DarkNaku.LocalNotification {
    [Serializable]
    public struct MessageData {
        [SerializeField] private string _title;
        [SerializeField] private string _body;

        public string Title => _title;
        public string Body => _body;
    }
}