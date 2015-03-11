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
        public void TestCreateArtist()
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

        /// <summary>
        /// This method tests the Delete method in the ArtistController. Deleting an artist
        /// also deletes the posts linked to the artist.
        /// </summary>
        [TestMethod]
        public void TestDeleteArtist()
        {
            //arrange ASSEMBLE
            List<Artist> artists = new List<Artist> { new Artist{ID = 1, UserName = "Yojimbo", Reputation=100, DateJoined = DateTime.Now},
                                                      new Artist{ID = 2, UserName = "Kim Kim", Reputation=90, DateJoined = DateTime.Now}};

            List<Post> posts = new List<Post> { new Post { ArtistID = 1, Title = "Title", Description = "Description", ViewCount = 100 },
                                                new Post { ArtistID = 1, Title = "Title", Description = "Description", ViewCount = 10 },
                                                new Post { ArtistID = 2, Title = "Title", Description = "Description", ViewCount = 100 } };

            var target = new ArtistController(new FakeUnitOfWork(null, artists, posts));

            //act
            target.Delete(artists[0]);

            //assert
            Assert.AreEqual(1, artists.Count);
            Assert.AreEqual(1, posts.Count);
        }
    }
}
