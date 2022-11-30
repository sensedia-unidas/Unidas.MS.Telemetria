using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;

namespace Unidas.MS.Telemetria.Application.Services
{
    public static class Util
    {
        public static string Serialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (JsonException ex)
            {
                throw new JsonNotValidException();
            }
        }

        public static T Derialize<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json)!;
            }
            catch (JsonException ex)
            {
                throw new JsonNotValidException();
            }
        }
    }
}
