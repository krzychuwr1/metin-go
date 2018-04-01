using System;
using System.Runtime.CompilerServices;
using MetinGo.Models.Character;
using MetinGo.Models.User;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MetinGo.Infrastructure.Session
{
    public interface ISessionManager
    {
        User User { get; set; }
        Character Character { get; set; }
        double? Latitude { get; set; }
        double? Longitude { get; set; }
    }

    public class SessionManager : ISessionManager
    {
        public User User
        {
            get => GetProperty<User>();
            set => SetProperty(value);
        }

        public Character Character
        {
            get => GetProperty<Character>();
            set => SetProperty(value);
        }

        public double? Longitude
        {
            get => GetProperty<double?>();
            set => SetProperty(value);
        }

        public double? Latitude
        {
            get => GetProperty<double?>();
            set => SetProperty(value);
        }


        public T GetProperty<T>([CallerMemberName] string propertyName = "")

        {
            if (typeof(T).IsPrimitive || typeof(T) == typeof(Guid?) || typeof(T) == typeof(double?))
                return Application.Current.Properties.TryGetValue(propertyName, out var objProperty)
                    ? (T)objProperty
                    : default(T);

            if (!typeof(T).IsPrimitive)
                return App.Current.Properties.TryGetValue(propertyName, out var objProperty) && objProperty is string s
                    ? JsonConvert.DeserializeObject<T>(s)
                    : default(T);


            throw new ArgumentException($"Invalid usage of GetProperty, type {typeof(T)}");
        }


        public void SetProperty(object value, [CallerMemberName] string propertyName = "")
        {
            if (Application.Current.Properties.ContainsKey(propertyName))

                Application.Current.Properties.Remove(propertyName);

            if(!value.GetType().IsPrimitive)
                Application.Current.Properties.Add(propertyName, JsonConvert.SerializeObject(value));
            else
                Application.Current.Properties.Add(propertyName, value);
        }
    }
}