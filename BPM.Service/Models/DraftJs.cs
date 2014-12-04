using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class DraftJs
    {
        private readonly string _prefix;

        public DraftJs(string prefix)
        {
            _prefix = prefix;
            Items = new List<string>();
        }

        public List<string> Items { get; private set; }

        public string GetJson()
        {
            var result = new StringBuilder();
            foreach (var item in Items)
            {
                result.AppendFormat("{0}_{1} : $('#{1}').val(),\n", _prefix, item);
            }
            return result.ToString().Trim('\n', ',');
        }

        public string GetNames()
        {
            return JsonConvert.SerializeObject(Items.Select(item => string.Format("{0}_{1}", _prefix, item)).ToArray());
            //var result = new StringBuilder();
            //foreach (var item in Items)
            //{
            //    result.AppendFormat("{0}_{1}{2}", _prefix, item, Protocols.ID_SPLITTER);
            //}
            //return result.ToString().Trim(Protocols.ID_SPLITTER);
        }

        public string GetRecover()
        {
            var result = new StringBuilder();
            foreach (var item in Items)
            {
                result.AppendFormat("if (data.{0}_{1} != undefined && data.{0}_{1} != \"\") {{\n", _prefix, item);
                result.AppendFormat("    $(\"#{1}\").val(data.{0}_{1});\n", _prefix, item);
                result.Append("}");
            }
            return result.ToString();
        }
    }
}