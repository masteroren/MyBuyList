using MyBuyListShare.Classes;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace MyBuyListShare.Services
{
    public sealed class AuthService
    {
        private static AuthService instance = null;
        public readonly BehaviorSubject<UserInfo> loginNotifications;
        public Dictionary<string, IDisposable> subscriptions = new Dictionary<string, IDisposable>();

        private AuthService()
        {
            loginNotifications = new BehaviorSubject<UserInfo>(null);
        }

        public static AuthService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthService();
                }

                return instance;
            }
        }

        public void RemoveSubscription(string key)
        {
            if (instance.subscriptions.ContainsKey(key))
            {
                instance.subscriptions[key].Dispose();
                instance.subscriptions.Remove(key);
            }
        }

        public void AddSubscription(string key, IDisposable subscription)
        {
            RemoveSubscription(key);
            instance.subscriptions.Add(key, subscription);
        }

        public void Notify(UserInfo userInfo)
        {
            loginNotifications.OnNext(userInfo);
        }
    }
}
