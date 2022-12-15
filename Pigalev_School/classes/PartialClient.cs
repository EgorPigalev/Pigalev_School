using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigalev_School
{
    /// <summary>
    /// Частичный класс для таблицы Client
    /// </summary>
    public partial class Client
    {
        public string FIO // ФИО Клиента
        {
            get
            {
                return LastName + " " + FirstName + " " + Patronymic;
            }
        }
    }
}
