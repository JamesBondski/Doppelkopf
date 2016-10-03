using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doppelkopf.Core {
    public class CardStack {
        List<Card> cards = new List<Card>();

        internal List<Card> CardsInternal {
            get { return cards; }
        }

        public IReadOnlyList<Card> Cards {
            get { return cards.AsReadOnly(); }
        }
    }
}
