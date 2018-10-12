using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCards.Entities
{
    /*
    Вы собираетесь совершить долгое путешествие через множество населенных пунктов.

Чтобы не запутаться, вы сделали карточки вашего путешествия.Каждая карточка содержит в себе пункт отправления и пункт назначения.

Например, у нас есть карточки


Мельбурн → Кельн

Москва → Париж

Кельн → Москва

Нужно упорядочить их так, чтобы пункт назначения на ней совпадал с пунктом отправления на следующей карточке:


Мельбурн → Кельн, Кельн → Москва, Москва → Париж

Требуется:

1. Написать метод, который принимает набор неупорядоченных карточек и возвращает набор упорядоченных карточек в соответствии с требованиями выше - то есть в возвращаемом из функции списке карточек для каждой карточки пункт назначения на ней должен совпадать с пунктом отправления на следующей карточке.

2. Написать тесты.


Оценивается:

1. Читабельность и понятность кода

2. Полнота набора тестов

3. Скорость работы метода
*/



    /// <summary>
    /// One step of the travel entity. Contains from and to points.
    /// </summary>
    public class TravelCard : ICloneable
    {
        private string _from;
        private string _to;

        public string From { get => _from; private set => _from = value; }
        public string To { get => _to; set => _to = value; }

        public TravelCard(string from, string to)
        {
            From = from;
            To = to;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        #region Equals & HashCode
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(TravelCard other)
        {
            if (other == null)
                return false;

            return other.From.Equals(_from, StringComparison.InvariantCulture)
                && other.To.Equals(_to, StringComparison.InvariantCulture);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + _from.GetHashCode();
                hash = hash * 31 + _to.GetHashCode();
                return hash;
            }
        }
        #endregion
    }
}
