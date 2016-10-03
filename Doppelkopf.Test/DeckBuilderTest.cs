using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doppelkopf.Core;
using System.Collections.Generic;

namespace Doppelkopf.Test {
    [TestClass]
    public class DeckBuilderTest {
        [TestMethod]
        public void DoppelkopfDeckTest() {
            CardStack deck = DeckBuilder.GetDoppelkopfDeck();
            Assert.AreEqual(40, deck.Cards.Count);

            //Should have two of each Card with a Rank of 10 or higher
            foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
                foreach (Rank rank in Enum.GetValues(typeof(Rank))) {
                    if(rank >= Rank.Ten) {
                        int count = 0;
                        foreach(Card card in deck.Cards) {
                            if(card.Suit == suit && card.Rank == rank) {
                                count++;
                            }
                        }
                        Assert.AreEqual(2, count);
                    }
                }
            }
        }
    }
}
