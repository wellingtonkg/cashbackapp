using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashBackApp.Integration.Spotify
{
    public class DisksService
    {
        private string _clientID;
        private string _clientSecret;

        public DisksService(string clientID, string clientSecret)
        {
            _clientID = clientID;
            _clientSecret = clientSecret;
        }

        public List<Product> GetDisksByGenre(int numberOfDisks, GenreEnum genre)
        {
            var api = GetTokenAuth();

            //Make a search on the Spotify Service
            SearchItem search = api.SearchItems(genre.ToString(), SearchType.Album, numberOfDisks);

            return search.Albums.Items.Select(s => new Product
            {
                Id = Guid.NewGuid(),
                Genre = genre,
                Name = $"{s.Name} - (Date: {s.ReleaseDate})",
                Price = Math.Round(new Random().NextDouble() * 50d, 2)
            }).ToList();
        }

        private SpotifyWebAPI GetTokenAuth()
        {
            CredentialsAuth auth = new CredentialsAuth(_clientID, _clientSecret);
            Token token = auth.GetToken().Result;

            return new SpotifyWebAPI { TokenType = token.TokenType, AccessToken = token.AccessToken };
        }
    }
}
