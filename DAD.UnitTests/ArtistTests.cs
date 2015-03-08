using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigitalArtistDatabase.Controllers;
using DigitalArtistDatabase.DAL;
using DigitalArtistDatabase.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAD.UnitTests
{
    [TestClass]
    public class AuthorTests
    {
        [TestMethod]
        public void TestCreateAuthor()
        {
            //arrange ASSEMBLE
            List<Artist> artists = new List<Artist>();
            var target = new ArtistController(new FakeUnitOfWork(null, artists, null, null, null));

            //act
            const string NAME = "MrTest";
            Artist artist = new Artist { UserName = NAME };
            target.Create(artist);

            //assert
            Assert.AreEqual(NAME, artists[0].UserName);
        }
    }
}
