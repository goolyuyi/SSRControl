using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Rest.Model
{
    public readonly struct SsrServerQuery
    {
        public string Remarks { get; init; }
        public string Server { get; init; }
    }

    public class SsrServerConfig
    {
        private readonly JsonDocument _document;

        public SsrServerConfig(string configJson)
        {
            _document = JsonDocument.Parse(configJson);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(_document);
        }

        public IEnumerable<string> ToJson(SsrServerQuery query)
        {
            var configs = _document.RootElement.GetProperty("configs").EnumerateArray();
            return from c in configs
                where query.Match(c)
                select JsonSerializer.Serialize(c);
        }
    }

    public static class SsrServerQueryExt
    {
        public static bool Match(this SsrServerQuery query, JsonElement config)
        {
            //NOTE pattern this
            if (!string.IsNullOrWhiteSpace(query.Remarks))
            {
                var j = config.GetProperty("remarks_base64").GetString();
                if (j is null) return false;
                var base64EncodedBytes = Convert.FromBase64String(j);
                var str = Encoding.UTF8.GetString(base64EncodedBytes);
                return string.CompareOrdinal(str, query.Remarks) == 0;
            }

            if (!string.IsNullOrWhiteSpace(query.Server))
                return string.CompareOrdinal(config.GetProperty("server").GetString(), query.Server) != 0;

            return false;
        }
    }
}