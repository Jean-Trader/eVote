using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;


namespace eVote.Core.Application.Helpers
{
    public static class SessionsHelper
    {
        public static void setSession<T>(this ISession S, string Key, T value)
        {
            S.SetString(Key, JsonSerializer.Serialize(value));
        }

        public static T? getSession<T>(this ISession S, string Key)
        {
            var value = S.GetString(Key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
