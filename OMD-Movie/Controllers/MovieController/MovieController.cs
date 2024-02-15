using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMD_Movie.Dto;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http;
using System.Numerics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OMD_Movie.Controllers.MovieController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly int _omdKey;
        private List<string> urlPath;
       // private readonly HttpContext _context;
        //private readonly object[] _searchResults;
        //private readonly MovieByTitleDto _searchResults;
        // MovieByTitleDto searchResults
        //private dynamic _queryData;
        public MovieController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
            _omdKey = _config.GetSection("movieKey").Get<int>();
           // _context = context;
            //  _searchResults = searchResults;
            //_queryData = queryData;
            urlPath = new List<string>();
        }

        /// <summary>
        ///Get a list of movies by title
        ///  <summary>
        [HttpGet("getmoviebytitle")]
        public async Task<ActionResult> GetMovieByTitle(string title)
        {
            HttpContext context = HttpContext; // Access the HttpContext property
           // string userAgent = context.Request.Headers["User-Agent"];
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync($"http://www.omdbapi.com/?apikey={_omdKey}&s={title}");
            urlPath.Add(context.Request.GetEncodedUrl());
           /* foreach (string num in urlPath)
            {
                string.Join(", ", num);
               // Console.WriteLine(num);
            }*/
            Console.Write(string.Join(System.Environment.NewLine, urlPath));
           // string items = string.Join(Environment.NewLine, urlPath);

            response.EnsureSuccessStatusCode();
           
            var json = await response.Content.ReadAsStringAsync();
            var dataResponse = JsonSerializer.Deserialize<object>(json);
            Console.WriteLine(urlPath);
            //   if(dataResponse != null)
            //  {
            // _searchResults = (MovieByTitleDto)dataResponse;
            //  Console.WriteLine(_searchResults);
            if (dataResponse != null)
            {
                // _queryData = dataResponse;
                MovieByTitleDto.info = dataResponse;
               // Console.WriteLine(MovieByTitleDto.info);
                return Ok(dataResponse);

            }
            return Ok(dataResponse);

            //  }
            // return Ok(null);


        }

        /// <summary>
        /// Get further information of a movie using a valid IMDB ID
        ///  <summary>
        ///   <remarks>
        ///   Get further information of a movie using a valid IMDB ID
        ///   You can get a valid IMDB ID from getmoviebytitle API
        ///   <remarks>
        [HttpGet("getmoviebyid")]
        public async Task<ActionResult> GetMovieById(string id)
        {          
           // HttpContext context = HttpContext; // Access the HttpContext property
           // string userAgent = context.Request.Headers["User-Agent"];
            //Console.WriteLine(userAgent);
            var client = _clientFactory.CreateClient();        
            HttpResponseMessage response = await client.GetAsync($"http://www.omdbapi.com/?apikey={_omdKey}&i={id}");
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(HttpContext.Request.GetEncodedUrl());
           // urlPath.Add(context.Request.GetEncodedUrl());
                     
            //Console.WriteLine(urlPath);
            var json = await response.Content.ReadAsStringAsync();
            var dataResponse = JsonSerializer.Deserialize<object>(json);
          //  Console.WriteLine(MovieByTitleDto.info);
            return Ok(dataResponse);

        }
        [HttpGet("getlatestsearchqueries")]
        public ActionResult GetLatestSearchQueries()
        {
            string items = string.Join(Environment.NewLine, urlPath);
            Console.WriteLine(items);
            return Ok(items);
            /* if(urlPath is null)
             {
                return Ok(Array.Empty<string>());
             }*/
            //string[] termsList );
          /*  if (urlPath is null)
            {
               // Console.WriteLine(urlPath);
               return Ok(Array.Empty<string>());
            }

               foreach (var i in urlPath)
            {
                Console.WriteLine(i);
                return Ok(i);
            }

         return Ok(urlPath.ToArray<object>());*/

        }
        /* [HttpGet("getlatestsearchresults")]
         public  ActionResult GetLatestSearchResults()
         {
             dynamic? data = MovieByTitleDto.info;
             string[] stringArray = new string[] { };
             if (data is null) 
             {
                 //string[] emptyData = Array.Empty<string>(); 
                 // return Ok(Array.Empty<object>());
                 // throw new ArgumentException(nameof(data));
                 return Ok(stringArray);

             }
            var selectFiveData = data ;
            // var selectFiveData = data ;
            // var selectFive = typeof(object).GetProperty("Search").GetValue(data);
           // var selectFive = getProp(data, "Search");
             //Console.WriteLine(selectFiveData);
             // ArraySegment<object> segmentData = new ArraySegment<object>(selectFiveData, 0, 5);
             //  Console.WriteLine(segmentData);
             // return  Ok(segmentData.ToList());
             return Ok(selectFiveData);

         }*/

        /*  dynamic getProp(dynamic source, string name)
          {
              var type = source.GetType();
              var prop = type.GetProperty(name);
              return prop.GetValue(source);
          }*/





    }
}
